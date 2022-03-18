using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameEvent
{
    private event Action action = delegate { };

    public void Invoke()
    {
        action.Invoke();
    }

    public void AddListener(Action listener)
    {
        RemoveListener(listener);
        action += listener;
    }

    public void RemoveListener(Action listener)
    {
        action -= listener;

    }

    public bool IsEmpty()
    {
        return action == null;
    }
}

public class GameEvent<T>
{
    private event Action<T> action = delegate { };

    public void Invoke(T param)
    {
        action.Invoke(param);
    }

    public void AddListener(Action<T> listener)
    {
        RemoveListener(listener);
        action += listener;
    }

    public void RemoveListener(Action<T> listener)
    {
        action -= listener;
    }
    public bool IsEmpty()
    {
        return action == null;
    }

}
