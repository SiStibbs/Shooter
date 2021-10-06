using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(FloatVariable))]
public class FloatVariableInspector : UnityEditor.Editor
{
	public override void OnInspectorGUI()
	{
		var floatVar = (FloatVariable)target;
		var initValueProp = serializedObject.FindProperty("_initValue");

		EditorGUILayout.PropertyField(initValueProp);

		if (!Application.isPlaying)
		{
			//At edit time - we can't edit the runtime field. Show a preview of whats it's going to be on startup
			using (new EditorGUI.DisabledScope(true))
			{
				EditorGUILayout.TextField("Value (Runtime)", initValueProp.floatValue.ToString()); //<-- reading initValue, not Value, to get an accurate preview
			}
		}
		else
		{
			//At runtime we can edit the runtime field, which will expire when play mode exits 
			floatVar.Value = float.Parse(EditorGUILayout.TextField("Value (Runtime)", floatVar.Value.ToString()));

			if (GUILayout.Button("APPLY TO INIT VALUE"))
			{
				initValueProp.floatValue = floatVar.Value;
			}
		}

		if (serializedObject.hasModifiedProperties)
		{
			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif