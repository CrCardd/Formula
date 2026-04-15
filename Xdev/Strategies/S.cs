using System;
using System.Configuration;
using System.Linq;
using Formula.Interfaces;
using Formula.Objects;

public class S : Behavior
{
    public override void Execute(BaseOBject obj, IWorld world)
    {
        int m = Random.Shared.Next(0,3);
        var pos = world.GetPlaceOrDefault(obj.X, obj.Y+m);
        if(world.isValid(obj.X, obj.Y+m) && (pos is null || pos?.Count < world.Depth))
            obj.Y+=m;
        // var neighborhood = world.GetGrid(obj.X,obj.Y, true);
        // var free = neighborhood.Where(n => !n.Value.Any()).ToList();
        // if(free.Count <= 0)
        //     return;
        // var pos = free[Random.Shared.Next(0,free.Count())]; 
        // if(pos.Key.Y < obj.Y || pos.Key.Y == obj.Y)
        //     return;
        // world.New(new BaseOBject(pos.Key.X, pos.Key.Y, behavior: this));
    }
}