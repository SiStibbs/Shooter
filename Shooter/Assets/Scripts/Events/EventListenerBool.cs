using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Listener for GameEventBool events
/// </summary>
[System.Serializable]
public class EventListenerBool 
{
    public GameEventBool EventListenedFor;
    public System.Action<bool> onEventRaisedAction;

    public virtual void OnEventRaised(bool b)
    {
        onEventRaisedAction?.Invoke(b);
    }

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
}
