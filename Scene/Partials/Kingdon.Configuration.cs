using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Formula.Objects;

namespace Formula.Scene;

partial class Kingdon
{
    private static Kingdon? instance = null;
    private static readonly object _padlock = new object();

    private readonly Timer timer; 
    private static Kingdon Instance
    {
        get
        {
            throw new InvalidOperationException("Kingdon must be initialized by GetInstance(w, h) before access.");
        }
    }
    private Kingdon(int w, int h, int? z=null, string label="screen")
    {   
        this.InitializeComponent();
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.DoubleBuffered = true;

        this.w = w;
        this.h = h;
        this.z = z;
        this.Text = label;

        this.ClientSize = new Size(BaseOBject.Size * w , BaseOBject.Size * h);
    
        Timer t = new();
        this.timer = t;
        timer.Interval = 16; // ~60 FPS
        timer.Tick += this.Loop;
        timer.Start();
    }

    public void Loop(object? sender, EventArgs e)
    {
        foreach(var obj in Objects.Values) obj.SyncShadow();
        CaptureInputSnapshot();
        foreach(var obj in Objects.Values) obj.Update(this, (double)timer.Interval/1000.0);
        MoveObjects();
        DestroyObjects();
        SpawnObjects();

        this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        foreach (var position in GridObjects.Values)
        {
            var pos = position.OrderByDescending(p => p.Z);
            foreach(var obj in pos)
                obj.Draw(e.Graphics);
        }
    }
}