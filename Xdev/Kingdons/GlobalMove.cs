using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Formula.Objects;
using Formula.Scene;

public class GlobalMove : SceneMap
{
    public GlobalMove()
    {
        Initialize(20,20,1);
        BaseOBject.Size = 30;
    }

    public override void OnMouseDown(MouseArgs e)
    {
        if(e.Button == MouseButtons.Left)
            New(new BaseOBject(e.Position.X, e.Position.Y, 0, Color.Blue, new M()));
        if(e.Button == MouseButtons.Right)
            New(new BaseOBject(e.Position.X, e.Position.Y, 0, Color.Gray));
        if(e.Button == MouseButtons.Middle)
            if(e.TargetObject().Any())
                Destroy(e.TargetObject().First());
    }

    public override void OnKeyDown(KeyEventArgs e)
    {
        if(e.KeyCode == Keys.R) DestroyAllObjects();
    }
}