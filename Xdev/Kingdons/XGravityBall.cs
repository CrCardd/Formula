using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Objects;
using Formula.Scene;

public class GravityBall : SceneMap
{
    public GravityBall(INavigation nav)
    {
        Initialize(100,100);
        BaseOBject.Size = 5;
    }
    public override void OnMouseDown(MouseArgs e)
    {
        // New(new Ball(e.Position.X, e.Position.Y,50,0, behavior: new G()));
    }
    public override void OnKeyDown(KeyEventArgs e)
    {
        if(e.KeyCode == Keys.R) DestroyAllObjects();
    }
}