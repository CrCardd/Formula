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
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Action<IWorld, MouseArgs>? MousePaint {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Action<IWorld, MouseArgs>? MouseDown {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Action<IWorld, MouseArgs>? MouseUp {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Action<IWorld, MouseArgs>? MouseMove {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Dictionary<Keys, Action<IUser>> GlobalHotkeys {get;set;} = [];

    private bool isMousePressed = false;
    private HashSet<(int,int)> passed = [];
    private Vector2D lastPosition = new(-1,-1);
    protected override void OnMouseDown(MouseEventArgs e)
    {
        var pos = Coords(e);
        var ma = new MouseArgs(e,this,lastPosition);
        isMousePressed = true;
        InvokeOnMousePaint(ma);

        MouseDown?.Invoke(this,ma);
        lastPosition = pos;
    }
    protected override void OnMouseUp(MouseEventArgs e)
    {
        var pos = Coords(e);
        var ma = new MouseArgs(e,this,lastPosition);
        MouseUp?.Invoke(this,ma);

        isMousePressed = false;
        passed = [];
        lastPosition = pos;
    }
    protected override void OnMouseMove(MouseEventArgs e)
    {
        var pos = Coords(e);
        var ma = new MouseArgs(e,this,lastPosition);
        MouseMove?.Invoke(this,ma);


        if(lastPosition == pos)
            return;
        InvokeOnMousePaint(ma);
        lastPosition = pos;
    }
    public Vector2D Coords(MouseEventArgs e) => new(e.X / BaseOBject.Size, e.Y / BaseOBject.Size);
    public void InvokeOnMousePaint(MouseArgs e)
    {
        if(!isMousePressed)
            return;

        if(!isValid(e.Position.X,e.Position.Y))
            return;
        if(passed.Contains(((int)e.Position.X, (int)e.Position.Y)))
            return;
        
        var rawWorld = new RawKingdon(this);
        MousePaint?.Invoke(rawWorld,e);
        if(!isFree(e.Position.X,e.Position.Y)) passed.Add(((int)e.Position.X, (int)e.Position.Y));
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
    private static extern short GetAsyncKeyState(Keys vKey);
    public bool IsKeyDown(Keys key)
    => (GetAsyncKeyState(key) & 0x8000) != 0;
}