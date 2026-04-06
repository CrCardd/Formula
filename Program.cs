using System;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Scene;

namespace A;

static class Program
{
    // [STAThread]
    // static void Main()
    // {
    //     ApplicationConfiguration.Initialize();

    //     var engine = new Kingdon(50,50);

    //     engine.OnGridClick = (x,y) =>
    //     {
    //         engine.New(new IObject(x,y, behavior: new S()));
    //     };

    //     engine.GlobalHotkeys.Add(Keys.R, engine.DestroyAll);

    //     Application.Run(engine);
    // }   



    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var engine = new Kingdon(50,50);
        for(int i=0; i<50; i++)
            for(int j=0; j<50; j++)
                engine.New(new Cell(i,j,behavior: new C()));

        engine.OnGridClick = (x,y) =>
        {
            var cell = engine.GetPlace<Cell>(x,y);
            cell.Alive = !cell.Alive;
        };

        engine.GlobalHotkeys.Add(Keys.R, () => engine.ApplyAll<Cell>(c => c.Alive = true));
        engine.GlobalHotkeys.Add(Keys.Space, () => engine.SetFlag("run", !engine.GetFlag("run")));

        Application.Run(engine);
    }   
}
