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

    // [STAThread]
    // static void Main()
    // {
    //     IObject.Size = 10;
    //     var engine = Kingdon.GetInstance(60,60);
    //     engine.New(new IObject(30,30));
    //     engine.OnGridClick = (world,x,y) => world.New(new Ball(x,y,50,0, behavior: new M()));
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
        for(int i=0; i<w; i++)
            for(int j=0; j<h; j++)
                engine.New(new Cell(i,j,behavior: new C()));


        engine.OnMousePaint = (world,x,y) =>
        {
            var cell = world.GetPlace<Cell>(x,y);
            cell.Alive = true;
        };

        engine.GlobalHotkeys.Add(Keys.R, (world) => {world.ApplyAll<Cell>(c => c.Alive = false); world.SetFlag("run", false);});

        engine.GlobalHotkeys.Add(Keys.Q, (world) => world.ApplyAll<Cell>(c => c.Behavior = new C()));
        engine.GlobalHotkeys.Add(Keys.W, (world) => world.ApplyAll<Cell>(c => c.Behavior = new CC()));
        engine.GlobalHotkeys.Add(Keys.Space, (world) => world.SetFlag("run", !engine.GetFlag("run")));
        
        engine.Run();
    }   
}
