
using System;
using System.Collections.Generic;
using System.Linq;
using Formula.Enum;
using Formula.Math;
using Formula.Objects;

namespace Formula.Scene;

partial class Kingdon
{
    public Dictionary<Guid, IObject> Objects = [];
    private Dictionary<(int,int), IObject> GridObjects = [];
    
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
            if (GridObjects.TryGetValue(targetPos, out var occupant) && occupant != move)
            {
                move.RestorePosition();
                continue;
            }
            GridObjects.Remove(((int)move.PrevPosition.X, (int)move.PrevPosition.Y));
            GridObjects.Add(targetPos, move);
        }
        toMove.Clear();
    }
    public void DestroyObjects()
    {
        while(toDestroy.Count > 0)
        {
            var obj = toDestroy.Dequeue();
            GridObjects.Remove(((int)obj.PrevPosition.X, (int)obj.PrevPosition.Y));
            Objects.Remove(obj.Id);
        }
    }
    public void SpawnObjects()
    {
        while(toSpawn.Count > 0)
        {
            var spawn = toSpawn.Dequeue();
            var targetPos = ((int)spawn.X, (int)spawn.Y);
            if(GridObjects.Keys.Contains(targetPos)) continue;
            Objects.Add(spawn.Id, spawn);
            GridObjects.Add(((int)spawn.X, (int)spawn.Y), spawn);
        }
        toSpawn.Clear();
    }




    public IObject GetRealPlace(double x, double y) => GridObjects[((int)x, (int)y)];
    public T GetRealPlace<T>(double x, double y) where T : IObject => (GridObjects[((int)x, (int)y)] as T)!;
    public IObject? GetRealPlaceOrDefault(double x, double y) 
    {
        if (GridObjects.TryGetValue(((int)x, (int)y), out var obj)) return obj;
        return null;
    }
    public T? GetRealPlaceOrDefault<T>(double x, double y) where T : IObject
    {
        if (GridObjects.TryGetValue(((int)x, (int)y), out var obj)) return obj as T;
        return null;
    }
    public IObject? GetRealPlace(Vector2D position)
    {
        if (GridObjects.TryGetValue(((int)position.X, (int)position.Y), out var obj)) return obj;
        return null;
    }

}