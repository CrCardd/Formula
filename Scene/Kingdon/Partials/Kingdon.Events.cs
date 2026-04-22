using Formula.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Formula.Scene;

partial class SceneMap
{
    private MouseArgs? MouseArgs = null;    

    public void BaseMouseClick(MouseEventArgs e)
    {
        var ma = BaseMouseAction(OnMouseDown, e);
        if(ma.TargetObject().Any())
            foreach(var obj in ma.TargetObject())
                obj.Behavior?.OnMouseClick(obj, ma);
    } 
    public void BaseMouseDown(MouseEventArgs e)
    {
        var ma = BaseMouseAction(OnMouseDown, e);
        if(ma.TargetObject().Any())
            foreach(var obj in ma.TargetObject())
                obj.Behavior?.OnMouseDown(obj, ma);
    } 
    public void BaseMouseUp(MouseEventArgs e)
    {
        var ma = BaseMouseAction(OnMouseUp, e);
        if(ma.TargetObject().Any())
            foreach(var obj in ma.TargetObject())
                obj.Behavior?.OnMouseUp(obj, ma);
    } 
    public void BaseMouseMove(MouseEventArgs e)
    {
        var ma = BaseMouseAction(OnMouseMove, e);
        if(ma.TargetObject().Any())
            foreach(var obj in ma.TargetObject())
                obj.Behavior?.OnMouseHover(obj, ma);
    } 

    private MouseArgs BaseMouseAction(Action<MouseArgs>? action, MouseEventArgs e)
    {
        this.getPlace = this.getRealPlace;
        var ma = new MouseArgs(e,this.getRealPlace!, MouseArgs);
        action?.Invoke(ma);
        this.getPlace = this.getShadowPlace;
        
        if(MouseArgs?.Position != ma.Position)
            OnReload?.Invoke();

        MouseArgs = ma;
        return ma;
    }

    private Dictionary<string, bool> Flags = [];
    public void SetFlag(string key, bool value)
    {
        if(Flags.Keys.Contains(key))
            Flags[key] = value;
        else
            Flags.Add(key,value);
        
    }
    public bool GetFlag(string key)
    {
        if(Flags.TryGetValue(key, out bool v))
            return v;
        return false;
    }


    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);
    private readonly bool[] snapshotKeys = new bool[256];
    private void CaptureInputSnapshot()
    {
        for (int i = 0; i < 256; i++)
            snapshotKeys[i] = (GetAsyncKeyState(i) & 0x8000) != 0;
    }
    
    public bool IsKeyDown(Keys key)
    {
        int k = (int)key;
        if (k < 0 || k > 255) return false;
        return snapshotKeys[k];
    }

}