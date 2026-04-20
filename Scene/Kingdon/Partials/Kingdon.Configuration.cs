using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Objects;
using Formula.Scene.GetPlaceStrategies;

namespace Formula.Scene;

partial class Kingdon
{   
    private Timer? timer; 

    private IGetPlace? getPlace;
    private IGetPlace? getShadowPlace;
    private IGetPlace? getRealPlace;

    
    public int Width {get;set;}
    public int Height {get;set;}
    public int? Depth {get;set;}
    
    public string Text {get;set;} = "Screen";

    public event Action? OnReload;


    public void Initialize(int w, int h, int? z=null, string? label=null)
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

        OnReload?.Invoke();
    }

    public void OnPaint(PaintEventArgs e, Control control)
    {
        foreach (var obj in GridObjects.Values.SelectMany(x => x).OrderBy(x => x.Z))
            obj.Draw(e.Graphics);


        var p = control.PointToClient(Control.MousePosition);
        int x = p.X / BaseOBject.Size;
        int y = p.Y / BaseOBject.Size;
        if(isValid(x,y))
            e.Graphics.DrawRectangle(
                new Pen(Color.Black, BaseOBject.Size/7), 
                x * BaseOBject.Size,
                y * BaseOBject.Size,
                BaseOBject.Size, 
                BaseOBject.Size
            );
    }
}