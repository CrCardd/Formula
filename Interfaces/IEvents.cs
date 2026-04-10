using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Formula.Interfaces;

public interface IEvents
{
    public Dictionary<Keys, Action<IUser>> GlobalHotkeys {get;set;}    
    public Action<IWorld, double, double>? MousePaint {get;set;}
    public Action<IWorld, double, double>? MouseDown {get;set;}
    public Action<IWorld, double, double>? MouseUp {get;set;}
    public Action<IWorld, double, double>? MouseMove {get;set;}

}