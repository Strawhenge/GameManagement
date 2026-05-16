#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Strawhenge.GameManagement.Unity.Tests.SaveData
{
    [CustomEditor(typeof(InMemorySaveDataRepositoryScript))]
    public class InMemorySaveDataRepositoryScriptEditor : Editor
    {
        InMemorySaveDataRepositoryScript _target;
        bool _listSaveData;

        void OnEnable()
        {
            _target = (InMemorySaveDataRepositoryScript)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginDisabledGroup(!Application.isPlaying);

            _listSaveData = EditorGUILayout.Foldout(_listSaveData, "Save Data");
            if (_listSaveData)
            {
                foreach (var (saveMetaData, saveData) in _target.Repository.GetAll())
                {
                    EditorGUILayout.LabelField($"Id: {saveMetaData.Id}", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField($"DateTimeCreated: {saveMetaData.DateTimeCreated}");
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField($"PlayerPosition: {saveData.PlayerPosition}");
                    EditorGUILayout.LabelField($"SecondsToWait: {saveData.SecondsToWait}");

                    if (GUILayout.Button("Delete"))
                        _target.Repository.Delete(saveMetaData.Id);

                    EditorGUILayout.Space();
                }
            }

            EditorGUI.EndDisabledGroup();
        }
    }
}
#endif