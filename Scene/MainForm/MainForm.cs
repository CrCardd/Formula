using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Objects;
using Formula.Scene;

internal class MainForm(
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
    internal void InitializeComponent(int width, int height)
    {
        nav.OnPop += this.UpdateForm;
        nav.OnPush += this.UpdateForm;

        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.DoubleBuffered = true;
        this.components = new Container();
        this.AutoScaleMode = AutoScaleMode.Font;
        
        this.ClientSize = new Size(width*BaseOBject.Size, height*BaseOBject.Size);
    }

    private void UpdateForm()
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
            this.Text = nav.Peek().Text;
        }
        this.timer.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        if(!nav.HasValue())
            return;

        var scene = nav.Peek();

        var p = this.PointToClient(MousePosition);
        int x = p.X / BaseOBject.Size;
        int y = p.Y / BaseOBject.Size;

        var g = BaseOBject.Size/6;

        nav.Peek().Draw(e);

        if(scene.isValid(x,y))
            e.Graphics.DrawRectangle(
                new Pen(Color.Black, g), 
                x*BaseOBject.Size+(g/2),
                y*BaseOBject.Size+(g/2),
                BaseOBject.Size-(g), 
                BaseOBject.Size-(g)
            );
        
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if(nav.HasValue())
            nav.Peek().OnKeyDown(e);
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        if(nav.HasValue())
            nav.Peek().BaseMouseClick(e);
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