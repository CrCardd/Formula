using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Formula.Enum;
using Formula.Math;
using Formula.Scene;

namespace Formula.Interfaces;


public class IObject
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

    public int X {get => Position.X; [MemberNotNull(nameof(Position))]set{Position = new(value, Y);}}
    public int Y {get => Position.Y; [MemberNotNull(nameof(Position))]set{Position = new(X, value);}}
    private Vector2D Position {get => position;
        set{
            SavePosition();
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
    
    public IObject? Shadow {get;set;}

    #endregion
    #region Engine Methods

    [MemberNotNull(nameof(tMatrix))]
    public void RecalculatePosition()
    {
        tMatrix = new Matrix();
        tMatrix.Translate(Position.X * Size, Position.Y * Size);
        Flags &= ~DirtyFlags.MoveDirty;
    }
    public void Draw(Graphics g)
    {
        if (Flags.HasFlag(DirtyFlags.MoveDirty))
            RecalculatePosition();
        var old = g.Transform;
        g.Transform = tMatrix!;
        g.FillRectangle(new SolidBrush(color), 0, 0, E.Width, E.Height);
        g.Transform = old;
    }
    private void SavePosition() => PrevPosition = Position;
    public void RestorePosition() => Position = PrevPosition;
    public void Update(Kingdon engine) => behavior?.Execute(this, engine);    
    public void SyncShadow()
    {
        if(Shadow == null) Shadow = (IObject)this.MemberwiseClone();
        foreach(var prop in this.GetType().GetProperties().Where(p => p.CanWrite))
            prop.SetValue(Shadow, prop.GetValue(this));
    }

    #endregion
    #region User methods
    public IObject(int x, int y, Color? color=null, IBehavior? behavior=null, string? label=null)
    {
        if(color == null)
            color = Color.Green;
            
        E = new Rectangle(x, y, Size, Size);
        Behavior = behavior;
        Label = label;
        X = x;
        Y = y;
        Color = (Color)color;
        RecalculatePosition();
    }
    public IObject(Vector2D position, Color? color=null, IBehavior? behavior=null, string? label=null)
    {
        if(color == null)
            color = Color.Green;
            
        E = new Rectangle(position.X, position.Y, Size, Size);
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