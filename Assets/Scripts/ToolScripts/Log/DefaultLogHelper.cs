using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Diagnostics;

class DefaultLogHelper : Log.ILogHelper
{
    public Dictionary<string, string> BindDataLogs = new Dictionary<string, string>();
    public List<string> consoleLogs = new List<string>();
    public UI_LogWindow logWindow;
    public bool isUseLogWindow = false;
    public bool isUseConsoleLog = false;
    public void Init()
    {
        if (isUseConsoleLog)
            Console.Init();
        if (isUseLogWindow)
            logWindow = (UI_LogWindow)UIManager.PushBackWindow(UIConst.LogWindow);
    }
    public void Release()
    {
        if (isUseConsoleLog)
            Console.OnDestroy();
    }

    public void Log(LogLevel level, object message)
    {
        if (level == LogLevel.Debug)
        {
            UnityEngine.Debug.Log(Utility.Text.Format("<color=#888888>{0}</color>", message));
        }
        else if (level == LogLevel.Error)
        {
            UnityEngine.Debug.LogError(message.ToString());
        }
        else if (level == LogLevel.Info)
        {
            UnityEngine.Debug.Log(message.ToString());
        }
        else if (level == LogLevel.Warning)
        {
            UnityEngine.Debug.LogWarning(message.ToString());
        }
    }


    public void UILogDataRemove(string key)
    {
        if (isUseLogWindow == false)
            return;
        if (BindDataLogs.ContainsKey(key))
            BindDataLogs.Remove(key);
    }

    public void UILogDataUpdate(string key, object value)
    {
        if (isUseLogWindow == false)
            return;
        if (BindDataLogs.ContainsKey(key) == false)
            BindDataLogs.Add(key, value.ToString());
        else
            BindDataLogs[key] = value.ToString();
        // ConsoleWrite();
        UILogWrite();
    }

    public void UILog(string value)
    {
        if (isUseLogWindow == false)
            return;
        consoleLogs.Add(value);
        //  ConsoleWrite();
        UILogWrite();
    }

    private void UILogWrite()
    {
        if (isUseLogWindow == false)
            return;
        logWindow.Clear();
        string str = "";
        int size = Mathf.Min(10, consoleLogs.Count);
        for (int i = consoleLogs.Count - size; i < consoleLogs.Count; i++)
            str += consoleLogs[i] + "\n";

        str += "\n";
        foreach (var k in BindDataLogs.Keys)
            str += k + ":" + BindDataLogs[k] + "\n";
        logWindow.WriteLine(str);
    }
    private void ConsoleWrite()
    {
        if (isUseConsoleLog == false)
            return;
        Console.Clear();
        int size = Mathf.Min(10, consoleLogs.Count);
        for (int i = consoleLogs.Count - size; i < consoleLogs.Count; i++)
            Console.WriteLine(consoleLogs[i]);

        Console.WriteLine("\n");
        foreach (var k in BindDataLogs.Keys)
        {
            string s = k + ":" + BindDataLogs[k];
            Console.WriteLine(s);
        }

    }

    public void ConsoleLog(LogLevel level, object message)
    {
        if (isUseConsoleLog == false)
            return;
        if (level == LogLevel.Debug)
        {

        }
        else if (level == LogLevel.Error)
        {
        }
        else if (level == LogLevel.Info)
        {
        }
        else if (level == LogLevel.Warning)
        {
        }

    }

    public void ConsoleWriteLine(string value)
    {
        if (isUseConsoleLog == false)
            return;
        Console.WriteLine(value);
    }
}
