using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Formula.Objects;

namespace Formula.Interfaces;

public interface IWorld : IInteract
{

    int Width {get;set;}
    int Height {get;set;}
    int? Depth {get;set;}
    string Text {get;set;}
    
    IReadOnlyCollection<BaseOBject> GetObjects { get; }
    
    
    public bool IsKeyDown(Keys key);


    public void SetFlag(string key, bool value);
    public bool GetFlag(string key);
}