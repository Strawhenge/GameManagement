using UnityEditor;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Editor
{
    static class GameManagementSettingsProvider
    {
        const string SettingsPath = "Project/Strawhenge/Game Management";
        const string DefaultAssetPath = "Assets/Game Management Settings.asset";

        [SettingsProvider]
        static SettingsProvider CreateProvider()
        {
            return new SettingsProvider(SettingsPath, SettingsScope.Project)
            {
                guiHandler = OnGUI,
                keywords = new[] { "Game Management", "Scenes" }
            };
        }

        static void OnGUI(string searchContext)
        {
            var settings = GetSettingsAsset();
            if (settings == null)
            {
                DrawMissingSettingsUI();
                return;
            }

            DrawSettingsUI(settings);
        }

        static void DrawMissingSettingsUI()
        {
            EditorGUILayout.HelpBox(
                "No Game Management settings asset was found. Create one inside an Assets/**/Resources folder.",
                MessageType.Info);

            if (GUILayout.Button("Create Settings Asset"))
            {
                var settings = CreateSettingsAssetAtUserPath();
                if (settings != null)
                {
                    Selection.activeObject = settings;
                    EditorGUIUtility.PingObject(settings);
                }
            }
        }

        static void DrawSettingsUI(GameManagementSettingsScriptableObject settings)
        {
            var serializedObject = new SerializedObject(settings);
            serializedObject.Update();

            EditorGUILayout.LabelField("Game Management Settings", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(
                serializedObject.FindProperty(GameManagementSettingsScriptableObject.MainMenuSceneFieldName),
                new GUIContent("Main Menu Scene"));

            EditorGUILayout.PropertyField(
                serializedObject.FindProperty(GameManagementSettingsScriptableObject.LoadingScreenSceneFieldName),
                new GUIContent("Loading Screen Scene"));

            EditorGUILayout.PropertyField(
                serializedObject.FindProperty(GameManagementSettingsScriptableObject.GameSceneFieldName),
                new GUIContent("Game Scene"));

            EditorGUILayout.PropertyField(
                serializedObject.FindProperty(GameManagementSettingsScriptableObject.GameManagerPrefabFieldName),
                new GUIContent("Game Manager Prefab"));

            var changed = serializedObject.ApplyModifiedProperties();
            if (changed)
            {
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.Space(8f);

            if (GUILayout.Button("Select Settings Asset"))
            {
                Selection.activeObject = settings;
                EditorGUIUtility.PingObject(settings);
            }
        }

        static GameManagementSettingsScriptableObject GetSettingsAsset()
        {
            var settingsGuids = AssetDatabase.FindAssets($"t:{nameof(GameManagementSettingsScriptableObject)}");
            if (settingsGuids.Length > 0)
            {
                var settingsPath = AssetDatabase.GUIDToAssetPath(settingsGuids[0]);
                return AssetDatabase.LoadAssetAtPath<GameManagementSettingsScriptableObject>(settingsPath);
            }

            return null;
        }

        static GameManagementSettingsScriptableObject CreateSettingsAssetAtUserPath()
        {
            string chosenPath;
            while (true)
            {
                chosenPath = EditorUtility.SaveFilePanelInProject(
                    "Create Game Management Settings",
                    "GameManagementSettings",
                    "asset",
                    "Choose where to save the Game Management settings asset.",
                    DefaultAssetPath);

                if (string.IsNullOrWhiteSpace(chosenPath))
                {
                    return null;
                }

                if (IsInResourcesFolder(chosenPath))
                {
                    break;
                }

                var chooseAgain = EditorUtility.DisplayDialog(
                    "Invalid Settings Location",
                    "GameManagementSettings must be created inside an Assets/**/Resources folder.",
                    "Choose Again",
                    "Cancel");

                if (!chooseAgain)
                {
                    return null;
                }
            }

            var settings = ScriptableObject.CreateInstance<GameManagementSettingsScriptableObject>();
            AssetDatabase.CreateAsset(settings, chosenPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return settings;
        }

        static bool IsInResourcesFolder(string assetPath)
        {
            if (string.IsNullOrWhiteSpace(assetPath) || !assetPath.StartsWith("Assets/"))
            {
                return false;
            }

            return assetPath.StartsWith("Assets/Resources/") || assetPath.Contains("/Resources/");
        }
    }
}