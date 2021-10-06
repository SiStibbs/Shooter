using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Raiser for GameEventBool events
/// </summary>
[System.Serializable]
public class EventRaiserBool 
{
    public GameEventBool eventToRaise;

    public void RaiseEvent(bool b)
    {
        eventToRaise.Raise(b);
    }
}
