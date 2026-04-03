using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Formula.Interfaces;

namespace Formula.Scene;

partial class Kingdon
{
    public Action<int, int>? OnGridClick;
    protected override void OnMouseDown(MouseEventArgs e)
    {
        int gridX = e.X / IObject.Size;
        int gridY = e.Y / IObject.Size;
        OnGridClick?.Invoke(gridX, gridY);
    }

    public Dictionary<Keys, Action> GlobalHotkeys = new();
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (GlobalHotkeys.TryGetValue(e.KeyCode, out var action))
            action.Invoke();
    }
}