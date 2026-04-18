using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Formula.Interfaces;

namespace Formula.Objects;

public class MouseArgs(MouseEventArgs e, IGetPlace world, MouseArgs? lastMouse)
{
    public bool IsKeyDown(Keys key) => (Control.ModifierKeys & key) == key;
    public readonly MouseButtons Button = e.Button;
    public readonly int Clicks = e.Clicks;
    public readonly int Delta = e.Delta;
    public readonly Vector2D Position = new(e.X / BaseOBject.Size, e.Y / BaseOBject.Size);
    public readonly Vector2D LastPosition = lastMouse?.Position ?? (-1,-1);
    public bool IsInsideGrid => world.isValid(Position.X, Position.Y);
    public Vector2D ScreenPosition => new(e.X,e.Y);
    public Vector2D DistanceMovedVector() => LastPosition - Position;
    public double DistanceMovedSquared()
    {
        var diff = LastPosition - Position;
        return diff.X * diff.X + diff.Y * diff.Y;
    }
    public IEnumerable<BaseOBject> TargetObjectOrDefault() => world.GetPlaceOrDefault(Position.X,Position.Y)
    ?? throw new Exception("-> TargetObjectOrDefault <- | There is no objects in this position");
    public IEnumerable<T> TargetObjectOrDefault<T>() where T : BaseOBject => world.GetPlaceOrDefault<T>(Position.X,Position.Y)
    ?? throw new Exception("-> TargetObjectOrDefault<T> <- | There is no objects in this position");
    public IEnumerable<BaseOBject> TargetObject() => world.GetPlace(Position.X,Position.Y);
    public IEnumerable<T> TargetObject<T>() where T : BaseOBject => world.GetPlace<T>(Position.X,Position.Y);
}