using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Scriptable object for storing a float value
/// </summary>
[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
	[SerializeField]
	private float _initValue;

	private float _valueRuntime;
	private bool _initialized = false;

	private void OnEnable()
	{
		_initialized = false;
	}

	public float Value
	{
		get
		{
			if (!_initialized)
			{
				_valueRuntime = _initValue; //Read from serialized field
				_initialized = true;
			}
			return _valueRuntime;
		}
		set
		{
			_initialized = true;
			_valueRuntime = value;
		}
	}

	public void Increment()
    {
		_valueRuntime++;
    }
}