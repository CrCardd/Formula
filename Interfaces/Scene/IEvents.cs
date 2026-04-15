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

}