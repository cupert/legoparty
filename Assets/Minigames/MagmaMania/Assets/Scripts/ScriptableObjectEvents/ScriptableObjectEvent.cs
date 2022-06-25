using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Object Event/Parameterless")]
public class ScriptableObjectEvent : ScriptableObject
{
    private UnityEvent backingEvent = new UnityEvent();

    public void Add(UnityAction a) => backingEvent.AddListener(a);

    public void Remove(UnityAction a) => backingEvent.RemoveListener(a);

    public void Raise()
    {
        backingEvent.Invoke();
    }

    public static ScriptableObjectEvent operator +(ScriptableObjectEvent a, UnityAction b)
    {
        a.Add(b);
        return a;
    }

    public static ScriptableObjectEvent operator -(ScriptableObjectEvent a, UnityAction b)
    {
        a.Remove(b);
        return a;
    }
}

public abstract class ScriptableObjectEvent<T> : ScriptableObject
{
    private UnityEvent<T> backingEvent = new UnityEvent<T>();

    public void Raise(T arg)
    {
        backingEvent.Invoke(arg);
    }

    public void Add(UnityAction<T> a) => backingEvent.AddListener(a);

    public void Remove(UnityAction<T> a) => backingEvent.RemoveListener(a);

    public static ScriptableObjectEvent<T> operator +(ScriptableObjectEvent<T> a, UnityAction<T> b)
    {
        a.Add(b);
        return a;
    }

    public static ScriptableObjectEvent<T> operator -(ScriptableObjectEvent<T> a, UnityAction<T> b)
    {
        a.Remove(b);
        return a;
    }
}