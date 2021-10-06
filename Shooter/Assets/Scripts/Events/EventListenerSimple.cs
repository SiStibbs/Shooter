using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// <summary>
/// Listener for GameEventSimple events
/// </summary>
[System.Serializable]
public class EventListenerSimple 
{
    public GameEventSimple EventListenedFor;
    public System.Action onEventRaisedAction;
    public UnityEvent Response;

    public void RegisterListener()
    {
        if (EventListenedFor != null)
        {
            EventListenedFor.RegisterListener(this);
        }
    }

    public void UnregisterListener()
    {
        if (EventListenedFor != null)
        {
            EventListenedFor.UnregisterListener(this);
        }
    }

    public void OnEventRaised()
    {
        onEventRaisedAction?.Invoke();
        Response?.Invoke(); 
    }
}