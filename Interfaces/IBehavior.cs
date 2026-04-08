using Formula.Objects;

namespace Formula.Interfaces;

public interface IBehavior
{
    public void Execute(IObject obj, IWorld world);
}

public abstract class Behavior<T> : IBehavior where T : IObject
{
    public abstract void Execute(T obj, IWorld world);

    void IBehavior.Execute(IObject obj, IWorld world)
    {
        if(obj is T t) Execute(t, world);
    }
}