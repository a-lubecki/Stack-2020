using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KevinCastejon.ConeMesh
{
    [CustomEditor(typeof(Cone))]
    public class ConeEditor : Editor
    {
        private SerializedProperty _pivotAtTop;
        private SerializedProperty _orientation;
        private SerializedProperty _invertDirection;
        private SerializedProperty _isTrigger;
        private SerializedProperty _material;
        private SerializedProperty _coneSides;
        private SerializedProperty _proportionalRadius;
        private SerializedProperty _coneRadius;
        private SerializedProperty _coneHeight;

        private Cone _script;

        private void OnEnable()
        {
            _pivotAtTop = serializedObject.FindProperty("_pivotAtTop");
            _orientation = serializedObject.FindProperty("_orientation");
            _invertDirection = serializedObject.FindProperty("_invertDirection");
            _isTrigger = serializedObject.FindProperty("_isTrigger");
            _material = serializedObject.FindProperty("_material");
            _coneSides = serializedObject.FindProperty("_coneSides");
            _proportionalRadius = serializedObject.FindProperty("_proportionalRadius");
            _coneRadius = serializedObject.FindProperty("_coneRadius");
            _coneHeight = serializedObject.FindProperty("_coneHeight");

            _script = (Cone)target;
            if (!_script.IsConeGenerated)
            {
                _script.GenerateCone();
            }
        }

        public override void OnInspectorGUI()
        {
            bool changed;
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_pivotAtTop);
            EditorGUILayout.PropertyField(_orientation);
            EditorGUILayout.PropertyField(_invertDirection);
            EditorGUILayout.PropertyField(_isTrigger);
            EditorGUILayout.PropertyField(_material);
            EditorGUILayout.PropertyField(_coneSides);
            EditorGUILayout.PropertyField(_proportionalRadius);
            EditorGUILayout.PropertyField(_coneRadius);
            EditorGUILayout.PropertyField(_coneHeight);
            changed = EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();
            if (changed)
            {
                _script.GenerateCone();
            }
        }
        // Add a menu item to create custom GameObjects.
        // Priority 1 ensures it is grouped with the other menu items of the same kind
        // and propagated to the hierarchy dropdown and hierarchy context menus.
        [MenuItem("GameObject/3D Object/Cone", false, 10)]
        static void CreateCustomGameObject(MenuCommand menuCommand)
        {
            // Create a custom game object
            GameObject go = new GameObject("Cone");
            // Add Cone component
            Cone cone = go.AddComponent<Cone>();
            // Set default lit material
            cone.Material = new Material(Shader.Find("Unlit/Color"));
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}
