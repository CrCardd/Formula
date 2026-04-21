using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Objects;

public class M : Behavior
{
    public override void Execute(BaseOBject obj, IWorld world, double t)
    {
        if(world.IsKeyDown(Keys.D))
            obj.X++;
        if(world.IsKeyDown(Keys.A))
            obj.X--;
        if(world.IsKeyDown(Keys.W))
            obj.Y--;
        if(world.IsKeyDown(Keys.S))
            obj.Y++;
    }
}