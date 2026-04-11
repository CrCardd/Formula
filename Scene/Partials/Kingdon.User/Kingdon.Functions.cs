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
    public new int Width => w;
    public new int Height => h;
    
    public static IControl GetInstance(int w, int h, string? label = null)
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

    public void New(BaseOBject obj){
        
        if (obj.X < 0 || obj.X >= Width) return;
        if (obj.Y < 0 || obj.Y >= Height) return;

        toSpawn.Enqueue(obj);
    }
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

    public void ResetWorld()
    {
        DestroyAllObjects();
        MouseDown = null;
        MouseUp = null;
        MousePaint = null;
        MousePaint = null;
        GlobalHotkeys.Clear();
    }
    
}