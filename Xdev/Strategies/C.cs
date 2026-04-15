using System.Linq;
using Formula.Interfaces;

public class C : Behavior<Cell>
{
    public override void Execute(Cell obj, IWorld world)
    {
        if(!world.GetFlag("run"))
            return;

        int n = world.RadiusAreaObjects<Cell>(obj.X, obj.Y).Count(n => n.Alive);
        if(obj.Alive)
            if(n < 2 || n > 3)
                obj.Alive = false;
        if(!obj.Alive)
            if(n == 3)      
                obj.Alive = true;
    }
}