using System.Drawing;
using Formula.Objects;

public class Cell : BaseOBject
{
    private bool alive = false;

    public Cell(int x, int y, IBehavior? behavior = null, string? label = null, bool alive = false) : base(x, y, 0, Color.White, behavior, label)
    {
        Alive = alive;
    }

    public bool Alive {
        get => alive;
        set
        {
            if(value)
                Color = Color.Black;
            else
                Color = Color.White;
            alive = value;
        }
    }
}