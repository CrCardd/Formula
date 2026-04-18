using Formula.Scene;

namespace Formula.Interfaces;

public interface INavigation
{
    void Push(Kingdon scene);
    void Pop();
    Kingdon? Last();
    Kingdon Peek();
    bool HasValue();
    public T Peek<T>() where T : Kingdon;
}