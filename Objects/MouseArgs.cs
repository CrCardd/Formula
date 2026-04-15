using System;
using System.Collections.Generic;
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
    public IReadOnlyCollection<BaseOBject> TargetObjectOrDefault() => world.GetPlaceOrDefault(Position.X,Position.Y)
    ?? throw new Exception("-> TargetObjectOrDefault <- | There is no objects in this position");
    public IReadOnlyCollection<T> TargetObjectOrDefault<T>() where T : BaseOBject => world.GetPlaceOrDefault<T>(Position.X,Position.Y)
    ?? throw new Exception("-> TargetObjectOrDefault<T> <- | There is no objects in this position");
    public IReadOnlyCollection<BaseOBject> TargetObject() => world.GetPlace(Position.X,Position.Y);
    public IReadOnlyCollection<T> TargetObject<T>() where T : BaseOBject => world.GetPlace<T>(Position.X,Position.Y);
}