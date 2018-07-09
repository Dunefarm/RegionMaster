// --------------------------------
// <copyright file="HideableAttributeDrawer.cs" company="Rumor Games">
//     Copyright (C) Rumor Games, LLC.  All rights reserved.
// </copyright>
// --------------------------------

using UnityEditor;
using UnityEngine;

namespace ExtendedEditor
{
    /// <summary>
    /// HideableAttributeDrawer class.
    /// </summary>
    public abstract class HideableAttributeDrawer : PropertyDrawer
    {
        /// <summary>
        /// Draws the GUI for the property.
        /// </summary>
        /// <param name="position">Rectangle on the screen to use for the property GUI.</param>
        /// <param name="property">The SerializedProperty to make the custom GUI for.</param>
        /// <param name="label">The label of this property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!this.IsSupposedToBeHidden(property))
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        /// <summary>
        /// Get the property height in pixels of the given property.
        /// </summary>
        /// <param name="property">The SerializedProperty to get height for.</param>
        /// <param name="label">The label of this property.</param>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return this.IsSupposedToBeHidden(property) ? 0f : base.GetPropertyHeight(property, label);
        }

        /// <summary>
        /// Checks whether the property is supposed to be hidden.
        /// </summary>
        /// <param name="property">The SerializedProperty to test.</param>
        /// <returns>True if the property should be hidden.</returns>
        protected abstract bool IsSupposedToBeHidden(SerializedProperty property);
    }

    /// <summary>
    /// HideUnlessAttribute class.
    /// </summary>
    public class ShowIfAttribute : PropertyAttribute
    {
        /// <summary>
        /// Initializes a new instance of the HideUnlessAttribute class.
        /// </summary>
        /// <param name="fieldName">Field that controls whether this property is hidden or visible.</param>
        public ShowIfAttribute(string fieldName)
        {
            this.FieldName = fieldName;
        }

        /// <summary>
        /// Gets the name of the field this attribute's drawer will test to see whether to show in the inspector.
        /// </summary>
        public string FieldName { get; private set; }
    }

    public class HideIfAttribute : PropertyAttribute
    {
        /// <summary>
        /// Initializes a new instance of the HideUnlessAttribute class.
        /// </summary>
        /// <param name="fieldName">Field that controls whether this property is hidden or visible.</param>
        public HideIfAttribute(string fieldName)
        {
            this.FieldName = fieldName;
        }

        /// <summary>
        /// Gets the name of the field this attribute's drawer will test to see whether to show in the inspector.
        /// </summary>
        public string FieldName { get; private set; }
    }

    /// <summary>
    /// HideUnlessAttributeDrawer class.
    /// </summary>
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfAttributeDrawer : HideableAttributeDrawer
    {
        /// <summary>
        /// Gets the HideUnlessAttribute to draw.
        /// </summary>
        private ShowIfAttribute Attribute
        {
            get
            {
                return (ShowIfAttribute)this.attribute;
            }
        }

        /// <summary>
        /// Checks whether the property is supposed to be hidden.
        /// </summary>
        /// <param name="property">The SerializedProperty to test.</param>
        /// <returns>True if the property should be hidden.</returns>
        protected override bool IsSupposedToBeHidden(SerializedProperty property)
        {
            var attributeProperty = property.serializedObject.FindProperty(this.Attribute.FieldName);
            if(attributeProperty == null)
            {
                Debug.LogError("ShowIf attribute is referencing an invalid field name: " + this.Attribute.FieldName);
                return false;
            }

            if (attributeProperty.propertyType == SerializedPropertyType.Boolean)
            {
                return !attributeProperty.boolValue;
            }

            Debug.LogError("ShowIf attribute is referencing an invalid field name: " + this.Attribute.FieldName);
            return false;
        }
    }

    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public class HideIfAttributeDrawer : HideableAttributeDrawer
    {
        /// <summary>
        /// Gets the HideUnlessAttribute to draw.
        /// </summary>
        private HideIfAttribute Attribute
        {
            get
            {
                return (HideIfAttribute)this.attribute;
            }
        }

        /// <summary>
        /// Checks whether the property is supposed to be hidden.
        /// </summary>
        /// <param name="property">The SerializedProperty to test.</param>
        /// <returns>True if the property should be hidden.</returns>
        protected override bool IsSupposedToBeHidden(SerializedProperty property)
        {
            var attributeProperty = property.serializedObject.FindProperty(this.Attribute.FieldName);
            if (attributeProperty.propertyType == SerializedPropertyType.Boolean)
            {
                return attributeProperty.boolValue;
            }

            Debug.LogWarning("ShowIf attribute is referencing an invalid field name: " + this.Attribute.FieldName);
            return true;
        }
    }
}