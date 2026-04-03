using System;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Scene;

namespace A;

static class Program
{
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

        engine.GlobalHotkeys.Add(Keys.R, () => engine.ApplyAll<Cell>(c => c.Alive = false));

        Application.Run(engine);
    }   
}
