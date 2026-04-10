using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Formula.Interfaces;

public interface IFunction
{
    public Action<IWorld, int, int>? OnMousePaint {get;set;}
    public Dictionary<Keys, Action<IWorld>> GlobalHotkeys {get;set;}
    public void Run();
}