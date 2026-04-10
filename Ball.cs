using System.Drawing;
using Formula.Objects;

public class Ball : IObject
{
    public double SpeedX;
    public double SpeedY;

    public Ball(int x, int y,double speedX, double speedY, IBehavior behavior, string? label = null) : base(x, y, Color.Green, behavior, label)
    {
        SpeedX = speedX;
        SpeedY = speedY;
    }
}