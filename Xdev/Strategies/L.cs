using System.Linq;
using Formula.Interfaces;
using Formula.Objects;

public class L : Behavior
{
    public override void Execute(BaseOBject obj, IWorld world)
    {
        int m = 1;
        var pos = world.GetPlaceOrDefault(obj.X, obj.Y+m);
        if(
            world.isValid(obj.X, obj.Y+m) 
            && (pos is null || pos?.Count() < world.Depth
            && (!pos.Any() || pos.Any(p => p.Z != obj.Z)))
        )
            obj.Y+=m;
    }
}