using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Math;
using Formula.Objects;

namespace Formula.Scene;

public class RawKingdon(Kingdon engine) : IUser
{
    private Kingdon engine = engine;
    public int Width => engine.Width;
    public int Height => engine.Width;
    public IReadOnlyCollection<IObject> GetObjects => engine.GetObjects;
    public Action<IWorld, double, double>? MouseDown {get;set;} = engine.MouseDown;
    public Action<IWorld, double, double>? MouseUp {get;set;} = engine.MouseUp;
    public Action<IWorld, double, double>? MouseMove {get;set;} = engine.MouseMove;
    public Action<IWorld, double, double>? MousePaint {get;set;} = engine.MousePaint;
    public Dictionary<Keys, Action<IUser>> GlobalHotkeys {get;set;} = engine.GlobalHotkeys;
    public void ApplyAll<T>(Action<T> apply) where T : IObject => engine.ApplyAll(apply);
    public void Destroy(IObject obj) => engine.Destroy(obj);
    public void DestroyAllObjects() => engine.DestroyAllObjects();
    public bool GetFlag(string key) => engine.GetFlag(key);
    public Vector2D? GetRandom4FreeNeighboorPlace(double x, double y) => engine.GetRandom4FreeNeighboorPlace(x,y);
    public bool isFree(double x, double y) => engine.isFree(x,y);
    public bool isValid(double x, double y) => engine.isValid(x,y);
    public void New(IObject obj) => engine.New(obj);
    public void SetFlag(string key, bool value) => engine.SetFlag(key,value);
    public IObject GetPlace(double x, double y) => engine.GetRealPlace(x,y);
    public T GetPlace<T>(double x, double y) where T : IObject => engine.GetRealPlace<T>(x,y);
    public IObject? GetPlaceOrDefault(double x, double y) => engine.GetRealPlaceOrDefault(x,y);
    public T? GetPlaceOrDefault<T>(double x, double y) where T : IObject => engine.GetRealPlaceOrDefault<T>(x,y);
    public bool IsKeyDown(Keys key) => engine.IsKeyDown(key);
    public Vector2D? GetRandom8FreeNeighboorPlace(double x, double y) => engine.GetRandom8FreeNeighboorPlace(x,y);
    public void ResetWorld() => engine.ResetWorld();
}