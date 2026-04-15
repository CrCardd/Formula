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
    int? Depth {get;}
    
    IReadOnlyCollection<BaseOBject> GetObjects { get; }
    
    public bool isValid(double x, double y);
    public IReadOnlyCollection<BaseOBject> GetPlace(double x, double y);
    public IReadOnlyCollection<T> GetPlace<T>(double x, double y) where T : BaseOBject;
    public IReadOnlyCollection<BaseOBject>? GetPlaceOrDefault(double x, double y);
    public IReadOnlyCollection<T>? GetPlaceOrDefault<T>(double x, double y) where T : BaseOBject;
    public IReadOnlyCollection<T> RadiusAreaObjects<T>(double x, double y, int n=1) where T : BaseOBject;
    public IReadOnlyCollection<BaseOBject> RadiusAreaObjects(double x, double y, int n);
    public IReadOnlyCollection<T> NeighborObjects<T>(double x, double y, bool diagonal=false) where T : BaseOBject;
    public IReadOnlyCollection<BaseOBject> NeighborObjects(double x, double y, bool diagonal=false);
    public Dictionary<Vector2D, IReadOnlyCollection<T>> GetGrid<T>(double x, double y, bool diagonal=false) where T : BaseOBject;
    public Dictionary<Vector2D, IReadOnlyCollection<BaseOBject>> GetGrid(double x, double y, bool diagonal=false);

    public void SetFlag(string key, bool value);
    public bool GetFlag(string key);

    public bool IsKeyDown(Keys key);
}