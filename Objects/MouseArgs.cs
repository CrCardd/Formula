using System;
using System.Windows.Forms;
using Formula.Interfaces;

namespace Formula.Objects;

public class MouseArgs(MouseEventArgs e, IWorld world, Vector2D lastPosition)
{
    public bool IsKeyDown(Keys key) => (Control.ModifierKeys & key) == key;
    public readonly MouseButtons Button = e.Button;
    public readonly int Clicks = e.Clicks;
    public readonly int Delta = e.Delta;
    public readonly Vector2D Position = new(e.X / BaseOBject.Size, e.Y / BaseOBject.Size);
    public readonly Vector2D LastPosition = lastPosition;
    public bool IsInsideGrid => world.isValid(Position.X, Position.Y);
    public Vector2D ScreenPosition => new(e.X,e.Y);
    public Vector2D DistanceMovedVector() => LastPosition - Position;
    public double DistanceMovedSquared()
    {
        var diff = LastPosition - Position;
        return diff.X * diff.X + diff.Y * diff.Y;
    }
    public BaseOBject? TargetObject() => world.GetPlaceOrDefault(Position.X,Position.Y);
    public T? TargetObject<T>() where T : BaseOBject => world.GetPlaceOrDefault<T>(Position.X,Position.Y);
}