using System;
using System.Collections.Generic;
using System.Linq;
using Formula.Interfaces;
using Formula.Objects;

namespace Formula.Scene;

partial class Kingdon
{
    private Queue<BaseOBject> toDestroy = [];
    private Queue<BaseOBject> toSpawn = [];
    
    private readonly int w;
    private readonly int h;
    private readonly int? z;
    public new int Width => w;
    public new int Height => h;
    public int? Depth => z;
    
    public static IControl GetInstance(int w, int h, int? z=null, string? label = null)
    {
        if (instance == null)
                lock (_padlock)
                    if (instance == null)
                    {
                        ApplicationConfiguration.Initialize();   
                        instance = new Kingdon(w,h,z,label ?? "Screen");
                    }
        return instance;
    }

    public T New<T>(T obj) where T : BaseOBject
    {
        
        if (obj.X < 0 || obj.X >= Width) throw new Exception("Invalid position to spawn!");
        if (obj.Y < 0 || obj.Y >= Height) throw new Exception("Invalid position to spawn!");

        toSpawn.Enqueue(obj);
        return obj;
    }
    public BaseOBject New(BaseOBject obj) => New<BaseOBject>(obj);
    
    public void Destroy(BaseOBject obj) => toDestroy.Enqueue(obj);
    public IReadOnlyCollection<BaseOBject> GetObjects => Objects.Values.ToList();
    public void DestroyAllObjects()
    {
        Objects = new();
        GridObjects = new();
    }
    public void ApplyAll<T>(Action<T> apply) where T : BaseOBject
    {
        foreach(var o in Objects.Values)
            if(o is T t)
                apply(t);   
    }
    public void ApplyAll(Action<BaseOBject> apply) => ApplyAll<BaseOBject>(apply);

    public void ResetWorld()
    {
        DestroyAllObjects();
        MouseDown = null;
        MouseUp = null;
        MouseMove = null;
        GlobalHotkeys.Clear();
    }
    
}