using System.Collections.Generic;
using Formula.Interfaces;

namespace Formula.Objects;

public class EventManager
{
    private Dictionary<string, List<IListener>> listeners = [];
    public void Subscribe(string key, IListener listener)
    {
        if(!listeners.TryGetValue(key, out var value))
            listeners.Add(key, []);
        listeners[key].Add(listener);
    }
    public void Unsubscribe(string key, IListener listener)
    {
        if(listeners.TryGetValue(key, out var value))
            value.Remove(listener);
    } 
    public void Notify(string key)
    {
        if(!listeners.TryGetValue(key, out var value))
            return;
        foreach(var v in value)
            v.Update();
    }
}