using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditor.Build;
using System;

/// <summary>
/// DefineSymbolを編集するクラス
/// </summary>
public class SettingSymbol
{
    private const string SYMBOL_SEPARATOR = ";";

    private const string SYMBOL_VALUE_SAVE_KEY_FORMAT = "SymbolValueKeyFor{0}";

    private static List<DefineSymbol> symbolList = new List<DefineSymbol>();

    private static List<DefineSymbol> oldSymbolList = new List<DefineSymbol>();

    public static List<DefineSymbol> SymbolList
    {
        get
        {
            return symbolList;
        }
    }

    private static BuildTargetGroup targetGroup;

    private static int profileIndex;

    public static int ProfileIndex { get { return profileIndex; } set { profileIndex = value; } }

    [MenuItem("Window/DefineSymbolsEditor")]
    public static void Open()
    {
        ScriptingDefineWindow.GetWindow( typeof(ScriptingDefineWindow), false, "DefineSymbolsEditor");
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public static void Init() 
    {
        symbolList.Clear();
        oldSymbolList.Clear();
        targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        var values = Enum.GetValues(typeof(BuildTargetGroup));
        profileIndex = Array.IndexOf(values, targetGroup);

        EditSymbolList();

    }

    /// <summary>
    /// プロファイルが変更
    /// </summary>
    public static void ChangeProfile()
    {
        symbolList.Clear();
        oldSymbolList.Clear();
        var values = Enum.GetValues(typeof(BuildTargetGroup));
        var i = values.GetValue(profileIndex);
        targetGroup = (BuildTargetGroup)i;

        EditSymbolList();
    }

    /// <summary>
    /// DefineSymbolを追加
    /// </summary>
    /// <param name="key">Define名</param>
    public static void Add(string key)
    {
        DefineSymbol symbol = new DefineSymbol(key, false);

        symbolList.Add(symbol);
    }

    /// <summary>
    /// DefineSymbolを削除
    /// </summary>
    /// <param name="key">Define名</param>
    public static void Delete(DefineSymbol defineSymbol)
    {
        symbolList.Remove(defineSymbol);
    }

    /// <summary>
    /// DefineSymbolの状態をScriptingDefineSymbolに保存
    /// </summary>
    public static void Save()
    {
        List<string> keyList = new List<string>();

        foreach (var symbol in symbolList)
        {
            if(symbol.IsEnable)
            {
                keyList.Add(symbol.Key);
            }
        }

        string str = String.Join(";", keyList);

        PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(targetGroup), str);

        oldSymbolList.Clear();
        foreach (var symbol in symbolList)
        {
            DefineSymbol defineSymbol = new DefineSymbol(symbol.Key, symbol.IsEnable);
            oldSymbolList.Add(defineSymbol);
        }
    }

    public static void Reset()
    {
        symbolList.Clear();
        foreach (var symbol in oldSymbolList)
        {
            DefineSymbol defineSymbol = new DefineSymbol(symbol.Key, symbol.IsEnable);
            symbolList.Add(defineSymbol);
        }
    }

    /// <summary>
    /// DefineSymbolの状態をEditorUserSettingsに保存
    /// </summary>
    public static void SaveForUserSetting()
    {
        List<string> keyList = new List<string>();

        foreach (var symbol in oldSymbolList)
        {
            keyList.Add(symbol.Key);
        }

        string saveKey = string.Format(SYMBOL_VALUE_SAVE_KEY_FORMAT, targetGroup.ToString());

        string str = String.Join(";", keyList);

        EditorUserSettings.SetConfigValue(saveKey, str);
    }

    public static bool isEdit()
    {
        if(symbolList.Count != oldSymbolList.Count)
        {
            return true;
        }

        for(int i = 0; i < symbolList.Count; i++)
        {
            if(symbolList[i].IsEnable != oldSymbolList[i].IsEnable)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// EditorUserSettingsやScriptingDefineSymbolからSymbolListを作成
    /// </summary>
    private static void EditSymbolList()
    {
        List<string> symbolKeyList = new List<string>();

        string[] settingSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup).Split(new[] { SYMBOL_SEPARATOR }, StringSplitOptions.None);
        symbolKeyList.AddRange(settingSymbols.ToList());

        string loadKey = string.Format(SYMBOL_VALUE_SAVE_KEY_FORMAT, targetGroup.ToString());

        string savingSymbolKeys = EditorUserSettings.GetConfigValue(loadKey);
        List<string> saveKeyList = new List<string>();

        if (!string.IsNullOrEmpty(savingSymbolKeys))
        {
            saveKeyList = new List<string>(savingSymbolKeys.Split(new[] { SYMBOL_SEPARATOR }, StringSplitOptions.None));
        }
        symbolKeyList.AddRange(saveKeyList);

        symbolKeyList = symbolKeyList.Where(symbolKey => !string.IsNullOrEmpty(symbolKey)).Distinct().ToList();
        symbolKeyList.Sort();

        foreach (var key in symbolKeyList)
        {
            bool isEnable = settingSymbols.Contains(key);

            DefineSymbol symbol = new DefineSymbol(key, isEnable);

            symbolList.Add(symbol);
        }

        foreach (var symbol in symbolList)
        {
            DefineSymbol defineSymbol = new DefineSymbol(symbol.Key, symbol.IsEnable);
            oldSymbolList.Add(defineSymbol);
        }
    }
}