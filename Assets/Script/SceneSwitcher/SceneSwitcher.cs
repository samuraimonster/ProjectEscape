using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// シーン移行のアシストツールクラス
/// </summary>
[ExecuteInEditMode]
public class SceneSwitcher : MonoBehaviour
{
#if DEVELOPMENT_BUILD
    private List<SceneNameStorage> sceneNameList = new List<SceneNameStorage>();

    public List<SceneNameStorage> SceneNameList { get { return sceneNameList; } set { sceneNameList = value; } }
#endif
    private const string SAVE_SCENE_KEY = "SaveSceneNameKeyFor{0}";

    [SerializeField]
    private TMP_Dropdown dropdown;

    public TMP_Dropdown Dropdown { get { return dropdown; } set { dropdown = value; } }

    private bool isInit = false;
    void Start()
    {
#if DEVELOPMENT_BUILD
        var obj = transform.parent.gameObject;
        var canvasGroup = obj.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        if (sceneNameList.Count == 0)
        {
            Init();
        }
#else
        var obj = transform.parent.gameObject;
        var canvasGroup = obj.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
#endif
    }

#if DEVELOPMENT_BUILD
    /// <summary>
    /// 初期動作
    /// </summary>
    public void Init()
    {
        if(dropdown == null)
        {
            return;
        }
        dropdown.ClearOptions();
        sceneNameList.Clear();
        string[] names = CuttingSceneName(EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes)).ToArray();
        for (int i = 0; i < names.Length; i++)
        {
            string loadKey = string.Format(SAVE_SCENE_KEY, names[i]);
            string str = EditorUserSettings.GetConfigValue(loadKey);
            if (!string.IsNullOrEmpty(str))
            {
                sceneNameList.Add(new SceneNameStorage(names[i], str));
            }
            else
            {
                sceneNameList.Add(new SceneNameStorage(names[i], names[i]));
            }
        }
        var list = new List<string>();
        foreach (var scene in sceneNameList)
        {
            list.Add(scene.displaySceneName);
        }
        dropdown.AddOptions(list);
        dropdown.value = SceneManager.GetActiveScene().buildIndex;
    }

    public void Update()
    {
        if(!isInit)
        {
            Init();
            isInit = true;
        }
        UpdateList();
        Save();
    }

    /// <summary>
    /// dropdownの値が変わった時
    /// </summary>
    public void OnChanged()
    {
        SceneManager.LoadSceneAsync(sceneNameList[dropdown.value].sceneName);
    }

    /// <summary>
    /// ファイルパス形式のscene名をscene名だけ切り取る
    /// </summary>
    /// <param name="arr">sceneのファイルパス</param>
    /// <returns></returns>
    private List<string> CuttingSceneName(string[] arr)
    {
        List<string> list = new();
        foreach(string str in arr)
        {
            int startPos = str.LastIndexOf("/") + 1;
            int endPos = str.LastIndexOf(".");

            list.Add(str[startPos..endPos]);
        }
        return list;
    }

    /// <summary>
    /// sceneNameListをアップデートする
    /// </summary>
    public void UpdateList()
    {
        if (dropdown == null)
        {
            return;
        }
        int i = dropdown.value;

        dropdown.ClearOptions();
        var list = new List<string>();
        foreach (var scene in sceneNameList)
        {
            list.Add(scene.displaySceneName);
        }
        dropdown.AddOptions(list);
        dropdown.value = i;
    }

    /// <summary>
    /// 情報をセーブする
    /// </summary>
    public void Save()
    {
        foreach (var scene in sceneNameList)
        {
            string saveKey = string.Format(SAVE_SCENE_KEY, scene.sceneName);
            EditorUserSettings.SetConfigValue(saveKey, scene.displaySceneName);
        }
    }

    public void OnDisable()
    {
        Save();
    }
#endif
}

