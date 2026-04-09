using Formula.Interfaces;
using Formula.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Formula.Scene;

partial class Kingdon
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Action<IWorld, int, int>? OnGridClick {get;set;}
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Dictionary<Keys, Action> GlobalHotkeys {get;set;} = [];
    protected override void OnMouseDown(MouseEventArgs e)
    {
        int gridX = e.X / IObject.Size;
        int gridY = e.Y / IObject.Size;
        var rawWorld = new RawKingdon(this);
        OnGridClick?.Invoke(rawWorld, gridX, gridY);
    }
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (GlobalHotkeys.TryGetValue(e.KeyCode, out var action))
            action.Invoke();
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
}