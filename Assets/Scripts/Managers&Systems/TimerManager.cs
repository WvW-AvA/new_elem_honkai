using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 此计时器为倒计时计时器，减为0时为超时。
/// </summary>
public class TimerNode
{
    public TimerManager.ReloadTimerHandler reloadCallback;
    public TimerManager.TimeoutHandle timeoutCallback;
    public TimerManager.TickTimerHandler tickCallBack = null;
    public float reloadValue;
    public float duration;
    public bool isAutoReload;
    public bool isEnable;

    public virtual void Update(float deltaTimer)
    {
        if (isEnable == false)
            return;
        duration -= deltaTimer;
        if (tickCallBack != null)
            tickCallBack.Invoke(duration);
        if (duration < 0)
        {
            if (timeoutCallback != null)
                timeoutCallback.Invoke();
            isEnable = false;
            if (isAutoReload)
                Reload();
        }
    }
    public void Reload()
    {
        if (reloadCallback != null)
            reloadCallback.Invoke();
        duration = reloadValue;
        isEnable = true;
    }
}
public class TimerManager : ManagerBase<TimerManager>
{
    public delegate void ReloadTimerHandler();
    public delegate void TickTimerHandler(float duration);
    public delegate void TimeoutHandle();
    private Dictionary<string, TimerNode> timers = new Dictionary<string, TimerNode>();
    private List<TimerNode> timerList = new List<TimerNode>();
    /// <summary>
    /// 注册定时器
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="reloadValue">定时器自动重置值</param>
    /// <param name="isLoop">是否自动重装</param>
    /// <param name="callback">触发回调委托</param>
    public static TimerNode Schedule(string name, float reloadValue, float beginDuration = 0, bool isLoop = false, TimeoutHandle timeoutCallback = null, TickTimerHandler tickCallback = null, ReloadTimerHandler reloadCallback = null)
    {
        var temp = new TimerNode();
        temp.reloadCallback = reloadCallback;
        temp.timeoutCallback = timeoutCallback;
        temp.tickCallBack = tickCallback;
        temp.reloadValue = reloadValue;
        temp.duration = beginDuration + 0.1f;
        temp.isAutoReload = isLoop;
        temp.isEnable = true;
        Instance.timerList.Add(temp);
        Unschedule(name);
        Instance.timers.Add(name, temp);
        return temp;
    }
    /// <summary>
    /// 注销定时器
    /// </summary>
    /// <param name="name">名称</param>
    public static void Unschedule(string name)
    {
        if (Instance.timers.ContainsKey(name))
        {
            Instance.timerList.Remove(Instance.timers[name]);
            Instance.timers.Remove(name);
        }
    }
    /// <summary>
    /// 获取定时器对象
    /// </summary>
    /// <param name="name">名称</param>
    /// <returns>定时器对象</returns>
    public static TimerNode GetTimer(string name)
    {
        if (Instance.timers.ContainsKey(name))
            return Instance.timers[name];
        else
        {
            Log.Error("You are trying to visit timer {0} but it was not scheduled before. \n or it was unschedule..", name);
            return null;
        }
    }

    private void Update()
    {
        foreach (var t in timerList)
        {
            t.Update(Time.deltaTime);
        }
    }

}
