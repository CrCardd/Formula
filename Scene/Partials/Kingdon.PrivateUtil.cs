
using System;
using System.Collections.Generic;
using System.Linq;
using Formula.Enum;
using Formula.Objects;

namespace Formula.Scene;

partial class Kingdon
{
    public Dictionary<Guid, BaseOBject> Objects = [];
    private Dictionary<(int,int), List<BaseOBject>> GridObjects = [];
    
    public void MoveObjects()
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

            RemoveFromGridObjects(prevPos,move);
            SetOnGridObjects(targetPos, move);

            move.Flags &= ~DirtyFlags.MoveDirty;
        }
        toMove.Clear();
    }
    public void DestroyObjects()
    {
        while(toDestroy.Count > 0)
        {
            var obj = toDestroy.Dequeue();
            RemoveFromGridObjects(((int)obj.PrevPosition.X, (int)obj.PrevPosition.Y), obj);
            Objects.Remove(obj.Id);
        }
    }
    public void SpawnObjects()
    {
        while(toSpawn.Count > 0)
        {
            var spawn = toSpawn.Dequeue();

            Objects.Add(spawn.Id, spawn);
            
            SetOnGridObjects(((int)spawn.X, (int)spawn.Y), spawn);
        }
        toSpawn.Clear();
    }

    private void SetOnGridObjects((int,int) pos, BaseOBject obj)
    {
        if(!GridObjects.TryGetValue(pos, out var gridPos))
            GridObjects.Add(pos, []);
        GridObjects[pos].Add(obj);
    }
    private void RemoveFromGridObjects((int,int) pos, BaseOBject obj)
    {
        if(!GridObjects.TryGetValue(pos, out var gridPos)) return;
        if(gridPos.FirstOrDefault(p => p.Id == obj.Id) is null) return;
        gridPos.Remove(gridPos.First(p => p.Id == obj.Id));
    }

    


    public IReadOnlyCollection<T> GetRealPlace<T>(double x, double y) where T : BaseOBject 
    => GridObjects[((int)x, (int)y)].Select(pos => (T)pos).ToList();
    public IReadOnlyCollection<BaseOBject> GetRealPlace(double x, double y) => GetRealPlace<BaseOBject>(x,y);

    public IReadOnlyCollection<T>? GetRealPlaceOrDefault<T>(double x, double y) where T : BaseOBject
    {
        if (GridObjects.TryGetValue(((int)x, (int)y), out var pos)) 
            return pos
                .Where(p => typeof(T) == p.GetType())
                .Select(p => (T)p)
                .ToList();
        return null;
    }
    public IReadOnlyCollection<BaseOBject>? GetRealPlaceOrDefault(double x, double y) => GetRealPlaceOrDefault<BaseOBject>(x,y);
}