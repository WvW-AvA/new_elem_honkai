using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class MonoClass : MonoBehaviour
{

}



/// <summary>
/// 协程管理器(该类需要到游戏一开始进行初始化)
/// </summary>
public class QuickCoroutineSystem : ManagerBase<QuickCoroutineSystem>
{

    /// <summary>
    /// 用来跑协程的Mono
    /// </summary>
    private MonoBehaviour m_coroutineMono;

    private static MonoBehaviour coroutineMono
    {
        get
        {
            if (Instance.m_coroutineMono == null)
                Instance.m_coroutineMono = Instance.gameObject.AddComponent<MonoClass>();//有些版本直接添加Monobehavior会出错，可以在创建一个内部类再继承Monobehavior,在挂载上来即可
            return Instance.m_coroutineMono;
        }
    }

    public static new Coroutine StartCoroutine(IEnumerator routine)
    {
        //协程的可迭代函数类型
        return coroutineMono.StartCoroutine(routine);
    }

}

