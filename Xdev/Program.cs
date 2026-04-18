using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Objects;
using Formula.Scene;

namespace A;

static class Program
{
    [STAThread]
    static void Main()
    {
        int w = 150;
        int h = w;

        BaseOBject.Size = 7;
        var engine = Kingdon.GetInstance(w,h,1);
        
        var p1 = engine.New(new(10,12,0,Color.Red,new M()));
        var p2 = engine.New(new(10,10,1,Color.Blue,new MM()));
        engine.GlobalHotkeys.Add(Keys.Space, (world) => world.ApplyAll(o => o.Z=Math.Abs(p1.Z-1)));
        
        engine.MouseDown = (world,e) => world.New(new BaseOBject(e.Position.X, e.Position.Y, behavior: new M()));
        
        engine.Run();
    }

    public static void SetGameOfLife(IUser engine)
    {
        engine.ResetWorld();
        for(int i=0; i<engine.Width; i++)
            for(int j=0; j<engine.Height; j++)
                engine.New(new Cell(i,j,behavior: new C()));

        engine.SetFlag("run", false);
        
        engine.MouseDown = (world,e) => e.TargetObject<Cell>().First().Alive = true;

        engine.GlobalHotkeys.Add(Keys.R, (world) => {world.ApplyAll<Cell>(c => c.Alive = false); world.SetFlag("run", false);});
        engine.GlobalHotkeys.Add(Keys.Q, (world) => world.ApplyAll<Cell>(c => c.Behavior = new C()));
        engine.GlobalHotkeys.Add(Keys.W, (world) => world.ApplyAll<Cell>(c => c.Behavior = new CC()));
        engine.GlobalHotkeys.Add(Keys.Space, (world) => world.SetFlag("run", !world.GetFlag("run")));
    }

    public static void SetGlobalMove(IUser engine)
    {
        engine.ResetWorld();
        engine.MouseDown = 
            (world,e) => 
            {
                if(e.Button == MouseButtons.Left)
                    world.New(new BaseOBject(e.Position.X, e.Position.Y, 0, Color.Blue, new M()));
                if(e.Button == MouseButtons.Right)
                    world.New(new BaseOBject(e.Position.X, e.Position.Y, 0, Color.Gray));
                if(e.Button == MouseButtons.Middle)
                    if(e.TargetObject() is not null)
                        world.Destroy(e.TargetObject().First()!);

            };
        engine.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAllObjects());   
    }

    // public static void SetGravityBalls(IUser engine)
    // {
    //     engine.ResetWorld();
    //     engine.MouseDown = (world,e) => world.New(new Ball(e.Position.X, e.Position.Y,50,0, behavior: new G()));
    //     engine.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAllObjects());
    // }
    public static void SetLig4(IUser engine)
    {
        engine.ResetWorld();
        engine.SetFlag("player", true);
        engine.SetFlag("side", true);

        engine.MouseDown = (world,e) => 
        {
            bool player = world.GetFlag("player");
            Color player1;
            Color player2;
            int z;
            if(engine.GetFlag("side"))
            {

                player1 = Color.Blue;
                player2 = Color.Black;
                z = 0;
            }
            else
            {
                player1 = Color.Black;
                player2 = Color.Red;
                z = 1;
            }
                
            if(player)
                world.New(new(e.Position.X, e.Position.Y,z,player1, new L()));
            else
                world.New(new(e.Position.X, e.Position.Y,z,player2, new L()));
            
            world.SetFlag("player", !world.GetFlag("player"));
        };
        engine.GlobalHotkeys.Add(Keys.Space, (world) => 
            {
                world.ApplyAll(obj =>
                {
                    Color color;
                    bool side = world.GetFlag("side");
                    if(side)
                        if(obj.Color == Color.Blue)
                            color = Color.Black;
                        else
                            color = Color.Red;
                    else
                        if(obj.Color == Color.Red)
                            color = Color.Black;
                        else
                            color = Color.Blue;
                    obj.Color = color;
                });
                world.SetFlag("side", !world.GetFlag("side"));
            }
        );
            
        engine.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAllObjects());
    }
    public static void SetSandJoke(IUser engine)
    {
        engine.ResetWorld();
        engine.MouseDown = (world,e) => world.New(new BaseOBject(e.Position.X, e.Position.Y, behavior: new S()));
        engine.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAllObjects());
    }
    // public static void SetPaint(IUser engine)
    // {
    //     engine.ResetWorld();
    //     engine.MousePaint = 
    //         (world,e) => 
    //         {
    //             var places = world.GetGrid(e.Position.X,e.Position.Y);

    //             // if(e.DistanceMovedSquared() > 0)
    //                 foreach(var p in places)
    //                     world.New(new BaseOBject(p.X,p.Y, Color.Black));
    //             // else
    //             //     world.New(new BaseOBject(e.Position.X,e.Position.Y, Color.Black));


    //         };
    //     engine.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAllObjects());
    // }
}
