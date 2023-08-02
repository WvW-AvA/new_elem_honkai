using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

class UIManager : ManagerBase<UIManager>
{
    private static Stack<UI_Window> m_windowsStack = new Stack<UI_Window>();
    public static Stack<UI_Window> WindowsStack { get { return m_windowsStack; } }
    private static Dictionary<string, UI_Base> m_ui_dict = new Dictionary<string, UI_Base>();
    private static Dictionary<string, UI_Base> UI_Dict { get { return m_ui_dict; } }
    public GameObject UIRoot;
    public GameObject canvas;

    private static UI_Playing m_playingUI;
    public static UI_Playing playingUI
    {
        get
        {
            if (m_playingUI == null)
                m_playingUI = CreateUI(UIConst.PlayingUI).GetComponent<UI_Playing>();
            return m_playingUI;
        }
    }
    private static UI_Pause m_pauseUI;
    public static UI_Pause pauseUI
    {
        get
        {
            if (m_pauseUI == null)
                m_pauseUI = CreateUI(UIConst.PauseUI).GetComponent<UI_Pause>();
            return m_pauseUI;
        }
    }
    private static UI_BossDocker m_bossDocker;
    public static UI_BossDocker bossDocker
    {
        get
        {
            if (m_bossDocker == null)
                m_bossDocker = CreateUI(UIConst.BossHPDocker).GetComponent<UI_BossDocker>();
            return m_bossDocker;
        }
    }
    private static UI_Icon m_landmarkUI;
    public static UI_Icon landmarkUI
    {
        get
        {
            if (m_landmarkUI == null)
                m_landmarkUI = CreateUI(UIConst.LandmarkUI).GetComponent<UI_Icon>();
            return m_landmarkUI;
        }
    }

    private static EventSystem m_eventSystem;
    public static EventSystem eventSystem
    {
        get
        {
            if (m_eventSystem == null)
                m_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            return m_eventSystem;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        UIRoot = ResourceManager.GetInstance(UIConst.UIRootPath);
        DontDestroyOnLoad(UIRoot);
        canvas = UIRoot.transform.GetChild(0).gameObject;
        if (FindObjectsOfType<EventSystem>().Length > 1)
            ResourceManager.Release(UIRoot.transform.GetChild(1).gameObject);
    }

    private void Update()
    {
    }
    public static UI_Window PushBackWindow(string window_path)
    {
        var go = CreateUI(window_path);
        PushBackWindow(go.GetComponent<UI_Window>());
        return go.GetComponent<UI_Window>();
    }

    public static void PushBackWindow(UI_Window window)
    {
        if (WindowsStack.Count != 0)
        {
            if (WindowsStack.Peek() == window)
                return;
            WindowsStack.Peek().IsEnable = false;
        }
        WindowsStack.Push(window);
        WindowsStack.Peek().IsEnable = true;
    }
    /// <summary>
    /// 若是要创建一个窗口，请使用PushBackWindow而不是CreateUI。
    /// </summary>
    /// <param name="ui"></param>
    public static GameObject CreateUI(string ui_path)
    {
        if (UI_Dict.ContainsKey(ui_path))
            return UI_Dict[ui_path].gameObject;
        else
        {
            var go = ResourceManager.GetInstance(ui_path, Instance.canvas.transform);
            UI_Dict.Add(ui_path, go.GetComponent<UI_Base>());
            return go;
        }
    }
    public static void CreateUI(string ui_path, float delaytime)
    {
        QuickCoroutineSystem.StartCoroutine(Instance.creatUI_co(ui_path, delaytime));
    }

    private IEnumerator creatUI_co(string ui_path, float delay)
    {
        yield return new WaitForSeconds(delay);
        CreateUI(ui_path);
    }
    public static void ReleaseUI(string ui_path)
    {
        if (UI_Dict.ContainsKey(ui_path))
        {
            ResourceManager.Release(UI_Dict[ui_path].gameObject);
            UI_Dict.Remove(ui_path);
            if (UI_Dict.ContainsKey(ui_path))
                Log.Warning(LogColor.UIManager + "Release UI {0} error", ui_path);
            else
                Log.Info(LogColor.UIManager + "Release UI {0}", ui_path);
        }
        else
        {
            Log.Warning(LogColor.UIManager + "Releasing UI {0} but it is not exit, maybe it was released before.", ui_path);
        }
    }

    public static void ReleaseUI(string ui_path, float time)
    {
        QuickCoroutineSystem.StartCoroutine(Instance.releaseUI_co(ui_path, time));
    }
    IEnumerator releaseUI_co(string ui_path, float time)
    {
        yield return new WaitForSeconds(time);
        ReleaseUI(ui_path);
    }
    public static void ReleaseAllUI()
    {
        WindowsStack.Clear();
        List<string> keys = new List<string>(UI_Dict.Keys);
        foreach (var k in keys)
        {
            if (k != UIConst.LogWindow && k != UIConst.LoadingUI)
                ReleaseUI(k);
        }
    }
    public static void PopBackWindow()
    {
        if (WindowsStack.Count == 0)
        {
            return;
        }
        WindowsStack.Peek().IsEnable = false;
        WindowsStack.Pop();
        if (WindowsStack.Count != 0)
            WindowsStack.Peek().IsEnable = true;
    }
    public static T GetUIInstance<T>(string UIConst) where T : UI_Base
    {
        if (UI_Dict.ContainsKey(UIConst) == false)
        {
            return null;
        }
        return (T)UI_Dict[UIConst];
    }

    public static void CreateFloatStringIcon(string value, Vector3 pos, Color color, float keepTime = 0.5f)
    {
        GameObject obj = ResourceManager.GetInstance(UIConst.FloatString, Instance.canvas.transform);
        obj.transform.position = pos;
        var icon = obj.GetComponent<FloatString>();
        icon.stringValue = value;
        icon.color = color;
        icon.stateFlag = 1;
        QuickCoroutineSystem.StartCoroutine(Instance.release_FloatStringIcon_co(icon, keepTime));
    }
    private IEnumerator release_FloatStringIcon_co(FloatString target, float t0)
    {
        yield return new WaitForSeconds(t0);
        target.stateFlag = -1;
        yield return new WaitForSeconds(target.effectTime);
        ResourceManager.Release(target.gameObject);
    }

    public static void CreateGuideUI(GuideUIParam param)
    {
        var gm = CreateUI(UIConst.GuideUI);
        var ui = gm.GetComponent<UI_Guide>();
        ui.SetValue(param);
        ui.IsEnable = true;
    }
}

class UIConst
{
    public static readonly string UIRootPath = "Prefabs/UI/UIRoot";
    public static readonly string LoadingUI = "Prefabs/UI/LoadingUI/LoadingScene";
    public static readonly string PlayingUI = "Prefabs/UI/PlayingUI/PlayingUI";
    public static readonly string GuideUI = "Prefabs/UI/PlayingUI/GuideUI";
    public static readonly string LandmarkUI = "Prefabs/UI/PlayingUI/LandmarkUI";
    public static readonly string PauseUI = "Prefabs/UI/SetSceneUI/PauseUI";
    public static readonly string BossHPDocker = "Prefabs/UI/PlayingUI/BossHPDockerUI";

    public static readonly string FloatString = "Prefabs/MapObj/FloatString";

    public static readonly string FileSelectWindow = "Prefabs/UI/FileSelect/FileSelectWindow";
    public static readonly string SureWindow = "Prefabs/UI/FileSelect/SureWindow";
    public static readonly string LogWindow = "Prefabs/UI/LogWindow/LogWindow";
    public static readonly string BeginMenu = "Prefabs/UI/Login/BeginMenu";
}
