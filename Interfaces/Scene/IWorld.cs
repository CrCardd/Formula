using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Formula.Objects;

namespace Formula.Interfaces;

public interface IWorld : IGetPlace
{
    public BaseOBject New(BaseOBject obj);
    public T New<T>(T obj) where T : BaseOBject;
    public void Destroy(BaseOBject obj);

    int Width {get;}
    int Height {get;}
    int? Depth {get;}
    
    IReadOnlyCollection<BaseOBject> GetObjects { get; }
    
    public bool isValid(double x, double y);
    public bool isValid(Vector2D position);

    public void SetFlag(string key, bool value);
    public bool GetFlag(string key);

    public bool IsKeyDown(Keys key);
}