using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditor.Build;
using System;

/// <summary>
/// Defineを操作する画面を描画するクラス
/// </summary>
public class ScriptingDefineWindow : EditorWindow
{
    private string defineName;

    private Vector2 scrollPosition = Vector2.zero;

    /// <summary>
    /// オブジェクトがアクティブになった時
    /// </summary>
    private void OnEnable()
    {
        SettingSymbol.Init();
    }

    /// <summary>
    /// 描画内容
    /// </summary>
    void OnGUI()
    {
        if (EditorApplication.isPlaying || Application.isPlaying || EditorApplication.isCompiling)
        {
            EditorGUILayout.HelpBox("コンパイル中、実行中は変更できません", MessageType.Warning);
            return;
        }
        else if (SettingSymbol.isEdit())
        {
            EditorGUILayout.HelpBox("保存していない編集されたデータがあります", MessageType.Info);
        }
        else
        {
            EditorGUILayout.HelpBox("", MessageType.None);
        }

        EditorGUILayout.BeginHorizontal();
        {
            SettingSymbol.ProfileIndex = EditorGUILayout.Popup(SettingSymbol.ProfileIndex, Enum.GetNames(typeof(BuildTargetGroup)));

            if (GUILayout.Button("切り替え"))
            {
                SettingSymbol.SaveForUserSetting();
                SettingSymbol.ChangeProfile();
            }
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);
        

        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUI.skin.scrollView);
            {
                foreach (var symbol in SettingSymbol.SymbolList)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    {
                        if (GUILayout.Button("- 削除", GUILayout.Width(100)))
                        {

                            SettingSymbol.Delete(symbol);
                            break;
                        }
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField(symbol.Key);
                            if (!string.IsNullOrEmpty(symbol.Key))
                            {
                                symbol.IsEnable = EditorGUILayout.Toggle(symbol.IsEnable, GUILayout.Width(15));
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(5);
                }
            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal();
            {
                defineName = EditorGUILayout.TextField("", defineName);
                if (GUILayout.Button("+ 追加"))
                {
                    if (defineName != "")
                    {
                        SettingSymbol.Add(defineName);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("リセット"))
            {
                SettingSymbol.Reset();
            }

            if (GUILayout.Button("保存"))
            {
                SettingSymbol.Save();
            }
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(5);
    }

    /// <summary>
    /// windowが閉じられた時
    /// </summary>
    void OnDestroy()
    {
        SettingSymbol.SaveForUserSetting();
    }
}
