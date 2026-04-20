using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Scene;

public class MainForm(
    INavigation nav
) : Form
{
    private IContainer components = null!;
    
    private Timer timer = new();

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }
    public void InitializeComponent(int width, int height)
    {
        var nav = Navigation.Get();
        this.timer.Tick += nav.Peek().Loop;
        this.timer.Start();

        nav.OnPop += Update;
        nav.OnPush += Update;

        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.DoubleBuffered = true;
        this.components = new Container();
        this.AutoScaleMode = AutoScaleMode.Font;
        
        this.ClientSize = new Size(width, height);
    }

    private new void Update()
    {
        this.timer.Stop();
        if(nav.Last() != null)
        {
            nav.Peek().OnReload -= this.Invalidate;
            this.timer.Tick -= nav.Last()!.Loop;
        }
        if(nav.HasValue())
        {
            // Vai dar problema, no fluxo ele vai se inscrever de volta 
            // em páginas que ele ja ta inscrito, precisa validar se 
            // ele já está inscrito nessa tela (resolvido com gambiarra)
            nav.Peek().OnReload += this.Invalidate;
            this.timer.Tick += nav.Peek().Loop;
        }
        this.timer.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        if(nav.HasValue())
            nav.Peek().OnPaint(e, this);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if(nav.HasValue())
            nav.Peek().OnKeyDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if(nav.HasValue())
            nav.Peek().BaseMouseMove(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if(nav.HasValue())
            nav.Peek().BaseMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        if(nav.HasValue())
            nav.Peek().BaseMouseUp(e);
    }
}