using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                _instance?.TryInit();
            }

            return _instance;
        }
    }

    private static T _instance;
    private bool _isSingletonInitialized;

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning($"Multiple instances of {nameof(T)} found. Destroying the new one.");
            Destroy(gameObject);
            return;
        }

        TryInit();
    }

    /// <summary>
    /// Called when the instance is created, either in Awake() or instance call.
    /// </summary>
    protected virtual void InitializeSingleton() {}

    protected void TryInit()
    {
        if (_isSingletonInitialized)
            return;

        _instance = (T)this;
        InitializeSingleton();
        _isSingletonInitialized = true;
    }
}
