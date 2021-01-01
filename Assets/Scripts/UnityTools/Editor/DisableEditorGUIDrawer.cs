using UnityEngine;
using UnityEditor;
/// <summary>
/// A custom GUI drawer for a ReadOnly attribute.
/// </summary>
[CustomPropertyDrawer(typeof(CrawfisSoftware.UnityTools.DisableEditorGUIAttribute))]
public class DisableEditorGUIDrawer : PropertyDrawer
{
    /// <summary>
    /// Unity method for drawing GUI in Editor
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="property">Property.</param>
    /// <param name="label">Label.</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Saving previous GUI enabled value
        var previousGUIState = GUI.enabled;
        // Disabling edit for property
        GUI.enabled = false;
        // Drawing Property
        EditorGUI.PropertyField(position, property, label);
        // Setting old GUI enabled value
        GUI.enabled = previousGUIState;
    }
}