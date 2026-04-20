using System.Windows.Forms;
using Formula.Interfaces;

namespace Formula.Scene;

public abstract partial class Kingdon : IControl
{
    public void Run()
    {
        var nav = Navigation.Get();
        nav.Push(this);
        Application.Run(new MainForm(nav));   
    } 
}