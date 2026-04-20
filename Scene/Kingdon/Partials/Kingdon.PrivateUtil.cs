
using System;
using System.Collections.Generic;
using System.Linq;
using Formula.Enum;
using Formula.Objects;

namespace Formula.Scene;

partial class SceneMap
{
    public Dictionary<Guid, BaseOBject> Objects = [];
    public Dictionary<Vector2D, List<BaseOBject>> GridObjects = [];
    
    internal void MoveObjects()
    {   
        var toMove = Objects.Values.Where(o => (o.Flags & DirtyFlags.MoveDirty) != DirtyFlags.None).ToList();

        foreach(var move in toMove)
        {

            if (move.X < 0 || move.X >= Width || move.Y < 0 || move.Y >= Height)
            {
                throw new Exception($@"
                =========Index out of map!=========
                Object
                X - {move.X,-5} | Y - {move.Y} 

                Map
                Width - {Width,-5} | Height - {Height} 
                ");
            }

            var targetPos = ((int)move.X, (int)move.Y);
            var prevPos = ((int)move.PrevPosition.X, (int)move.PrevPosition.Y);
            if (
                GridObjects.TryGetValue(targetPos, out var occupant) 
                && (occupant.Contains(move)
                || (Depth is not null && occupant.Count >= Depth))
            )
            {
                move.RestorePosition();
                continue;
            }
            RemoveFromGridObjects(prevPos,move);
            SetOnGridObjects(targetPos, move);

            move.Flags &= ~DirtyFlags.MoveDirty;
        }
        toMove.Clear();
    }
    internal void DestroyObjects()
    {
        while(toDestroy.Count > 0)
        {
            var obj = toDestroy.Dequeue();
            ApplyDestroy(obj, ((int)obj.PrevPosition.X, (int)obj.PrevPosition.Y));
        }
    }
    internal void ApplyDestroy(BaseOBject obj, Vector2D pos)
    {
        RemoveFromGridObjects(pos, obj);
        Objects.Remove(obj.Id);
    }

    internal void SpawnObjects()
    {
        while(toSpawn.Count > 0)
        {
            var spawn = toSpawn.Dequeue();
            var targetPos = ((int)spawn.X, (int)spawn.Y);
            var pos = GetPlaceOrDefault(spawn.X, spawn.Y);
            if(pos is not null && Depth is not null && pos.Count() >= Depth) continue;

            Objects.Add(spawn.Id, spawn);
            SetOnGridObjects(targetPos, spawn);
        }
        toSpawn.Clear();
    }
    internal void ApplySpawn(BaseOBject obj, Vector2D pos)
    {
        Objects.Add(obj.Id, obj);
        SetOnGridObjects(pos, obj);
    }

    private void SetOnGridObjects(Vector2D pos, BaseOBject obj)
    {
        if(!GridObjects.TryGetValue(pos, out var gridPos))
            GridObjects.Add(pos, []);
        GridObjects[pos].Add(obj);
    }
    private void RemoveFromGridObjects(Vector2D pos, BaseOBject obj)
    {
        if(!GridObjects.TryGetValue(pos, out var gridPos)) return;
        if(gridPos.FirstOrDefault(p => p.Id == obj.Id) is null) return;
        gridPos.Remove(gridPos.First(p => p.Id == obj.Id));
    }
}