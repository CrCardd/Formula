using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Objects;
using Formula.Scene.GetPlaceStrategies;

namespace Formula.Scene;

partial class SceneMap
{   
    private Timer? timer; 

    private IInteract? getPlace;
    private IInteract? getShadowPlace;
    private IInteract? getRealPlace;

    
    public int Width {get;set;}
    public int Height {get;set;}
    public int? Depth {get;set;}
    
    public string Text {get;set;} = "Screen";

    public event Action? OnReload;

    
    protected void Initialize(int w, int h, int? z=null, string? label=null)
    {
        this.Width = w;
        this.Height = h;
        this.Depth = z;
        if(label is not null)
            this.Text = label;

        this.getRealPlace = new GetReal(this);
        this.getShadowPlace = new GetShadow(this);
        this.getPlace = this.getShadowPlace;

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
        if(timer is not null)
            foreach(var obj in Objects.Values) obj.Update(this, (double)timer.Interval/1000.0);
        MoveObjects();
        DestroyObjects();
        SpawnObjects();

        OnLoop();

        OnReload?.Invoke();
    }

    internal void Draw(PaintEventArgs e)
    {
        foreach (var obj in GridObjects.Values.SelectMany(x => x).OrderBy(x => x.Z))
            obj.Draw(e.Graphics);
    }
}