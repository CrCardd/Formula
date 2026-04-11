using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Objects;
using Formula.Scene;
using Microsoft.VisualBasic.Devices;

namespace A;

static class Program
{
    // [STAThread]
    // static void Main()
    // {
    //     var engine = Kingdon.GetInstance(50,50);

    //     engine.OnMousePaint = (iWorld,x,y) => iWorld.New(new IObject(x,y, behavior: new S()));
    //     engine.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAll());

    //     engine.Run();
    // }   



    [STAThread]
    static void Main()
    {
        int w = 150;
        int h = w;

        BaseOBject.Size = 5;
        var engine = Kingdon.GetInstance(w,h,"NOMELEGAL");
        
        
        // SetGlobalMove(engine);
        // SetGameOfLife(engine);
        // SetPaint(engine);
        // engine.GlobalHotkeys.Add(Keys.Z, SetGameOfLife);
        engine.GlobalHotkeys.Add(Keys.X, SetGlobalMove);
        // engine.GlobalHotkeys.Add(Keys.C, SetGravityBalls);
        // engine.GlobalHotkeys.Add(Keys.V, SetSandJoke);
        
        engine.Run();
    }

    public static void SetGameOfLife(IUser engine)
    {
        engine.ResetWorld();
        for(int i=0; i<engine.Width; i++)
            for(int j=0; j<engine.Height; j++)
                engine.New(new Cell(i,j,behavior: new C()));


        engine.MousePaint = (world,e) =>
        {
            var cell = world.GetPlace<Cell>(e.Position.X, e.Position.Y);
            cell.Alive = true;
        };
        engine.GlobalHotkeys.Add(Keys.R, (world) => {world.ApplyAll<Cell>(c => c.Alive = false); world.SetFlag("run", false);});
        engine.GlobalHotkeys.Add(Keys.Q, (world) => world.ApplyAll<Cell>(c => c.Behavior = new C()));
        engine.GlobalHotkeys.Add(Keys.W, (world) => world.ApplyAll<Cell>(c => c.Behavior = new CC()));
        engine.GlobalHotkeys.Add(Keys.Space, (world) => world.SetFlag("run", !world.GetFlag("run")));
    }
    public static void SetGlobalMove(IUser engine)
    {
        engine.ResetWorld();
        engine.MousePaint = 
            (world,e) => 
            {
                if(e.Button == MouseButtons.Left)
                    world.New(new BaseOBject(e.Position.X, e.Position.Y, Color.Blue, new M()));
                if(e.Button == MouseButtons.Right)
                    world.New(new BaseOBject(e.Position.X, e.Position.Y, Color.Gray));
                if(e.Button == MouseButtons.Middle)
                    if(e.TargetObject() is not null)
                        world.Destroy(e.TargetObject()!);

            };
        engine.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAllObjects());   
    }
    public static void SetGravityBalls(IUser engine)
    {
        engine.ResetWorld();
        engine.MousePaint = (world,e) => world.New(new Ball(e.Position.X, e.Position.Y,50,0, behavior: new G()));
        engine.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAllObjects());
    }
    public static void SetSandJoke(IUser engine)
    {
        engine.ResetWorld();
        engine.MousePaint = (world,e) => world.New(new BaseOBject(e.Position.X, e.Position.Y, behavior: new S()));
        engine.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAllObjects());
    }
    public static void SetPaint(IUser engine)
    {
        engine.ResetWorld();
        engine.MousePaint = 
            (world,e) => 
            {
                var places = world.GetGrid(e.Position.X,e.Position.Y);

                // if(e.DistanceMovedSquared() > 0)
                    foreach(var p in places)
                        world.New(new BaseOBject(p.X,p.Y, Color.Black));
                // else
                //     world.New(new BaseOBject(e.Position.X,e.Position.Y, Color.Black));


            };
        engine.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAllObjects());
    }
}
