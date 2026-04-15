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
    public int? Depth => engine.Depth;
    public IReadOnlyCollection<BaseOBject> GetObjects => engine.GetObjects;
    public Action<IWorld, MouseArgs>? MouseDown {get;set;} = engine.MouseDown;
    public Action<IWorld, MouseArgs>? MouseUp {get;set;} = engine.MouseUp;
    public Action<IWorld, MouseArgs>? MouseMove {get;set;} = engine.MouseMove;
    public Dictionary<Keys, Action<IUser>> GlobalHotkeys {get;set;} = engine.GlobalHotkeys;
    public void ApplyAll<T>(Action<T> apply) where T : BaseOBject => engine.ApplyAll<T>(apply);
    public void ApplyAll(Action<BaseOBject> apply) => engine.ApplyAll(apply);
    public void Destroy(BaseOBject obj) => engine.Destroy(obj);
    public void DestroyAllObjects() => engine.DestroyAllObjects();
    public bool GetFlag(string key) => engine.GetFlag(key);
    public bool isValid(double x, double y) => engine.isValid(x,y);
    public void New(BaseOBject obj) => engine.New(obj);
    public void SetFlag(string key, bool value) => engine.SetFlag(key,value);
    public IReadOnlyCollection<BaseOBject> GetPlace(double x, double y) => engine.GetRealPlace(x,y);
    public IReadOnlyCollection<T> GetPlace<T>(double x, double y) where T : BaseOBject => engine.GetRealPlace<T>(x,y);
    public IReadOnlyCollection<BaseOBject>? GetPlaceOrDefault(double x, double y) => engine.GetRealPlaceOrDefault(x,y);
    public IReadOnlyCollection<T>? GetPlaceOrDefault<T>(double x, double y) where T : BaseOBject => engine.GetRealPlaceOrDefault<T>(x,y);
    public bool IsKeyDown(Keys key) => engine.IsKeyDown(key);
    public void ResetWorld() => engine.ResetWorld();
    public IReadOnlyCollection<T> RadiusAreaObjects<T>(double x, double y, int n) where T : BaseOBject => engine.RadiusAreaObjects<T>(x,y,n);
    public IReadOnlyCollection<BaseOBject> RadiusAreaObjects(double x, double y, int n) => engine.RadiusAreaObjects<BaseOBject>(x,y,n);
    public IReadOnlyCollection<T> NeighborObjects<T>(double x, double y, bool diagonal = false) where T : BaseOBject => engine.NeighborObjects<T>(x,y,diagonal);
    public IReadOnlyCollection<BaseOBject> NeighborObjects(double x, double y, bool diagonal = false) => engine.NeighborObjects<BaseOBject>(x,y,diagonal);
    public Dictionary<Vector2D,IReadOnlyCollection<T>> GetGrid<T>(double x, double y, bool diagonal = false) where T : BaseOBject 
    => engine.GetGrid<T>(x,y,diagonal) ;
    public Dictionary<Vector2D,IReadOnlyCollection<BaseOBject>> GetGrid(double x, double y, bool diagonal = false) => engine.GetGrid(x,y,diagonal);
}