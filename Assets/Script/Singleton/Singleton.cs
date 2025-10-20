// このクラスを継承してシングルトンを実装する
// 例: public class GameManager : Singleton<GameManager> { }

// シーンを跨いで保持したい場合は、継承先で IsPersistent プロパティを true にする
// 例: protected override bool IsPersistent => true;

using UnityEngine;

/// <summary>
/// 継承して使うシングルトンの基底クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    private static readonly object _lock = new object();

    // 継承先で上書き可能
    // デフォルトではシーンを跨いで保持される
    protected virtual bool IsPersistent => true;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                        SetupInstance();
                }
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        RemoveDuplicates();
    }

    private static void SetupInstance()
    {
        instance = FindFirstInstance();
        if (instance == null)
        {
            GameObject gameObj = new GameObject(typeof(T).Name);
            instance = gameObj.AddComponent<T>();
        }

        // この段階では IsPersistent にアクセスできないので、
        // RemoveDuplicates 側で判定を行う
    }

    private void RemoveDuplicates()
    {
        if (instance == null)
        {
            instance = this as T;
            if (IsPersistent)
                DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private static T FindFirstInstance()
    {
#if UNITY_2023_1_OR_NEWER
        return Object.FindFirstObjectByType<T>();
#else
        return Object.FindObjectOfType<T>();
#endif
    }
}
