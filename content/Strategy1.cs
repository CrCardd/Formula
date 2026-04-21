using Formula.Interfaces;
using Formula.Objects;

public class Strategy1 : Behavior
{
    public override void Execute(BaseOBject obj, IWorld world)
    {
        obj.X = 1;
        obj.Y = 1;
    }
}