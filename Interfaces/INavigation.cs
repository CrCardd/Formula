using System;
using Formula.Scene;

namespace Formula.Interfaces;

public interface INavigation
{
    event Action? OnPush;
    event Action? OnPop;

    void Push(SceneMap scene);
    void Pop();
    SceneMap? Last();
    SceneMap Peek();
    bool HasValue();
    public T Peek<T>() where T : SceneMap;
}