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

    int Width {get;set;}
    int Height {get;set;}
    int? Depth {get;set;}
    string Text {get;set;}
    
    IReadOnlyCollection<BaseOBject> GetObjects { get; }
    

    public void SetFlag(string key, bool value);
    public bool GetFlag(string key);

    public bool IsKeyDown(Keys key);
}