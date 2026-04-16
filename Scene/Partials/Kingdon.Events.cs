using Formula.Interfaces;
using Formula.Objects;
using Formula.Scene.GetPlaceStrategies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Formula.Scene;

partial class Kingdon
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Action<IWorld, MouseArgs>? MouseDown {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Action<IWorld, MouseArgs>? MouseUp {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Action<IWorld, MouseArgs>? MouseMove {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Dictionary<Keys, Action<IUser>> GlobalHotkeys {get;set;} = [];

    private MouseArgs? MouseArgs = null;

    protected override void OnMouseDown(MouseEventArgs e) => BaseMouseAction(MouseDown, e);
    protected override void OnMouseUp(MouseEventArgs e) => BaseMouseAction(MouseUp, e);
    protected override void OnMouseMove(MouseEventArgs e) => BaseMouseAction(MouseMove, e);    
    private void BaseMouseAction(Action<IWorld, MouseArgs>? action, MouseEventArgs e)
    {
        this.getplace = new GetReal(this);
        var ma = new MouseArgs(e,this,MouseArgs);
        action?.Invoke(this,ma);
        this.getplace = new GetShadow(this);
        
        if(MouseArgs?.Position != ma.Position)
            this.Invalidate();

        MouseArgs = ma;
        
    }


    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
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
    public void CaptureInputSnapshot()
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