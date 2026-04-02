using Formula.Interfaces;
using Formula.Scene;

namespace Formula;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        
        IObject.Size = 10;
        var W = new Kingdon(40,40);
        W.New(new IObject(20,20,behavior: new S(Color.Black)));
        W.New(new IObject(21,21,behavior: new S(Color.Purple)));

        Application.Run(W);
    }
}