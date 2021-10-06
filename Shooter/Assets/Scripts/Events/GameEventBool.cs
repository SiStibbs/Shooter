using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Scriptable object event that passes a bool to listeners
/// </summary>
[System.Serializable]
public class GameEventBool : ScriptableObject
{
	private List<EventListenerBool> listeners = new List<EventListenerBool>();

	public void Raise(bool b)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised(b);
	}

	public void RegisterListener(EventListenerBool listener)
	{
		listeners.Add(listener);
	}

	public void UnregisterListener(EventListenerBool listener)
	{
		listeners.Remove(listener);
	}
}
