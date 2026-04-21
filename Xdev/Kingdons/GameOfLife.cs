using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Formula.Objects;
using Formula.Scene;

public class GameOfLife : SceneMap
{
    public GameOfLife()
    {
        Initialize(100,100);
        BaseOBject.Size = 5;

        SetFlag("run", false);

        
        for(int i=0; i<Width; i++)
            for(int j=0; j<Height; j++)
                New(new Cell(i,j,behavior: new C()));
    }

    public override void OnMouseDown(MouseArgs e)
    {
        e.TargetObject<Cell>().First().Alive = !e.TargetObject<Cell>().First().Alive;
    }

    public override void OnKeyDown(KeyEventArgs e)
    {
        if(e.KeyCode == Keys.Q)
            ApplyAll<Cell>(c => c.Behavior = new C());
        if(e.KeyCode == Keys.W)
            ApplyAll<Cell>(c => c.Behavior = new CC());


        if(e.KeyCode == Keys.P)
            Nav.Push(new GameOfLife());
        if(e.KeyCode == Keys.O)
            Nav.Pop();
            
        if(e.KeyCode == Keys.Space)
            SetFlag("run", !GetFlag("run"));
    }
}