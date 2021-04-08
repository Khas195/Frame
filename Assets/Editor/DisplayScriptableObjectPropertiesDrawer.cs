using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DisplayScriptableObjectPropertiesAttribute))]
public class DisplayScriptableObjectPropertiesDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = 16;
        EditorGUI.PropertyField(position, property);
        position.y += 20;

        if (property.objectReferenceValue != null)
        {
            var so = new SerializedObject(property.objectReferenceValue);
            position.x += 20;
            position.width -= 40;
            so.Update();

            var prop = so.GetIterator();
            prop.NextVisible(true);
            while (prop.NextVisible(true))
            {
                position.height = 16;
                EditorGUI.PropertyField(position, prop);
                position.y += 20;
            }
            if (GUI.changed)
            {
                so.ApplyModifiedProperties();
            }
        }

    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = base.GetPropertyHeight(property, label);
        if (property.objectReferenceValue != null)
        {
            var so = new SerializedObject(property.objectReferenceValue);
            var prop = so.GetIterator();
            prop.NextVisible(true);
            while (prop.NextVisible(true)) height += 20;
        }
        return height;
    }

}