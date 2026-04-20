using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Formula.Enum;
using Formula.Scene;

namespace Formula.Objects;

public partial class BaseOBject
{
    #region Private constants
    public static int Size {get;set;} = 15;
    #endregion
    #region Private properties

    private int version = 0;
    private Vector2D position;
    private int z = 0;
    private Vector2D prevPosition;
    private Color color;
    private IBehavior? behavior;
    private DirtyFlags dirtyFlag;
    private Matrix tMatrix;
    private Rectangle e;


    #endregion
    #region Properties setters

    public Guid Id {get;} = Guid.NewGuid();
    public int Version {get => version; set{version = value;}}

    private void PosChange()
        => Flags |= DirtyFlags.MoveDirty;   

    public double X {get => Position.X; [MemberNotNull(nameof(Position))]set{Position = new(value, Y);}}
    public double Y {get => Position.Y; [MemberNotNull(nameof(Position))]set{Position = new(X, value);}}
    public int Z {
        get => z;
        set
        {
            z = value;
            PosChange();    
        }
    }
    private Vector2D Position {get => position;
        set{
            position = value;
            PosChange();
        }
    }
    internal Vector2D PrevPosition {get => prevPosition; private set{prevPosition = value;}}

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
    
    internal DirtyFlags Flags {get => dirtyFlag; 
        [MemberNotNull(nameof(dirtyFlag))]
        set{
            dirtyFlag = value;
            Version++;
        }
    }

    public string? Label {get;set;}

    internal BaseOBject? Shadow {get;set;}

    #endregion
    #region Engine Methods

    [MemberNotNull(nameof(tMatrix))]
    private void RecalculatePosition()
    {
        tMatrix = new Matrix();
        tMatrix.Translate((int)Position.X * Size, (int)Position.Y * Size);
    }
    
    private SolidBrush brush = new(Color.White);
    internal void Draw(Graphics g)
    {
        brush.Color = color;
        RecalculatePosition();
        var old = g.Transform;
        g.Transform = tMatrix!;
        g.FillRectangle(brush, 0, 0, e.Width, e.Height);
        g.Transform = old;
    }
    internal void RestorePosition() => Position = PrevPosition;
    internal void Update(SceneMap engine, double time)
    {
        behavior?.Execute(this, engine, time);    
    }
    internal void SyncShadow()
    {
        PrevPosition = Position;
        if(Shadow == null) Shadow = (BaseOBject)this.MemberwiseClone();
        foreach(var prop in this.GetType().GetProperties().Where(p => p.CanWrite))
            prop.SetValue(Shadow, prop.GetValue(this));
    }

    #endregion
    #region User methods
    public BaseOBject(double x, double y, int z=0, Color? color=null, IBehavior? behavior=null, string? label=null)
    {
        if(color == null)
            color = Color.Green;
            
        e = new Rectangle((int)x, (int)y, Size, Size);
        Behavior = behavior;
        Label = label;
        X = x;
        Y = y;
        Z = z;
        Color = (Color)color;
        RecalculatePosition();
    }
    public BaseOBject(Vector2D position, int z=0, Color? color=null, IBehavior? behavior=null, string? label=null)
    {
        if(color == null)
            color = Color.Green;
            
        e = new Rectangle((int)position.X, (int)position.Y, Size, Size);
        Behavior = behavior;
        Label = label;
        X = position.X;
        Y = position.Y;
        Z = z;
        Color = (Color)color;
        RecalculatePosition();
    }
    #endregion
}