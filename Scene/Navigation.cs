using System;
using System.Collections.Generic;
using Formula.Interfaces;
using Formula.Objects;

namespace Formula.Scene;

public class Navigation : INavigation
{
    public event Action? OnPush;
    public event Action? OnPop;


    private Kingdon? last = null;

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
    public static INavigation Get() => instance;


    private Stack<Kingdon> stack = [];

    public Kingdon Peek() => stack.Peek();
    public T Peek<T>() where T : Kingdon => (T)stack.Peek();

    public bool HasValue() => stack.Count != 0;

    public void Pop()
    {
        last = stack.Pop();
        OnPop?.Invoke();
    } 
    public void Push(Kingdon scene)
    {
        if(HasValue())
            last = Peek();
        else
            last = null;

        stack.Push(scene);
        OnPush?.Invoke();
    }

    public Kingdon? Last() => last;
}