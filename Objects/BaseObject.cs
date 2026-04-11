using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Formula.Enum;
using Formula.Scene;

namespace Formula.Objects;

public class BaseOBject
{
    #region Private constants
    public static int Size {get;set;} = 15;
    #endregion
    #region Private properties

    private int version = 0;
    private Vector2D position;
    private Vector2D prevPosition;
    private Color color;
    private IBehavior? behavior;
    private DirtyFlags dirtyFlag;
    private Matrix tMatrix;
    private Rectangle e;
    private string? label;


    #endregion
    #region Properties setters

    public Guid Id {get;} = Guid.NewGuid();
    public int Version {get => version; set{version = value;}}

    private void PosChange()
        => Flags |= DirtyFlags.MoveDirty;   

    public double X {get => Position.X; [MemberNotNull(nameof(Position))]set{Position = new(value, Y);}}
    public double Y {get => Position.Y; [MemberNotNull(nameof(Position))]set{Position = new(X, value);}}
    private Vector2D Position {get => position;
        set{
            position = value;
            PosChange();
        }
    }
    public Vector2D PrevPosition {get => prevPosition; private set{prevPosition = value;}}

    private void ColorChange()
        => Flags |= DirtyFlags.RenderDirty;
    public Color Color {get => color; 
        [MemberNotNull(nameof(color))]
        set{
            color = value;
            ColorChange();
        }
    }
    
    private void BehaviorChange()
        => Flags |= DirtyFlags.BehaviorDirty;
    public IBehavior? Behavior {get => behavior;
        set{
            behavior = value;
            BehaviorChange();
        }
    }
    
    public DirtyFlags Flags {get => dirtyFlag; 
        [MemberNotNull(nameof(dirtyFlag))]
        set{
            dirtyFlag = value;
            Version++;
        }
    }

    public Rectangle E {get => e; private set {e = value;}}
    
    public string? Label {get => label;set{label = value;}}
    
    public BaseOBject? Shadow {get;set;}

    #endregion
    #region Engine Methods

    [MemberNotNull(nameof(tMatrix))]
    public void RecalculatePosition()
    {
        tMatrix = new Matrix();
        tMatrix.Translate((int)Position.X * Size, (int)Position.Y * Size);
        Flags &= ~DirtyFlags.MoveDirty;
    }
    
    private SolidBrush brush = new(Color.White);
    public void Draw(Graphics g)
    {
        brush.Color = color;
        if (Flags.HasFlag(DirtyFlags.MoveDirty))
            RecalculatePosition();
        var old = g.Transform;
        g.Transform = tMatrix!;
        g.FillRectangle(brush, 0, 0, E.Width, E.Height);
        g.Transform = old;
    }
    public void RestorePosition() => Position = PrevPosition;
    public void Update(Kingdon engine, double time) => behavior?.Execute(this, engine, time);    
    public void SyncShadow()
    {
        PrevPosition = Position;
        if(Shadow == null) Shadow = (BaseOBject)this.MemberwiseClone();
        foreach(var prop in this.GetType().GetProperties().Where(p => p.CanWrite))
            prop.SetValue(Shadow, prop.GetValue(this));
    }

    #endregion
    #region User methods
    public BaseOBject(double x, double y, Color? color=null, IBehavior? behavior=null, string? label=null)
    {
        if(color == null)
            color = Color.Green;
            
        E = new Rectangle((int)x, (int)y, Size, Size);
        Behavior = behavior;
        Label = label;
        X = x;
        Y = y;
        Color = (Color)color;
        RecalculatePosition();
    }
    public BaseOBject(Vector2D position, Color? color=null, IBehavior? behavior=null, string? label=null)
    {
        if(color == null)
            color = Color.Green;
            
        E = new Rectangle((int)position.X, (int)position.Y, Size, Size);
        Behavior = behavior;
        Label = label;
        X = position.X;
        Y = position.Y;
        Color = (Color)color;
        RecalculatePosition();
    }

    [MemberNotNull(nameof(X))]
    [MemberNotNull(nameof(Y))]
    public void SetPosition(int x, int y)
    {
        X = x;
        Y = y;
    }
    [MemberNotNull(nameof(Y))]
    [MemberNotNull(nameof(X))]
    public void SetPosition(Vector2D position)
    {
        X = position.X;
        Y = position.Y;
    }

    #endregion
}