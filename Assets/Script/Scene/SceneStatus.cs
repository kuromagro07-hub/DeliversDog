using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンの状態を表す列挙型
/// (シーン数と揃える)
/// </summary>
public enum SceneState
{
    Title,
    Game,
    Result
}

/// <summary>
/// シーン状態を管理するクラス
/// </summary>
public class SceneStatus : Singleton<SceneStatus>
{
    /// <summary>
    /// 列挙型でシーンを切り替える
    /// </summary>
    /// <param name="newState"></param>
    public void SceneChange(SceneState newState)
    {
        string currentSceneName = SceneManager.GetSceneByBuildIndex((int)newState).name;
        SceneManager.LoadScene(currentSceneName);
    }
}
