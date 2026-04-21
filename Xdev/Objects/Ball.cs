using System.Drawing;
using Formula.Objects;

public class Ball : BaseOBject
{
    public double SpeedX;
    public double SpeedY;

    public Ball(double x, double y,double speedX, double speedY, IBehavior behavior, string? label = null) : base(x, y, 0, Color.Green, behavior, label)
    {
        SpeedX = speedX;
        SpeedY = speedY;
    }
}