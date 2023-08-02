using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : ManagerBase<TimeScaleManager>
{
    public static void changeTimeScaleForFrames(int framesCount, float scale)
    {
        float time = framesCount / 60f;//默认60fps 需要修改
        changeTimeScaleForSeconds(time, scale);
    }

    public static void changeTimeScaleForSeconds(float time, float scale)
    {
        QuickCoroutineSystem.StartCoroutine(Instance.IChangeTimeScaleForSeconds(time, scale));
    }
    private IEnumerator IChangeTimeScaleForSeconds(float time, float scale)
    {
        //print(time);
        Time.timeScale = scale;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }
}
