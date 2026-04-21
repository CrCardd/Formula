using System;
using System.Collections.Generic;
using System.Linq;
using Formula.Interfaces;
using Formula.Objects;

namespace Formula.Scene;

partial class SceneMap
{
    public Queue<BaseOBject> toDestroy = [];
    public Queue<BaseOBject> toSpawn = [];
    

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
}