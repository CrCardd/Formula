using System;
using System.Linq;
using Formula.Interfaces;
using Formula.Objects;

public class S : Behavior
{
    public override void Execute(BaseOBject obj, IWorld world)
    {
        int m = 1;
        var pos = world.GetPlaceOrDefault(obj.X, obj.Y+m);
        if(world.isValid(obj.X, obj.Y+m) && (pos is null || pos?.Count() < world.Depth))
            obj.Y+=m;

        var neighborhood = world.GetGrid(obj.X,obj.Y, true);
        var free = neighborhood.Where(n => !n.Value.Any()).ToList();
        if(free.Count <= 0)
            return;
        var randomPos = free[Random.Shared.Next(0,free.Count())]; 
        if(randomPos.Key.Y < obj.Y || randomPos.Key.Y == obj.Y)
            return;
        world.New(new BaseOBject(randomPos.Key.X, randomPos.Key.Y, behavior: this));
    }
}