using Formula.Interfaces;
using Formula.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Formula.Scene;

partial class Kingdon
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Action<IWorld, int, int>? OnMousePaint {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Dictionary<Keys, Action<IWorld>> GlobalHotkeys {get;set;} = [];

    private bool isMousePressed = false;
    private HashSet<(double,double)> passed = [];
    protected override void OnMouseDown(MouseEventArgs e)
    {
        isMousePressed = true;
        InvokeOnMousePaint(e);
    }
    protected override void OnMouseUp(MouseEventArgs e)
    {
        isMousePressed = false;
        passed = [];
    }
    protected override void OnMouseMove(MouseEventArgs e)
    {
        InvokeOnMousePaint(e);
    }
    public void InvokeOnMousePaint(MouseEventArgs e)
    {
        if(!isMousePressed)
            return;
        int gridX = e.X / IObject.Size;
        int gridY = e.Y / IObject.Size;

        if(!isValid(gridX,gridY))
            return;
        if(passed.Contains((gridX, gridY)))
            return;
        
        var rawWorld = new RawKingdon(this);
        OnMousePaint?.Invoke(rawWorld, gridX, gridY);
        if(!isFree(gridX,gridY)) passed.Add((gridX, gridY));
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