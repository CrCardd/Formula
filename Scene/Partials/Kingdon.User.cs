using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Math;

namespace Formula.Scene;

partial class Kingdon
{
    private Queue<IObject> toDestroy = [];
    private Queue<IObject> toSpawn = [];
    
    private readonly int w;
    private readonly int h;
    public new int Width => w;
    public new int Height => h;

    public Kingdon(int w, int h)
    {
        InitializeComponent();
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.DoubleBuffered = true;

        this.w = w;
        this.h = h;
        this.ClientSize = new Size(IObject.Size * w,IObject.Size * h);
    
        Timer t = new();
        t.Interval = 16; // ~60 FPS
        t.Tick += (s, e) => Loop(s,e);
        
        t.Start();

    }

    #region Base functions
    public void New(IObject obj){
        
        if (obj.X < 0 || obj.X >= Width) return;
        if (obj.Y < 0 || obj.Y >= Height) return;

        toSpawn.Enqueue(obj);
    }
    public void Destroy(IObject obj) => toDestroy.Enqueue(obj);
    public IReadOnlyCollection<IObject> GetObjects => Objects.Values.ToList();
    
    public void DestroyAll()
    {
        Objects = new();
        GridObjects = new();
    }

    public void ApplyAll<T>(Action<T> apply) where T : IObject
    {
        foreach(var o in Objects.Values)
            if(o is T t)
                apply(t);   
    }


    #endregion
    #region Util functions
    public bool isFree(int x, int y) => GetPlaceOrDefault(x,y) == null;
    public bool isValid(int x, int y) => x>=0 && x<50 && y>=0 && y < Height;
    public IObject GetPlace(int x, int y) => GridObjects[(x, y)].Shadow!;
    public T GetPlace<T>(int x, int y) where T : IObject => (GridObjects[(x, y)].Shadow as T)!;
    public IObject? GetPlaceOrDefault(int x, int y) 
    {
        if (GridObjects.TryGetValue((x, y), out var obj)) return obj.Shadow;
        return null;
    }
    public T? GetPlaceOrDefault<T>(int x, int y) where T : IObject
    {
        if (GridObjects.TryGetValue((x, y), out var obj)) return obj.Shadow as T;
        return null;
    }
    public IObject? GetPlace(Vector2D position)
    {
        if (GridObjects.TryGetValue((position.X, position.Y), out var obj)) return obj.Shadow;
        return null;
    }
    public Vector2D? GetRandomFreeNeighboorPlace(int x, int y)
    {
        List<Tuple<int, int>> offsets = [
                new(-1, 0), new(1, 0), new(0, -1), new(0, 1)
        ];
        return GetRandomFreeNeighboorPlace(offsets, x, y);
    }
    public Vector2D? GetRandomFreeNeighboorPlace(List<Tuple<int,int>> offsets, int x, int y)
    {
        int lenght = offsets.Count;
        if(lenght <= 0)
            return null;

        int r = Random.Shared.Next(0,lenght);
        
        if(GetPlaceOrDefault(x+offsets[r].Item1, y+offsets[r].Item2) == null)
            return new(x+offsets[r].Item1, y+offsets[r].Item2);

        offsets.RemoveAt(r);
        return GetRandomFreeNeighboorPlace(offsets, x, y);
    }
    
    #endregion
}