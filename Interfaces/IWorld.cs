using Formula.Math;

namespace Formula.Interfaces;

public interface IWorld
{
    public bool isFree(int x, int y);
    public IObject? GetPlace(int x, int y);
    public void New(IObject obj);
    public void Destroy(IObject obj);
    IReadOnlyCollection<IObject> GetObjects { get; }
    int Width {get;}
    int Height {get;}

    public Vector2D? GetRandomFreeNeighboorPlace(IWorld world, int x, int y);
}