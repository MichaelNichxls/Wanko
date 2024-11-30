using System;
using UnityEditor;
using UnityEngine;
using Wanko.Utilities.Attributes;

namespace Wanko.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
    public sealed class MinMaxSliderDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var minMaxSliderAttribute = attribute as MinMaxSliderAttribute;

            Rect control = EditorGUI.PrefixLabel(position, label);
            Vector2 vector;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                    vector = property.vector2Value;
                    break;
                case SerializedPropertyType.Vector2Int:
                    vector = property.vector2IntValue;
                    break;
                default:
                    EditorGUI.LabelField(control, $"Use MinMaxSlider with {nameof(Vector2)} or {nameof(Vector2Int)}.");
                    return;
            }

            EditorGUI.BeginChangeCheck();

            vector.x = EditorGUI.FloatField(
                new Rect(control.x, control.y, EditorGUIUtility.fieldWidth, control.height),
                MathF.Round(vector.x, 2));

            vector.y = EditorGUI.FloatField(
                new Rect(control.xMax - EditorGUIUtility.fieldWidth, control.y, EditorGUIUtility.fieldWidth, control.height),
                MathF.Round(vector.y, 2));

            EditorGUI.MinMaxSlider(
                new Rect(control.x + EditorGUIUtility.fieldWidth + 5f, control.y, control.width - EditorGUIUtility.fieldWidth * 2f - 10f, control.height),
                ref vector.x,
                ref vector.y,
                minMaxSliderAttribute.Min,
                minMaxSliderAttribute.Max);

            vector.x = Mathf.Clamp(vector.x, minMaxSliderAttribute.Min, vector.y);
            vector.y = Mathf.Clamp(vector.y, vector.x, minMaxSliderAttribute.Max);

            if (!EditorGUI.EndChangeCheck())
                return;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                    property.vector2Value = vector;
                    break;
                case SerializedPropertyType.Vector2Int:
                    property.vector2IntValue = Vector2Int.FloorToInt(vector);
                    break;
            }
        }
    }
}