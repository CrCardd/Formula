using Formula.Objects;

namespace Formula.Scene;

public partial class Kingdon
{
    public BaseOBject GetPlace(double x, double y) => GridObjects[((int)x, (int)y)].Shadow!;
    public BaseOBject? GetPlace(Vector2D position)
    {
        if (GridObjects.TryGetValue(((int)position.X, (int)position.Y), out var obj)) return obj.Shadow;
        return null;
    }
    public T GetPlace<T>(double x, double y) where T : BaseOBject => (GridObjects[((int)x, (int)y)].Shadow as T)!;

    public BaseOBject? GetPlaceOrDefault(double x, double y) 
    {
        if (GridObjects.TryGetValue(((int)x, (int)y), out var obj)) return obj.Shadow;
        return null;
    }
    public T? GetPlaceOrDefault<T>(double x, double y) where T : BaseOBject
    {
        if (GridObjects.TryGetValue(((int)x, (int)y), out var obj)) return obj.Shadow as T;
        return null;
    }

}