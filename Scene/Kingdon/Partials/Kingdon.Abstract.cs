using System.Windows.Forms;
using Formula.Objects;

namespace Formula.Scene;

public partial class SceneMap
{
    public virtual void OnMouseDown(MouseArgs e){}
    public virtual void OnMouseUp(MouseArgs e){}
    public virtual void OnMouseMove(MouseArgs e){}
    public virtual void OnKeyDown(KeyEventArgs e){}

    public virtual void OnLoop(){}
}