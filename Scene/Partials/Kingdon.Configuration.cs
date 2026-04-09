using System;
using System.Drawing;
using System.Windows.Forms;
using Formula.Objects;

namespace Formula.Scene;

partial class Kingdon
{
    private static Kingdon? isntance = null;
    private static readonly object _padlock = new object();
    public static Kingdon Instance
    {
        get
        {
            throw new InvalidOperationException("Kingdon must be initialized by GetInstance(w, h) before access.");
        }
    }
    private Kingdon(int w, int h)
    {   
        ApplicationConfiguration.Initialize();
        this.InitializeComponent();
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.DoubleBuffered = true;

        this.w = w;
        this.h = h;

        this.ClientSize = new Size(IObject.Size * w,IObject.Size * h);
    
        System.Windows.Forms.Timer t = new();
        t.Interval = 16; // ~60 FPS
        t.Tick += this.Loop;
        
        t.Start();
    }

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
    }
}