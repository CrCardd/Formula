using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Objects;

namespace Formula.Scene;

public class RawKingdon(Kingdon engine) : IUser
{
    private Kingdon engine = engine;
    public int Width => engine.Width;
    public int Height => engine.Width;
    public IReadOnlyCollection<BaseOBject> GetObjects => engine.GetObjects;
    public Action<IWorld, MouseArgs>? MouseDown {get;set;} = engine.MouseDown;
    public Action<IWorld, MouseArgs>? MouseUp {get;set;} = engine.MouseUp;
    public Action<IWorld, MouseArgs>? MouseMove {get;set;} = engine.MouseMove;
    public Action<IWorld, MouseArgs>? MousePaint {get;set;} = engine.MousePaint;
    public Dictionary<Keys, Action<IUser>> GlobalHotkeys {get;set;} = engine.GlobalHotkeys;
    public void ApplyAll<T>(Action<T> apply) where T : BaseOBject => engine.ApplyAll(apply);
    public void Destroy(BaseOBject obj) => engine.Destroy(obj);
    public void DestroyAllObjects() => engine.DestroyAllObjects();
    public bool GetFlag(string key) => engine.GetFlag(key);
    public Vector2D? GetRandom4FreeNeighborPlace(double x, double y) => engine.GetRandom4FreeNeighborPlace(x,y);
    public bool isFree(double x, double y) => engine.isFree(x,y);
    public bool isValid(double x, double y) => engine.isValid(x,y);
    public void New(BaseOBject obj) => engine.New(obj);
    public void SetFlag(string key, bool value) => engine.SetFlag(key,value);
    public BaseOBject GetPlace(double x, double y) => engine.GetRealPlace(x,y);
    public T GetPlace<T>(double x, double y) where T : BaseOBject => engine.GetRealPlace<T>(x,y);
    public BaseOBject? GetPlaceOrDefault(double x, double y) => engine.GetRealPlaceOrDefault(x,y);
    public T? GetPlaceOrDefault<T>(double x, double y) where T : BaseOBject => engine.GetRealPlaceOrDefault<T>(x,y);
    public bool IsKeyDown(Keys key) => engine.IsKeyDown(key);
    public Vector2D? GetRandom8FreeNeighborPlace(double x, double y) => engine.GetRandom8FreeNeighborPlace(x,y);
    public void ResetWorld() => engine.ResetWorld();
    public IEnumerable<T> NNeighborCells<T>(double x, double y, int n) where T : BaseOBject => engine.NNeighborCells<T>(x,y,n);
    public IEnumerable<BaseOBject> NNeighborCells(double x, double y, int n) => engine.NNeighborCells<BaseOBject>(x,y,n);
    public IEnumerable<T> NeighborCells<T>(double x, double y, bool diagonal = false) where T : BaseOBject => engine.NeighborCells<T>(x,y,diagonal);
    public IEnumerable<BaseOBject> NeighborCells(double x, double y, bool diagonal = false) => engine.NeighborCells<BaseOBject>(x,y,diagonal);
    public IEnumerable<Vector2D> GetGrid(double x, double y, bool diagonal = false) => engine.GetGrid(x,y,diagonal);
}