using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Math;
using Formula.Objects;

namespace Formula.Scene;

public class RawKingdon(Kingdon engine) : IWorld
{
    private Kingdon engine = engine;
    public int Width => engine.Width;
    public int Height => engine.Width;
    public IReadOnlyCollection<IObject> GetObjects => engine.GetObjects;
    public Action<IWorld, int, int>? OnGridClick {get;set;} = engine.OnGridClick;
    public Dictionary<Keys, Action> GlobalHotkeys {get;set;} = engine.GlobalHotkeys;
    public void ApplyAll<T>(Action<T> apply) where T : IObject => engine.ApplyAll(apply);
    public void Destroy(IObject obj) => engine.Destroy(obj);
    public void DestroyAll() => engine.DestroyAll();
    public bool GetFlag(string key) => engine.GetFlag(key);
    public Vector2D? GetRandomFreeNeighboorPlace(int x, int y) => engine.GetRandomFreeNeighboorPlace(x,y);
    public bool isFree(int x, int y) => engine.isFree(x,y);
    public bool isValid(int x, int y) => engine.isValid(x,y);
    public void New(IObject obj) => engine.New(obj);
    public void SetFlag(string key, bool value) => engine.SetFlag(key,value);
    public IObject GetPlace(int x, int y) => engine.GetRealPlace(x,y);
    public T GetPlace<T>(int x, int y) where T : IObject => engine.GetRealPlace<T>(x,y);
    public IObject? GetPlaceOrDefault(int x, int y) => engine.GetRealPlaceOrDefault(x,y);
    public T? GetPlaceOrDefault<T>(int x, int y) where T : IObject => engine.GetRealPlaceOrDefault<T>(x,y);
}