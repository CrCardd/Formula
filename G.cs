using Formula.Interfaces;

public class G : Behavior<Ball>
{
    public int g = 980;  //        pixels/s²     ||    100px -> 1m   ||  980px -> 9.8m;

    // public int? CollideGround(IWorld world, double x, double y)
    // {
    //     if(y < 0 || y >= world.Height)
    //         return null;
    //     y++;
    //     for(; y<world.Height; y++)
    //         if(!world.isFree(x,y))
    //             return (int)y;
        
    //     return (int)y;
    // }

    // public bool Collide(IWorld world, double x, double y, double destX, double destY)
    // {
        
    //     world.isFree(x+deltaX,y+deltaY) && world.isValid(x+deltaX,y+deltaY);
    // }


    public override void Execute(Ball obj, IWorld world, double t)
    {
        obj.SpeedY += g * t;
        
        // var ground = CollideGround(world, obj.X, obj.Y);
        // if(ground is null)
        //     return;

        var y = obj.Y + obj.SpeedY * t;
        var x = obj.X + obj.X * t;

        // if(y > ground-1 && obj.SpeedY < 1)
        // {
        //     obj.Y = (double)ground-1;
        //     obj.SpeedY = 0;
        // }
        // else
        //     if(y > ground-1 || y < 0)
        //         if(obj.SpeedY > 0)
        //         {
        //             obj.Y = (double)ground-1;
        //             obj.SpeedY *= -0.8;
        //             obj.SpeedX *= 0.8;
        //         }
        //         else
        //             obj.SpeedY *= -1;
        //     else
        //         obj.Y = y;



        // var x = obj.X + obj.SpeedX * t;
        // if(x > world.Width-1 || x < 0)
        // {
        //     if(x < 0)
        //         obj.X = 0;
        //     else
        //         obj.X = world.Width-1;
        //     obj.SpeedX *= -0.8;
        // }
        // else
        //     obj.X = x;

        
        // if(Collide(obj.X,obj.Y,deltaY));
    }
}