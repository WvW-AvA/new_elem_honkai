using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 存档数据基类
/// </summary>
[Serializable]
public class SaveData
{
    /// <summary>
    /// 保存之前调用
    /// </summary>
    public virtual void BeforeSave() { }
    /// <summary>
    /// 载入之后调用
    /// </summary>
    public virtual void AfterLoad() { }
}

[Serializable]
public class SaveData_Player : SaveData
{
    public int curr_HP = 100;
    public EElement element;
    public string scene = "Scenes/Real Use Scene/Wei.test/level1";
    public Vector3 pos = new Vector3(-33f, -1.5f, 0);

    public override void BeforeSave()
    {
        base.BeforeSave();
        element = Player.instance.curr.element;
        scene = SceneManager.GetActiveScene().name;
        curr_HP = Mathf.Max(1, Player.instance.curr.hp);
        pos = Player.instance.transform.position;
    }

    public override void AfterLoad()
    {
        base.AfterLoad();
        GameManager.LoadSceneAsync(scene, () =>
    {
        Player.instance.transform.position = pos;
        Player.instance.curr.hp = curr_HP;
        Player.instance.ChangeElement(element);
        Player.instance.playerFSM.Start();
        Player.instance.gameObject.SetActive(true);
        CameraManager.AutoSetCameraBoundary();
        GameManager.ChangeGameMode(GameMode.EGameMode.NormalMode);
        SaveSystem.SyncCurrentSceneData();
    });
    }
}
/// <summary>
/// 场景数据类，里面存储的是某个场景中的数据
/// </summary>
[Serializable]
public class SaveData_Scene : SaveData, ISerializationCallbackReceiver
{

    public string sceneName;
    /// <summary>
    /// 使用字典保存每个不同gameObject的存档内容
    /// </summary>
    public Dictionary<GameObject, SceneObjectSaveData.Base> sceneObjects = new Dictionary<GameObject, SceneObjectSaveData.Base>();
    [HideInInspector]
    public List<GameObject> m_sceneObjectsKeys = new List<GameObject>();
    [HideInInspector]
    public List<SceneObjectSaveData.Base> m_sceneObjectValues = new List<SceneObjectSaveData.Base>();
    private SaveData_Scene() { }
    public SaveData_Scene(string sceneName)
    {
        this.sceneName = sceneName;
    }
    public override void AfterLoad()
    {
        base.AfterLoad();
        foreach (var s in sceneObjects.Values)
            s.AfterLoad();
    }
    public override void BeforeSave()
    {
        base.BeforeSave();
        foreach (var s in sceneObjects.Values)
            s.BeforeSave();
    }

    public void OnBeforeSerialize()
    {
        m_sceneObjectsKeys = new List<GameObject>(sceneObjects.Keys);
        m_sceneObjectValues = new List<SceneObjectSaveData.Base>(sceneObjects.Values);

    }

    public void OnAfterDeserialize()
    {
        sceneObjects.Clear();
        int size = Mathf.Min(m_sceneObjectsKeys.Count, m_sceneObjectValues.Count);
        for (int i = 0; i < size; i++)
            sceneObjects.Add(m_sceneObjectsKeys[i], m_sceneObjectValues[i]);
    }
}


[Serializable]
public class SaveData_Setting : SaveData
{
    public int BGM_Volume;
    public int AudioEffectVolume;
    public int TotalVolume;
}
