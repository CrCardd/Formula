using System;

namespace Formula.Math;

public struct Vector2D(int x, int y)
{
    public int X = x;
    public int Y = y;

    public static Vector2D operator *(Vector2D v, int scalar)
        => new Vector2D(v.X * scalar, v.Y * scalar);
    
    public static bool operator ==(Vector2D a, Vector2D b)
        => a.Y == b.Y && a.X == b.X;

    public static bool operator !=(Vector2D a, Vector2D b)
        => !(a == b);
    
    public override bool Equals(object? obj) 
    => obj is Vector2D other && this == other;
    public override int GetHashCode() 
        => HashCode.Combine(X, Y);
}