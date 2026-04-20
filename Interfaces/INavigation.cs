using System;
using Formula.Scene;

namespace Formula.Interfaces;

public interface INavigation
{
    event Action? OnPush;
    event Action? OnPop;

    void Push(Kingdon scene);
    void Pop();
    Kingdon? Last();
    Kingdon Peek();
    bool HasValue();
    public T Peek<T>() where T : Kingdon;
}