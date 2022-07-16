using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// シーン移行のアシストツールクラスのinspector編集クラス
/// </summary>
#if DEVELOPMENT_BUILD
[CustomEditor(typeof(SceneSwitcher))]
public class SceneSwitcherEditor : Editor
{
    private Vector2 scrollPosition = Vector2.zero;

    private SceneSwitcher switcher;
    public void OnEnable()
    {
        switcher = target as SceneSwitcher;
    }

    public override void OnInspectorGUI()
    {
        switcher.Dropdown = EditorGUILayout.ObjectField("ドロップボックス", switcher.Dropdown, typeof(TMP_Dropdown), true) as TMP_Dropdown;

        GUILayout.Space(10);

        EditorGUILayout.LabelField("シーンリスト");

        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUI.skin.scrollView);
            {
                for(int i = 0; i < switcher.SceneNameList.Count; i++)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    {
                        EditorGUILayout.LabelField("シーン名", switcher.SceneNameList[i].sceneName);
                        switcher.SceneNameList[i].displaySceneName = EditorGUILayout.TextField("表示名", switcher.SceneNameList[i].displaySceneName);
                        GUILayout.Space(5);
                    }
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(5);
                }
            }
            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndVertical();
    }

    
    public void OnDestroy()
    {
        switcher.Save();
    }
}
#endif