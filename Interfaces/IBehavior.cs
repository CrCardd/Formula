using System;
using System.Windows.Forms;
using Formula.Interfaces;
using Formula.Objects;

public interface IBehavior
{
    void Execute(BaseOBject obj, IWorld world, double t);
    void OnMouseHover(BaseOBject obj, MouseArgs e);
    void OnMouseClick(BaseOBject obj, MouseArgs e);
    void OnMouseDown(BaseOBject obj, MouseArgs e);
    void OnMouseUp(BaseOBject obj, MouseArgs e);
}
public abstract class Behavior<T> : IBehavior where T : BaseOBject
{
    public virtual void Execute(T obj, IWorld world) 
    => throw new NotImplementedException("Você deve implementar pelo menos uma sobrecarga de Execute.");
    public virtual void Execute(T obj, IWorld world, double t)
    => Execute(obj, world);
    void IBehavior.Execute(BaseOBject obj, IWorld world, double t)
    {
        if (obj is T v) Execute(v, world, t);
    }


    public virtual void OnMouseClick(T obj, MouseArgs e){}
    void IBehavior.OnMouseClick(BaseOBject obj, MouseArgs e)
    {
        if (obj is T v) OnMouseClick(v, e);
    }
    
    public virtual void OnMouseDown(T obj, MouseArgs e){}
    void IBehavior.OnMouseDown(BaseOBject obj, MouseArgs e)
    {
        if (obj is T v) OnMouseDown(v, e);
    }

    public virtual void OnMouseHover(T obj, MouseArgs e){}
    void IBehavior.OnMouseHover(BaseOBject obj, MouseArgs e)
    {
        if (obj is T v) OnMouseHover(v, e);
    }

    public virtual void OnMouseUp(T obj, MouseArgs e){}
    void IBehavior.OnMouseUp(BaseOBject obj, MouseArgs e)
    {
        if (obj is T v) OnMouseUp(v, e);
    }

}
public abstract class Behavior : IBehavior
{
    public virtual void Execute(BaseOBject obj, IWorld world) 
    => throw new NotImplementedException("Você deve implementar pelo menos uma sobrecarga de Execute.");
    public virtual void Execute(BaseOBject obj, IWorld world, double t)
    => Execute(obj, world);
    void IBehavior.Execute(BaseOBject obj, IWorld world, double t)
    => Execute(obj, world, t);

    public virtual void OnMouseClick(BaseOBject obj, MouseArgs e){}
    public virtual void OnMouseDown(BaseOBject obj, MouseArgs e){}
    public virtual void OnMouseHover(BaseOBject obj, MouseArgs e){}
    public virtual void OnMouseUp(BaseOBject obj, MouseArgs e){}
}