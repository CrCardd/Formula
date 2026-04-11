using System;
using Formula.Interfaces;
using Formula.Objects;

public interface IBehavior
{
    void Execute(Formula.Objects.BaseOBject obj, IWorld world, double t);
}
public abstract class Behavior<T> : IBehavior where T : Formula.Objects.BaseOBject
{
    public virtual void Execute(T obj, IWorld world) 
    => throw new NotImplementedException("Você deve implementar pelo menos uma sobrecarga de Execute.");
    
    public virtual void Execute(T obj, IWorld world, double t)
    => Execute(obj, world);
    
    void IBehavior.Execute(Formula.Objects.BaseOBject obj, IWorld world, double t)
    {
        if (obj is T v) Execute(v, world, t);
    }
}
public abstract class Behavior : IBehavior
{
    public virtual void Execute(Formula.Objects.BaseOBject obj, IWorld world) 
    => throw new NotImplementedException("Você deve implementar pelo menos uma sobrecarga de Execute.");
    
    public virtual void Execute(Formula.Objects.BaseOBject obj, IWorld world, double t)
    => Execute(obj, world);
    
    void IBehavior.Execute(Formula.Objects.BaseOBject obj, IWorld world, double t)
    => Execute(obj, world, t);
}