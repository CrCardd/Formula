
using Formula.Enum;

namespace Formula.Scene;

partial class Kingdon
{
    public void MoveObjects()
    {   
        var toMove = Objects.Values.Where(o => (o.Flags & DirtyFlags.MoveDirty) != DirtyFlags.None).ToList();

        foreach(var move in toMove)
        {

            var targetPos = (move.Position.X, move.Position.Y);
            move.Flags &= ~DirtyFlags.MoveDirty;

            if(GridObjects.Keys.Contains(targetPos))
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
            GridObjects.Remove((obj.Position.X, obj.Position.Y));
            Objects.Remove(obj.Id);
        }
    }
    public void SpawnObjects()
    {
        while(toSpawn.Count > 0)
        {
            var spawn = toSpawn.Dequeue();
            var targetPos = (spawn.Position.X, spawn.Position.Y);
            if(GridObjects.Keys.Contains(targetPos)) continue;
            Objects.Add(spawn.Id, spawn);
            GridObjects.Add((spawn.Position.X, spawn.Position.Y), spawn);
        }
        toSpawn.Clear();
    }

}