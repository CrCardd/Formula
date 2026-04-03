using System.Drawing;
using Formula.Interfaces;

public class Cell : IObject
{
    private bool alive = false;

    public Cell(int x, int y, IBehavior? behavior = null, string? label = null) : base(x, y, Color.White, behavior, label){}

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