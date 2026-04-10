using System;
using System.Drawing;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Objects;
using Formula.Scene;

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
        
        IObject.Size = 5;
        var engine = Kingdon.GetInstance(w,h,"NOMELEGAL");
        
        AddGameOfLife(engine, Keys.Z);
        AddGlobalMove(engine, Keys.X);
        AddGravityBalls(engine, Keys.C);
        AddSandJoke(engine, Keys.V);
        
        engine.Run();
    }

    public static void AddGameOfLife(IControl engine, Keys key)
    {
        engine.GlobalHotkeys.Add(key, 
            (world) =>
            {
                world.ResetWorld();
                for(int i=0; i<world.Width; i++)
                    for(int j=0; j<world.Height; j++)
                        world.New(new Cell(i,j,behavior: new C()));


                world.MousePaint = (world,x,y) =>
                {
                    var cell = world.GetPlace<Cell>(x,y);
                    cell.Alive = true;
                };
                world.GlobalHotkeys.Add(Keys.R, (world) => {world.ApplyAll<Cell>(c => c.Alive = false); world.SetFlag("run", false);});
                world.GlobalHotkeys.Add(Keys.Q, (world) => world.ApplyAll<Cell>(c => c.Behavior = new C()));
                world.GlobalHotkeys.Add(Keys.W, (world) => world.ApplyAll<Cell>(c => c.Behavior = new CC()));
                world.GlobalHotkeys.Add(Keys.Space, (world) => world.SetFlag("run", !world.GetFlag("run")));
            }
        );
    }
    public static void AddGlobalMove(IControl engine, Keys key)
    {
        engine.GlobalHotkeys.Add(key,
            (world) =>
            {
                world.ResetWorld();
                engine.New(new IObject(30,30));
                engine.MousePaint = (world,x,y) => world.New(new IObject(x,y, behavior: new M()));
                engine.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAllObjects());
            }
        );
    }
    public static void AddGravityBalls(IControl engine, Keys key)
    {
        engine.GlobalHotkeys.Add(key,
            (world) =>
            {
                world.ResetWorld();
                engine.MousePaint = (world,x,y) => world.New(new Ball(x,y,50,0, behavior: new G()));
                engine.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAllObjects());
            }
        );
    }
    public static void AddSandJoke(IControl engine, Keys key)
    {
        engine.GlobalHotkeys.Add(key,
            (world) =>
            {
                world.MousePaint = (iWorld,x,y) => iWorld.New(new IObject(x,y, behavior: new S()));
                world.GlobalHotkeys.Add(Keys.R, (world) => world.DestroyAllObjects());
            }
        );
    }
}
