using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Raiser for GameEventSimple events
/// </summary>
[System.Serializable]
public class EventRaiserSimple
{
    public GameEventSimple eventToRaise;

    public void RaiseEvent()
    {
        eventToRaise.Raise();
    }
}
