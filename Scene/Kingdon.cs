using System.Windows.Forms;
using Formula.Interfaces;
using System.ComponentModel;

namespace Formula.Scene;

public partial class Kingdon : Form, IScene
{
    public void Run() => Application.Run(this);   
}