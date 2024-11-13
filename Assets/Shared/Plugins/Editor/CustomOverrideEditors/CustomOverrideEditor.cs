using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Plugins.Editor.CustomOverrideEditors
{
	//[CustomEditor(typeof(TargetType)),CanEditMultipleObjects]
	public abstract class CustomOverrideEditor : UnityEditor.Editor
	{
		protected abstract string DefaultEditorTypeName { get; }// => "UnityEditor.TargetTypeEditor";
		private Type DefaultEditorType => Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType(DefaultEditorTypeName);

		private UnityEditor.Editor _defaultEditor;

		protected virtual void OnEnable()
		{
			if (_defaultEditor)
			{
				CreateCachedEditor(targets, DefaultEditorType, ref _defaultEditor);
			}
			else
			{
				_defaultEditor = CreateEditor(targets, DefaultEditorType);
			}
		}

		protected void OnDisable()
		{
			if (_defaultEditor)
			{
				DestroyImmediate(_defaultEditor);
			}
		}

		public override void OnInspectorGUI()
		{
			if (_defaultEditor)
			{
				_defaultEditor.OnInspectorGUI();
			}

			GUI.enabled = false;
			EditorGUILayout.ObjectField("Custom Editor", MonoScript.FromScriptableObject(this), typeof(MonoScript), true);
			GUI.enabled = true;
		}
	}
}