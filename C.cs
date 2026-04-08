using Formula.Interfaces;

public class C : Behavior<Cell>
{
    public int CountNeighborhood(IWorld world, int x, int y)
    {
        int n = 0;
        if(world.isValid(x,y-1) && world.GetPlace<Cell>(x,y-1).Alive)
            n++;
        if(world.isValid(x,y+1) && world.GetPlace<Cell>(x,y+1).Alive)
            n++;
        if(world.isValid(x-1,y) && world.GetPlace<Cell>(x-1,y).Alive)
            n++;
        if(world.isValid(x+1,y) && world.GetPlace<Cell>(x+1,y).Alive)
            n++;

        if(world.isValid(x-1,y-1) && world.GetPlace<Cell>(x-1,y-1).Alive)
            n++;
        if(world.isValid(x+1,y-1) && world.GetPlace<Cell>(x+1,y-1).Alive)
            n++;
        if(world.isValid(x+1,y+1) && world.GetPlace<Cell>(x+1,y+1).Alive)
            n++;
        if(world.isValid(x-1,y+1) && world.GetPlace<Cell>(x-1,y+1).Alive)
            n++;
        
        return n;
    }
    public override void Execute(Cell obj, IWorld world)
    {
        if(!world.GetFlag("run"))
            return;

        int n = CountNeighborhood(world, obj.X, obj.Y);
        if(obj.Alive)
            if(n < 2 || n > 3)
                obj.Alive = false;
        if(!obj.Alive)
            if(n == 3)      
                obj.Alive = true;
            
    }
}