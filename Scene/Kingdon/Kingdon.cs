using System.Windows.Forms;
using Formula.Interfaces;

namespace Formula.Scene;

public abstract partial class Kingdon : IControl
{
    public void Run() => Application.Run(this);   
}