using System.Windows.Forms;
using Formula.Interfaces;

namespace Formula.Scene;

public partial class SceneMap : IControl
{
    protected INavigation Nav = Navigation.Get();
    public void Run()
    {
        var nav = Navigation.Get();
        var MainForm = new MainForm(nav);
        MainForm.InitializeComponent(this.Width, this.Height);
        nav.Push(this);
        Application.Run(MainForm);
    }
}