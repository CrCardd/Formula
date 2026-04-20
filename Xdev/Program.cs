using System;
using Formula.Scene;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var map = new GlobalMove();

        map.Run();
    }
}
