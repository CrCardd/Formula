using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Formula.Objects;

namespace Formula.Interfaces;

public interface IWorld
{
    public void New(BaseOBject obj);
    public void Destroy(BaseOBject obj);

    int Width {get;}
    int Height {get;}
    IReadOnlyCollection<BaseOBject> GetObjects { get; }
    
    public bool isFree(double x, double y);
    public bool isValid(double x, double y);
    public BaseOBject GetPlace(double x, double y);
    public T GetPlace<T>(double x, double y) where T : BaseOBject;
    public BaseOBject? GetPlaceOrDefault(double x, double y);
    public T? GetPlaceOrDefault<T>(double x, double y) where T : BaseOBject;
    public Vector2D? GetRandom4FreeNeighborPlace(double x, double y);
    public Vector2D? GetRandom8FreeNeighborPlace(double x, double y);
    public IEnumerable<T> NNeighborCells<T>(double x, double y, int n=1) where T : BaseOBject;
    public IEnumerable<BaseOBject> NNeighborCells(double x, double y, int n);
    public IEnumerable<T> NeighborCells<T>(double x, double y, bool diagonal=false) where T : BaseOBject;
    public IEnumerable<BaseOBject> NeighborCells(double x, double y, bool diagonal=false);
    public IEnumerable<Vector2D> GetGrid(double x, double y, bool diagonal=false);

    public void ResetWorld();

    public void DestroyAllObjects();
    public void ApplyAll<T>(Action<T> apply) where T : BaseOBject;

    public void SetFlag(string key, bool value);
    public bool GetFlag(string key);

    public bool IsKeyDown(Keys key);
}