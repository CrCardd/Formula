
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
        var toMove = Objects.Values.Where(o => (o.Flags & DirtyFlags.MoveDirty) != DirtyFlags.None).ToList();;

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
                // move.RestorePosition(); 
                // continue;
            }

            var targetPos = (move.X, move.Y);
            if (GridObjects.TryGetValue(targetPos, out var occupant) && occupant != move)
            {
                move.RestorePosition();
                continue;
            }

            GridObjects.Remove((move.PrevPosition.X, move.PrevPosition.Y));
            GridObjects.Add(targetPos, move);
        }
        toMove.Clear();
    }
    public void DestroyObjects()
    {
        while(toDestroy.Count > 0)
        {
            var obj = toDestroy.Dequeue();
            GridObjects.Remove((obj.PrevPosition.X, obj.PrevPosition.Y));
            Objects.Remove(obj.Id);
        }
    }
    public void SpawnObjects()
    {
        while(toSpawn.Count > 0)
        {
            var spawn = toSpawn.Dequeue();
            var targetPos = (spawn.X, spawn.Y);
            if(GridObjects.Keys.Contains(targetPos)) continue;
            Objects.Add(spawn.Id, spawn);
            GridObjects.Add((spawn.X, spawn.Y), spawn);
        }
        toSpawn.Clear();
    }




    public IObject GetRealPlace(int x, int y) => GridObjects[(x, y)];
    public T GetRealPlace<T>(int x, int y) where T : IObject => (GridObjects[(x, y)] as T)!;
    public IObject? GetRealPlaceOrDefault(int x, int y) 
    {
        if (GridObjects.TryGetValue((x, y), out var obj)) return obj;
        return null;
    }
    public T? GetRealPlaceOrDefault<T>(int x, int y) where T : IObject
    {
        if (GridObjects.TryGetValue((x, y), out var obj)) return obj as T;
        return null;
    }
    public IObject? GetRealPlace(Vector2D position)
    {
        if (GridObjects.TryGetValue((position.X, position.Y), out var obj)) return obj;
        return null;
    }

}