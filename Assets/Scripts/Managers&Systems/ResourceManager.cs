using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



/// <summary>
/// 资源管理器。加载方式和使用逻辑分离
/// 以便于支持：热更新、对象池。
/// </summary>
public class ResourceManager : ManagerBase<ResourceManager>
{
    override protected void Awake()
    {
        base.Awake();
    }

    public static GameObject GetInstance(string resPath, Transform parent)//位置默认值不可以为空,Vector3是个struct类型，不是类，struct是值类型，class是引用类型，也就是struct不能为空，必须要有个值，如果要想为空，给个问号，成为可空类型
    {

        var temp = GetResources<GameObject>(resPath);
        if (temp == null)
        {
            Log.Error("{0}未找到资源:{1}", LogColor.ResourceManager, resPath);
            return null;
        }

        //相当于lua里面的  realPos=pos or temp.transform.position
        return (GameObject)GameObject.Instantiate(temp, parent);//与上面一个重载的区别就在于上面有一个默认位置，就是模板是什么位置就是什么位置
    }
    public static GameObject GetInstance(GameObject prefab, Vector3? pos = null)//位置默认值不可以为空,Vector3是个struct类型，不是类，struct是值类型，class是引用类型，也就是struct不能为空，必须要有个值，如果要想为空，给个问号，成为可空类型
    {
        if (prefab == null)
        {
            Log.Error("{0}引用丢失", LogColor.ResourceManager);
        }
        var temp = GameObject.Instantiate(prefab);//与上面一个重载的区别就在于上面有一个默认位置，就是模板是什么位置就是什么位置
        if (pos != null)
            temp.transform.position = pos.Value;
        return temp;
    }
    public static GameObject GetInstance(string resPath, Vector3? pos = null)//位置默认值不可以为空,Vector3是个struct类型，不是类，struct是值类型，class是引用类型，也就是struct不能为空，必须要有个值，如果要想为空，给个问号，成为可空类型
    {

        var temp = GetResources<GameObject>(resPath);
        if (temp == null)
        {
            Log.Error("{0}未找到资源:{1}", LogColor.ResourceManager, resPath);
            return null;
        }

        //相当于lua里面的  realPos=pos or temp.transform.position
        var realPos = pos != null ? pos.Value : temp.transform.position;//三元运算符，pos不为空，则执行前面

        return (GameObject)GameObject.Instantiate(temp, realPos, temp.transform.rotation);//与上面一个重载的区别就在于上面有一个默认位置，就是模板是什么位置就是什么位置
    }

    public static GameObject GetInstance(string resPath, Quaternion rotation, Vector3? pos = null)//位置默认值不可以为空,Vector3是个struct类型，不是类，struct是值类型，class是引用类型，也就是struct不能为空，必须要有个值，如果要想为空，给个问号，成为可空类型
    {

        var temp = GetResources<GameObject>(resPath);
        if (temp == null)
        {
            Log.Error("{0}未找到资源:{1}", LogColor.ResourceManager, resPath);
            return null;
        }

        //相当于lua里面的  realPos=pos or temp.transform.position
        var realPos = pos != null ? pos.Value : temp.transform.position;//三元运算符，pos不为空，则执行前面

        return (GameObject)GameObject.Instantiate(temp, realPos, rotation);//与上面一个重载的区别就在于上面有一个默认位置，就是模板是什么位置就是什么位置
    }


    public static T GetInstance<T>(string resPath) where T : UnityEngine.Object
    {
        var temp = GetResources<T>(resPath);
        if (temp == null)
        {
            Log.Error("{0}未找到资源:{1}", LogColor.ResourceManager, resPath);
            return null;
        }
        return GameObject.Instantiate<T>(temp);
    }

    public static T GetResources<T>(string resPath) where T : UnityEngine.Object
    {
        T resource = Resources.Load<T>(resPath);
        if (resource is null)
            Log.Error("{0}未找到资源:{1}", LogColor.ResourceManager, resPath);
        return resource;
    }
    public static void GetAllResources<T>(string resPath, ref T[] values) where T : UnityEngine.Object
    {
        T[] resources;
        resources = Resources.LoadAll<T>(resPath);
        if (resources is null)
            Log.Error("{0}未找到资源:{1}", LogColor.ResourceManager, resPath);
        values = new T[resources.Length];
        int i = 0;
        foreach (T item in resources)
        {
            values[i++] = item;
        }
        return;
    }

    public static void Release(GameObject target)
    {
        if (target == null)
            return;
        GameObject.Destroy(target);
    }

    public static void Release(GameObject target, float delayTime)
    {
        if (target == null)
            return;
        GameObject.Destroy(target, delayTime);
    }
}

