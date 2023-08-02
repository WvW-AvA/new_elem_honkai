using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using UnityEngine.Events;


public class GameManager : ManagerBase<GameManager>
{
    private Dictionary<GameMode.EGameMode, GameMode.Base> modeMap;
    public string gameManagerConfigPath = "Config/GameManagerConfig";
    public GameManager_SO gm_SO;
    public static GlobalParamConfig_SO globalParam
    {
        get
        {
            return Instance.gm_SO.GlobalParamConfig;
        }
    }
    [SerializeField]
    [ReadOnly]
    public GameMode.Base currentMode;
    private GameMode.EGameMode e_currentMode;
    override protected void Awake()
    {
        base.Awake();
        Log.GetLogHelper().Init();
        gm_SO = ResourceManager.GetResources<GameManager_SO>(gameManagerConfigPath);
        GlobalParamConfig_SO.GlobalParamConfigInit(gm_SO.GlobalParamConfig);
        modeMap = new Dictionary<GameMode.EGameMode, GameMode.Base>();
        currentMode = new GameMode.Base();
        e_currentMode = GameMode.EGameMode.Base;
        if (gm_SO == null)
        {
            Log.Error("{0} GameManagerConfig not found in {1}", LogColor.GameManager, gameManagerConfigPath);
        }
        modeMap.Add(GameMode.EGameMode.BeginMenuMode, gm_SO.BeginMenuConfig.modeConfig);
        modeMap.Add(GameMode.EGameMode.BattleMode, gm_SO.BattleModeConfig.modeConfig);
        modeMap.Add(GameMode.EGameMode.DialogMode, gm_SO.DialogModeConfig.modeConfig);
        modeMap.Add(GameMode.EGameMode.NormalMode, gm_SO.NormalModeConfig.modeConfig);
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += OnEditorPlay;
#endif
        var scene = SceneManager.GetActiveScene();
        if (!scene.path.StartsWith("Assets/Scenes/Real Use Scene/"))
            return;
        if (scene.buildIndex < 3)
            ChangeGameMode(GameMode.EGameMode.BeginMenuMode);
        else
            ChangeGameMode(GameMode.EGameMode.NormalMode);
    }

#if UNITY_EDITOR
    static void OnEditorPlay(PlayModeStateChange playMode)
    {
        if (playMode == PlayModeStateChange.EnteredEditMode)
            Log.GetLogHelper().Release();
        if (playMode == PlayModeStateChange.EnteredPlayMode)
        {
        }
    }
#endif
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void PreCreate()
    {
        Log.Info("{0} PreCreate in {1}", LogColor.GameManager, Instance.gameObject);
    }


    void Start()
    {

    }

    void Update()
    {
        if (currentMode != null)
        {
            currentMode.OnGameModeUpdate();
        }
    }

    public static void ChangeGameMode(GameMode.EGameMode mode)
    {
        if (Instance.e_currentMode == mode)
            return;
        Log.Info("{0} Change game mode from {1} to {2}", LogColor.GameManager, Instance.currentMode.modeType.ToString(), mode.ToString());
        Instance.currentMode.OnGameModeExit();
        Instance.currentMode = Instance.modeMap[mode];
        Instance.currentMode.OnGameModeEnter();
        Instance.e_currentMode = mode;
    }

    public static void LoadSceneAsync(string scene, UnityAction sceneLoadedCallback = null)
    {
        Instance.LoadSceneAsync_pri(scene, sceneLoadedCallback);
    }

    public static void GameExit()
    {
        Application.Quit();
    }
    private void LoadSceneAsync_pri(string scene, UnityAction sceneLoadedCallback)
    {
        StartCoroutine(LoadSceneAsync_Co(scene, sceneLoadedCallback));
    }
    private static IEnumerator LoadSceneAsync_Co(string scene, UnityAction sceneLoadedCallback)
    {
        // UIManager.ReleaseAllUI();
        var loadingUI = UIManager.CreateUI(UIConst.LoadingUI).GetComponent<UI_Loading>();
        UIManager.GetUIInstance<UI_Loading>(UIConst.LoadingUI).IsEnable = true;
        yield return new WaitForSeconds(1f);
        var op = SceneManager.LoadSceneAsync(scene);
        op.allowSceneActivation = false;
        while (op.progress < 0.8)
        {
            loadingUI.LoadingPercent = op.progress * 100;
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
        loadingUI.LoadingPercent = 100;
        while (op.isDone == false)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        UIManager.GetUIInstance<UI_Loading>(UIConst.LoadingUI).IsEnable = false;
        if (sceneLoadedCallback != null)
            sceneLoadedCallback.Invoke();
        Log.Info(LogColor.GameManager + "Scene " + LogColor.Dye(LogColor.EColor.purple, scene) + "loaded");
    }
}

public class GameConst
{
    public const string PlayerPrefab = "Prefabs/Player/Player";
    public const string CameraPrefab = "Prefabs/Main Camera";
}
