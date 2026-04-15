using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Formula.Objects;

namespace Formula.Interfaces;

public interface IEvents
{
    public Dictionary<Keys, Action<IUser>> GlobalHotkeys {get;set;}
    public Action<IWorld, MouseArgs>? MouseDown {get;set;}
    public Action<IWorld, MouseArgs>? MouseUp {get;set;}
    public Action<IWorld, MouseArgs>? MouseMove {get;set;}

    public void ApplyAll(Action<BaseOBject> apply);
    public void ApplyAll<T>(Action<T> apply) where T : BaseOBject;
    public void ResetWorld();
    public void DestroyAllObjects();
}