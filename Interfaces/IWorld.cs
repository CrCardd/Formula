using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Formula.Math;
using Formula.Objects;

namespace Formula.Interfaces;

public interface IWorld
{
    public void New(IObject obj);
    public void Destroy(IObject obj);

    int Width {get;}
    int Height {get;}
    IReadOnlyCollection<IObject> GetObjects { get; }
    
    public bool isFree(double x, double y);
    public bool isValid(double x, double y);
    public IObject GetPlace(double x, double y);
    public T GetPlace<T>(double x, double y) where T : IObject;
    public IObject? GetPlaceOrDefault(double x, double y);
    public T? GetPlaceOrDefault<T>(double x, double y) where T : IObject;
    public Vector2D? GetRandom4FreeNeighboorPlace(double x, double y);
    public Vector2D? GetRandom8FreeNeighboorPlace(double x, double y);

    public void DestroyAll();
    public void ApplyAll<T>(Action<T> apply) where T : IObject;

    public void SetFlag(string key, bool value);
    public bool GetFlag(string key);

    public bool IsKeyDown(Keys key);
}