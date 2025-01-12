using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [SerializeField] UIPanelTransition sceneLoaderPanel = null;

    public event Action OnStartLoadScene;
    public event Action OnEndLoadScene;

    string sceneToLoad;

    int sceneToLoadIndex;

    bool loader;

    public bool Loading { get => loader; }

    bool isBuildIndex;

    public void LoadScene(string _sceneToLoad)
    {
        if (loader) return;

        sceneToLoad = _sceneToLoad;
        isBuildIndex = false;
        loader = true;
        sceneLoaderPanel.Open();
    }

    public void LoadScene(int _sceneToLoad)
    {
        if (loader) return;

        sceneToLoadIndex = _sceneToLoad;
        isBuildIndex = true;
        loader = true;
        sceneLoaderPanel.Open();
    }

    public static void Load(int _sceneToLoad)
    {
        Instance.LoadScene(_sceneToLoad);
    }

    public static void Load(string _sceneToLoad)
    {
        Instance.LoadScene(_sceneToLoad);
    }

    public void StartLoad()
    {
        OnStartLoadScene?.Invoke();
        var async = isBuildIndex ? SceneManager.LoadSceneAsync(sceneToLoadIndex) : SceneManager.LoadSceneAsync(sceneToLoad);
        async.completed += OnEndLoad;
    }

    public static void DEBUG_Reload_This_Scene()
    {
        Instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnEndLoad(AsyncOperation async)
    {
        sceneLoaderPanel.Close();
        OnEndLoadScene?.Invoke();
    }

    public void EndLoad()
    {
        loader = false;
    }

    private void Start()
    {
        sceneLoaderPanel.Close();
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
        }

    }
}
