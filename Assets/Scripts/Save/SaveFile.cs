using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 存档文件类，这个是一个存档中所包含的所有信息，
/// 我们序列化就是序列化这个类
/// </summary>
public class SaveFile : ISerializationCallbackReceiver
{
    /// <summary>
    /// 存档名
    /// </summary>
    public string fileName = "Default.json";
    /// <summary>
    /// 玩家数据
    /// </summary>
    public SaveData_Player playerData;
    /// <summary>
    /// 场景数据
    /// </summary>
    public Dictionary<string, SaveData_Scene> sceneData;
    /// <summary>
    /// 设置数据
    /// </summary>
    public SaveData_Setting settingData;


    [HideInInspector]
    public List<string> m_sceneDataKeys;
    [HideInInspector]
    public List<SaveData_Scene> m_sceneDataValues;
    public SaveFile()
    {
        playerData = new SaveData_Player();
        settingData = new SaveData_Setting();
        sceneData = new Dictionary<string, SaveData_Scene>();
        m_sceneDataValues = new List<SaveData_Scene>();
        m_sceneDataKeys = new List<string>();
    }

    public void OnAfterDeserialize()
    {
        int count = Mathf.Min(m_sceneDataValues.Count, m_sceneDataKeys.Count);
        sceneData.Clear();
        for (int i = 0; i < count; i++)
            sceneData.Add(m_sceneDataKeys[i], m_sceneDataValues[i]);
    }

    public void OnBeforeSerialize()
    {
        m_sceneDataKeys = new List<string>(sceneData.Keys);
        m_sceneDataValues = new List<SaveData_Scene>(sceneData.Values);
    }

    public void BeforeSave()
    {
        playerData.BeforeSave();
        settingData.BeforeSave();
    }

    public void AfterLoad()
    {
        playerData.AfterLoad();
        settingData.AfterLoad();
    }
}
