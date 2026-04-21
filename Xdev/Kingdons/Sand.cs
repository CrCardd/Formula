using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Objects;
using Formula.Scene;

public class Sand : SceneMap
{
    public Sand()
    {
        Initialize(100,40,1);
        BaseOBject.Size = 5;
    }

    public override void OnKeyDown(KeyEventArgs e)
    {
        if(e.KeyCode == Keys.R) DestroyAllObjects();
    }

    public override void OnMouseDown(MouseArgs e)
    {
        New(new BaseOBject(e.Position.X, e.Position.Y, behavior: new S()));
    }
}