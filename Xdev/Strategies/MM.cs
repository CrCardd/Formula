using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Objects;

public class MM : Behavior
{
    public override void Execute(BaseOBject obj, IWorld world, double t)
    {
        if(world.IsKeyDown(Keys.Right))
            obj.X++;
        if(world.IsKeyDown(Keys.Left))
            obj.X--;
        if(world.IsKeyDown(Keys.Up))
            obj.Y--;
        if(world.IsKeyDown(Keys.Down))
            obj.Y++;
    }
}