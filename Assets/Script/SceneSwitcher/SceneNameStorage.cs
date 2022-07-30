#if DEVELOPMENT_BUILD
/// <summary>
/// シーン移行のアシストツール用の情報格納クラス
/// </summary>
public class SceneNameStorage
{
    public string sceneName;

    public string displaySceneName;

    public SceneNameStorage(string sceneName, string displaySceneName)
    {
        this.sceneName = sceneName;
        this.displaySceneName = displaySceneName;
    }
}
#endif
