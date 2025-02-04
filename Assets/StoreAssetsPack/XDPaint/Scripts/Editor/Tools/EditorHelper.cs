﻿using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using XDPaint.Tools;

namespace XDPaint.Editor.Tools
{
    public static class EditorHelper
    {
        private static bool allowSavePresetsInRuntime = false;

        public static void MarkComponentAsDirty(Component component)
        {
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(component);
                EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
            }
        }

        public static void MarkAsDirty(Component component)
        {
            if (!Application.isPlaying && component != null)
            {
                EditorUtility.SetDirty(component);
            }
            if (!Application.isPlaying || Application.isPlaying && allowSavePresetsInRuntime)
            {
                EditorUtility.SetDirty(Settings.Instance);
            }
            if (!Application.isPlaying)
            {
                AssetSaveProcessor.SavePresets = true;
                AssetDatabase.SaveAssets();
                EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
            }
        }

        public static void DrawHorizontalLine()
        {
            GUILayout.Space(5f);
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 2f), Color.gray);
            GUILayout.Space(5f);
        }
    }
}