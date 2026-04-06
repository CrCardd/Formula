using System;
using System.Collections.Generic;
using Formula.Math;

namespace Formula.Interfaces;

public interface IWorld
{
    public void New(IObject obj);
    public void Destroy(IObject obj);

    int Width {get;}
    int Height {get;}
    IReadOnlyCollection<IObject> GetObjects { get; }
    
    public bool isFree(int x, int y);
    public bool isValid(int x, int y);
    public IObject GetPlace(int x, int y);
    public T GetPlace<T>(int x, int y) where T : IObject;
    public IObject? GetPlaceOrDefault(int x, int y);
    public T? GetPlaceOrDefault<T>(int x, int y) where T : IObject;
    public Vector2D? GetRandomFreeNeighboorPlace(int x, int y);

    public void DestroyAll();
    public void ApplyAll<T>(Action<T> apply) where T : IObject;

    public void SetFlag(string key, bool value);
    public bool GetFlag(string key);

}