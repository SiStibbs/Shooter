using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Scriptable object event that calls listeners
/// </summary>
[CreateAssetMenu]
[System.Serializable]
public class GameEventSimple : ScriptableObject
{
	private List<EventListenerSimple> listeners = new List<EventListenerSimple>();

	public void Raise()
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised();
	}

	public void RegisterListener(EventListenerSimple listener)
	{ 
		listeners.Add(listener); 
	}

	public void UnregisterListener(EventListenerSimple listener)
	{ 
		listeners.Remove(listener); 
	}
}