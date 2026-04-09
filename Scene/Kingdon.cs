using System.Windows.Forms;
using Formula.Interfaces;
using System.ComponentModel;

namespace Formula.Scene;

public partial class Kingdon : Form, IScene
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Form scene {get;set;} = Instance;
}