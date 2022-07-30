using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditor.Build;
using System;

/// <summary>
/// Define�𑀍삷���ʂ�`�悷��N���X
/// </summary>
public class ScriptingDefineWindow : EditorWindow
{
    private string defineName;

    private Vector2 scrollPosition = Vector2.zero;

    /// <summary>
    /// �I�u�W�F�N�g���A�N�e�B�u�ɂȂ�����
    /// </summary>
    private void OnEnable()
    {
        SettingSymbol.Init();
    }

    /// <summary>
    /// �`����e
    /// </summary>
    void OnGUI()
    {
        if (EditorApplication.isPlaying || Application.isPlaying || EditorApplication.isCompiling)
        {
            EditorGUILayout.HelpBox("�R���p�C�����A���s���͕ύX�ł��܂���", MessageType.Warning);
            return;
        }
        else if (SettingSymbol.isEdit())
        {
            EditorGUILayout.HelpBox("�ۑ����Ă��Ȃ��ҏW���ꂽ�f�[�^������܂�", MessageType.Info);
        }
        else
        {
            EditorGUILayout.HelpBox("", MessageType.None);
        }

        EditorGUILayout.BeginHorizontal();
        {
            SettingSymbol.ProfileIndex = EditorGUILayout.Popup(SettingSymbol.ProfileIndex, Enum.GetNames(typeof(BuildTargetGroup)));

            if (GUILayout.Button("�؂�ւ�"))
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
                        if (GUILayout.Button("- �폜", GUILayout.Width(100)))
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
                if (GUILayout.Button("+ �ǉ�"))
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
            if (GUILayout.Button("���Z�b�g"))
            {
                SettingSymbol.Reset();
            }

            if (GUILayout.Button("�ۑ�"))
            {
                SettingSymbol.Save();
            }
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(5);
    }

    /// <summary>
    /// window������ꂽ��
    /// </summary>
    void OnDestroy()
    {
        SettingSymbol.SaveForUserSetting();
    }
}
