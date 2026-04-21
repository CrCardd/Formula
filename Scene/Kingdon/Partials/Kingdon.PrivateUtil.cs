
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

    private HashSet<Guid> processing = new(); // Para detectar ciclos

    private void TryMove(BaseOBject obj, HashSet<BaseOBject> moved)
    {

        if (moved.Contains(obj)) return;
        if (processing.Contains(obj.Id)) return;
        
        processing.Add(obj.Id);

        if (obj.X < 0 || obj.X >= Width || obj.Y < 0 || obj.Y >= Height)
        {
            obj.RestorePosition();
            processing.Remove(obj.Id);
            return;
        }

        var targetPos = ((int)obj.X, (int)obj.Y);
        var prevPos = ((int)obj.PrevPosition.X, (int)obj.PrevPosition.Y);

        if (targetPos == prevPos)
        {
            obj.Flags &= ~DirtyFlags.MoveDirty;
            moved.Add(obj);
            processing.Remove(obj.Id);
            return;
        }

        if (GridObjects.TryGetValue(targetPos, out var occupants) && occupants.Count > 0)
        {
            if (Depth is not null && occupants.Count >= Depth)
            {
                var currentOccupants = occupants.ToList();
                foreach (var occupant in currentOccupants)
                    if ((occupant.Flags & DirtyFlags.MoveDirty) != DirtyFlags.None) TryMove(occupant, moved);
                    
            }

            if (Depth is not null && occupants.Count >= Depth)
            {
                obj.RestorePosition();
                processing.Remove(obj.Id);
                return;
            }
        }


        RemoveFromGridObjects(prevPos, obj);
        SetOnGridObjects(targetPos, obj);
        
        obj.Flags &= ~DirtyFlags.MoveDirty;
        moved.Add(obj);
        processing.Remove(obj.Id);
    }
        
    internal void MoveObjects()
    {   
        var toMove = Objects.Values.Where(o => (o.Flags & DirtyFlags.MoveDirty) != DirtyFlags.None).ToList();
        HashSet<BaseOBject> moved = [];

        foreach(var move in toMove)
            TryMove(move, moved);
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