using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Math;
using Formula.Objects;

namespace Formula.Scene;

partial class Kingdon
{
    private Queue<IObject> toDestroy = [];
    private Queue<IObject> toSpawn = [];
    
    private readonly int w;
    private readonly int h;
    public new int Width => w;
    public new int Height => h;
    
    public static Kingdon GetInstance(int w, int h, string? label = null)
    {
        if (instance == null)
                lock (_padlock)
                    if (instance == null)
                    {
                        ApplicationConfiguration.Initialize();   
                        instance = new Kingdon(w,h, label ?? "Screen");
                    }
        return instance;
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

    public bool isFree(double x, double y) => GetPlaceOrDefault(x,y) == null;
    public bool isValid(double x, double y) => x>=0 && x<Width && y>=0 && y < Height;
    public IObject GetPlace(double x, double y) => GridObjects[((int)x, (int)y)].Shadow!;
    public T GetPlace<T>(double x, double y) where T : IObject => (GridObjects[((int)x, (int)y)].Shadow as T)!;
    public IObject? GetPlaceOrDefault(double x, double y) 
    {
        if (GridObjects.TryGetValue(((int)x, (int)y), out var obj)) return obj.Shadow;
        return null;
    }
    public T? GetPlaceOrDefault<T>(double x, double y) where T : IObject
    {
        if (GridObjects.TryGetValue(((int)x, (int)y), out var obj)) return obj.Shadow as T;
        return null;
    }
    public IObject? GetPlace(Vector2D position)
    {
        if (GridObjects.TryGetValue(((int)position.X, (int)position.Y), out var obj)) return obj.Shadow;
        return null;
    }
    public Vector2D? GetRandom4FreeNeighboorPlace(double x, double y)
    {
        List<Tuple<int, int>> offsets = [
                new(-1, 0), new(1, 0), new(0, -1), new(0, 1)
        ];
        return GetRandom4FreeNeighboorPlace(offsets, x, y);
    }
    public Vector2D? GetRandom4FreeNeighboorPlace(List<Tuple<int,int>> offsets, double x, double y)
    {
        int lenght = offsets.Count;
        if(lenght <= 0)
            return null;

        int r = Random.Shared.Next(0,lenght);
        
        if(GetPlaceOrDefault(x+offsets[r].Item1, y+offsets[r].Item2) == null)
            return new(x+offsets[r].Item1, y+offsets[r].Item2);

        offsets.RemoveAt(r);
        return GetRandom4FreeNeighboorPlace(offsets, x, y);
    }


    public Vector2D? GetRandom8FreeNeighboorPlace(double x, double y)
    {
        List<Tuple<int, int>> offsets = [
                new(-1, 0), new(1, 0), new(0, -1), new(0, 1),
                new(-1, -1), new(1, 1), new(1, -1), new(-1, 1),
        ];
        return GetRandom8FreeNeighboorPlace(offsets, x, y);
    }
    public Vector2D? GetRandom8FreeNeighboorPlace(List<Tuple<int,int>> offsets, double x, double y)
    {
        int lenght = offsets.Count;
        if(lenght <= 0)
            return null;

        int r = Random.Shared.Next(0,lenght);
        
        if(GetPlaceOrDefault(x+offsets[r].Item1, y+offsets[r].Item2) == null)
            return new(x+offsets[r].Item1, y+offsets[r].Item2);

        offsets.RemoveAt(r);
        return GetRandom8FreeNeighboorPlace(offsets, x, y);
    }
    
    #endregion
}