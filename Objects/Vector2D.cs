using System;

namespace Formula.Objects;

public struct Vector2D(double x, double y)
{
    public double X = x;
    public double Y = y;

    public static Vector2D operator *(Vector2D v, double scalar)
        => new Vector2D(v.X * scalar, v.Y * scalar);
    public static Vector2D operator +(Vector2D v, Vector2D b)
        => new Vector2D(v.X+b.X, v.Y+b.Y);
    public static Vector2D operator -(Vector2D v, Vector2D b)
        => new Vector2D(v.X-b.X, v.Y-b.Y);
    
    public static bool operator ==(Vector2D a, Vector2D b)
        => a.Y == b.Y && a.X == b.X;

    public static bool operator !=(Vector2D a, Vector2D b)
        => !(a == b);
    
    public override bool Equals(object? obj) 
    => obj is Vector2D other && this == other;
    public override int GetHashCode() 
        => HashCode.Combine(X, Y);

    public static implicit operator Vector2D((double,double) source)
        => new(source.Item1, source.Item2);
    
}