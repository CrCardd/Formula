using System;
using System.Drawing;
using System.Windows.Forms;

namespace Formula.Scene;

partial class Kingdon
{
    public void Loop(object? sender, EventArgs e)
    {
        foreach(var obj in Objects.Values) obj.SyncShadow();
        foreach(var obj in Objects.Values) obj.Update(this);
        MoveObjects();
        DestroyObjects();
        SpawnObjects();

        this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        foreach (var obj in Objects.Values)
            obj.Draw(e.Graphics);
        
        e.Graphics.DrawString(Objects.Count.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 1, 1);
        e.Graphics.DrawString(GridObjects.Count.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 1, 11);
    }
}