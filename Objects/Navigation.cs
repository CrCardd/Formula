using System;
using System.Collections.Generic;
using Formula.Interfaces;
using Formula.Scene;

namespace Formula.Objects;

public class Navigation : INavigation
{
    public event Action? OnPush;
    public event Action? OnPop;


    private SceneMap? last = null;

    private static INavigation? _instance;
    private static INavigation instance
    {
        get
        {
            if(_instance is null)
                _instance = new Navigation();
            return _instance;
        }
        set => _instance = value;
    }
    private Navigation(){}
    internal static INavigation Get() => instance;


    private Stack<SceneMap> stack = [];

    public SceneMap Peek() => stack.Peek();
    public T Peek<T>() where T : SceneMap => (T)stack.Peek();

    public bool HasValue() => stack.Count != 0;

    public void Pop()
    {
        last = stack.Pop();
        OnPop?.Invoke();
    } 
    public void Push(SceneMap scene)
    {
        if(HasValue())
            last = Peek();
        else
            last = null;

        stack.Push(scene);
        OnPush?.Invoke();
    }

    public SceneMap? Last() => last;
}