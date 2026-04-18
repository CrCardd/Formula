using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Scene;

public class MainForm : Form, IListener
{
    private IContainer components = null!;
    
    private Timer timer = new();

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }
    public MainForm(int width, int height)
    {
        var nav = Navigation.Get();
        this.timer.Tick += nav.Peek().Loop;
        this.timer.Start();

        this.DoubleBuffered = true;
        this.components = new Container();
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(width, height);
    }

    void IListener.Update()
    {
        var nav = Navigation.Get();

        this.timer.Stop();
        if(nav.Last() != null)
            this.timer.Tick -= nav.Last()!.Loop;
        if(nav.HasValue())
            this.timer.Tick += nav.Peek().Loop;
        this.timer.Start();
    }

    // FAZER ON KEY DOWN
    
    // FAZER ON MOUSE MOVE
    // FAZER ON MOUSE DOWN
    // FAZER ON MOUSE UP
}