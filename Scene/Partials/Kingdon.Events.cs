using Formula.Interfaces;
using Formula.Math;
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
    public Action<IWorld, double, double>? MousePaint {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Action<IWorld, double, double>? MouseDown {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Action<IWorld, double, double>? MouseUp {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Action<IWorld, double, double>? MouseMove {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Dictionary<Keys, Action<IUser>> GlobalHotkeys {get;set;} = [];

    private bool isMousePressed = false;
    private HashSet<(int,int)> passed = [];
    protected override void OnMouseDown(MouseEventArgs e)
    {
        var pos = Coords(e);
        isMousePressed = true;
        InvokeOnMousePaint(e,pos);
    }
    protected override void OnMouseUp(MouseEventArgs e)
    {
        isMousePressed = false;
        passed = [];
    }
    protected override void OnMouseMove(MouseEventArgs e)
    {
        var pos = Coords(e);
        InvokeOnMousePaint(e,pos);
    }

    public Vector2D Coords(MouseEventArgs e) => new(e.X / IObject.Size, e.Y / IObject.Size);
    public void InvokeOnMousePaint(MouseEventArgs e, Vector2D pos)
    {
        if(!isMousePressed)
            return;

        if(!isValid(pos.X,pos.Y))
            return;
        if(passed.Contains(((int)pos.X, (int)pos.Y)))
            return;
        
        var rawWorld = new RawKingdon(this);
        MousePaint?.Invoke(rawWorld, pos.X, pos.Y);
        if(!isFree(pos.X,pos.Y)) passed.Add(((int)pos.X, (int)pos.Y));
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