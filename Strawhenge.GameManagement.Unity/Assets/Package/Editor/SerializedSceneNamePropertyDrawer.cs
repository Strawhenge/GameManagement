using Strawhenge.GameManagement.Unity.Setup;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Editor
{
    [CustomPropertyDrawer(typeof(SerializedSceneName))]
    public class SerializedSceneNamePropertyDrawer : PropertyDrawer
    {
        const string NoneOption = "(None)";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var nameProperty = property.FindPropertyRelative(SerializedSceneName.NameFieldName);
            var currentName = nameProperty.stringValue;

            var sceneOptions = GetSceneOptions();

            var displayOptions = new List<string> { NoneOption };
            displayOptions.AddRange(sceneOptions);

            var displayIndex = string.IsNullOrEmpty(currentName)
                ? 0
                : sceneOptions.IndexOf(currentName) + 1;

            if (displayIndex < 0) displayIndex = 0;

            EditorGUI.BeginProperty(position, label, property);

            var selectedIndex = EditorGUI.Popup(position, label.text, displayIndex, displayOptions.ToArray());

            nameProperty.stringValue = selectedIndex == 0 ? string.Empty : displayOptions[selectedIndex];

            EditorGUI.EndProperty();
        }

        static List<string> GetSceneOptions()
        {
            var scenes = new List<string>();

            foreach (var scene in EditorBuildSettings.scenes)
                scenes.Add(Path.GetFileNameWithoutExtension(scene.path));

            return scenes;
        }
    }
}