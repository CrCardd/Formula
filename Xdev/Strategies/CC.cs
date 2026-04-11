using System;
using System.Linq;
using Formula.Interfaces;

public class CC : Behavior<Cell>
{
    public override void Execute(Cell obj, IWorld world)
    {
        if(!world.GetFlag("run"))
            return;

        int n = world.NNeighborCells<Cell>(obj.X, obj.Y).Count(n => n.Alive);
        if(obj.Alive)
        {
            if(n < 2 || n > 5)
                obj.Alive = false;
            if(Random.Shared.Next(1,25) == 1)
                obj.Alive = true;
        }
        if(!obj.Alive)
            if(n == 3)      
                obj.Alive = true;
    }
}