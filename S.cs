using Formula.Interfaces;
using Formula.Objects;

public class S : Behavior
{
    public override void Execute(IObject obj, IWorld world)
    {
        if(world.isValid(obj.X, obj.Y+1) && world.isFree(obj.X, obj.Y+1))
            obj.Y++;
        var pos = world.GetRandom8FreeNeighboorPlace(obj.X,obj.Y);
        if(pos is null || pos.Value.Y < obj.Y || pos.Value.Y == obj.Y)
            return;
        world.New(new IObject(pos.Value.X, pos.Value.Y, behavior: this));
    }
}