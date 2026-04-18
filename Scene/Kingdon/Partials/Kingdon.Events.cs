using Formula.Interfaces;
using Formula.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Formula.Scene;

partial class Kingdon
{
    public abstract void OnMouseDown(IWorld world, MouseArgs e);
    public abstract void OnMouseUp(IWorld world, MouseArgs e);
    public abstract void OnMouseMove(IWorld world, MouseArgs e);
    
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Dictionary<Keys, Action<IUser>> GlobalHotkeys {get;set;} = [];

    private MouseArgs? MouseArgs = null;

    public void BaseMouseDown(MouseEventArgs e) => BaseMouseAction(OnMouseDown, e);


    public void BaseMouseUp(MouseEventArgs e) => BaseMouseAction(OnMouseUp, e);
    public void BaseMouseMove(MouseEventArgs e) => BaseMouseAction(OnMouseMove, e);    

    private void BaseMouseAction(Action<IWorld, MouseArgs>? action, MouseEventArgs e)
    {
        this.getPlace = this.getRealPlace;
        var ma = new MouseArgs(e,this.getRealPlace, MouseArgs);
        action?.Invoke(this,ma);
        this.getPlace = this.getShadowPlace;
        
        if(MouseArgs?.Position != ma.Position)
            this.Invalidate();

        MouseArgs = ma;
        
    }


    private void BaseOnKeyDown(KeyEventArgs e)
    {
        if (GlobalHotkeys.TryGetValue(e.KeyCode, out var action))
            action.Invoke(this);
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