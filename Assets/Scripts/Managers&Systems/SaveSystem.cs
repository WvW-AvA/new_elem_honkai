using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;


public class SaveSystem : ManagerBase<SaveSystem>
{
    [Serializable]
    public class SaveFileInfo
    {
        public string name;
        public string path;
        public string scene;
        public float HP;
        public SaveFileInfo(string name) { this.name = name; }
    }

    [Serializable]
    private class SaveFilesInfoList_Serial
    {
        public SaveFileInfo ContinueFileInfo;
        public List<SaveFileInfo> saveFilesInfo;
    }

    [HideInInspector]
    public List<SaveFileInfo> saveFilesInfo;
    [HideInInspector]
    public SaveFileInfo currentFileInfo;

    private SaveFilesInfoList_Serial saveFilesInfoList;

    private string saveFilesInfoPath;
    private SaveFile m_currentFile;
    public static SaveFile CurrentFile
    {
        get
        {
            if (Instance.m_currentFile == null)
                Instance.m_currentFile = new SaveFile();
            return Instance.m_currentFile;
        }
        set
        {
            Instance.m_currentFile = value;
        }
    }

    override protected void Awake()
    {
        base.Awake();
        saveFilesInfoList = new SaveFilesInfoList_Serial();
        saveFilesInfoPath = Application.persistentDataPath + "/SaveFilesInfo.json";
        LoadSaveFilesInfo();
    }

    private void LoadSaveFilesInfo()
    {
        if (File.Exists(saveFilesInfoPath) == false)
        {
            saveFilesInfo = new List<SaveFileInfo>(8);
            Save_SaveFilesInfo();
        }
        StreamReader sr = new StreamReader(saveFilesInfoPath);
        saveFilesInfoList = JsonUtility.FromJson<SaveFilesInfoList_Serial>(sr.ReadToEnd());
        saveFilesInfo = saveFilesInfoList.saveFilesInfo;
        sr.Close();
        Log.Info("{0}Load FilesInfo from {1}", LogColor.SaveSystem, saveFilesInfoPath);
    }

    private void Save_SaveFilesInfo()
    {
        saveFilesInfoList.saveFilesInfo = saveFilesInfo;
        string js = JsonUtility.ToJson(saveFilesInfoList);
        StreamWriter sw = new StreamWriter(saveFilesInfoPath);
        sw.Write(js);
        sw.Close();
        Log.Info("{0}Save FilesInfo in {1}", LogColor.SaveSystem, saveFilesInfoPath);
    }

    private void UpdateSaveFliesInfo()
    {
        SaveFileInfo sf = null;
        foreach (var info in saveFilesInfo)
        {
            if (info.name == CurrentFile.fileName)
            {
                sf = info;
                continue;
            }
        }
        if (sf == null)
        {
            sf = new SaveFileInfo(CurrentFile.fileName);
            saveFilesInfo.Add(sf);
        }
        sf.path = Application.persistentDataPath + "/" + sf.name + ".json";
        sf.scene = CurrentFile.playerData.scene;
        sf.HP = CurrentFile.playerData.curr_HP;
        saveFilesInfoList.ContinueFileInfo = sf;
        Save_SaveFilesInfo();
    }
    /// <summary>
    /// 新建一个新的存档
    /// </summary>
    /// <param name="fileName">存档名</param>
    public static void CreateNewSaveFile(string fileName)
    {
        CurrentFile = new SaveFile();
        CurrentFile.fileName = fileName;
        string js = JsonUtility.ToJson(CurrentFile);
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + CurrentFile.fileName + ".json");
        sw.Write(js);
        sw.Close();
        Instance.UpdateSaveFliesInfo();
        Log.Info("{0}Save SaveFile {1}", LogColor.SaveSystem, CurrentFile.fileName);

    }
    /// <summary>
    /// 新建一个存档，自动命名,并返回名称。
    /// </summary>
    public static string CreateNewSaveFile()
    {
        string fileName = (Instance.saveFilesInfo.Count() + 1).ToString();
        CurrentFile = new SaveFile();
        CurrentFile.fileName = fileName;
        string js = JsonUtility.ToJson(CurrentFile);
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + CurrentFile.fileName + ".json");
        sw.Write(js);
        sw.Close();
        Instance.UpdateSaveFliesInfo();
        Log.Info("{0}Save SaveFile {1}", LogColor.SaveSystem, CurrentFile.fileName);
        return fileName;
    }

    /// <summary>
    /// 将currentFile 序列化并保存。
    /// </summary>
    public static void Save()
    {
        CurrentFile.BeforeSave();
        string js = JsonUtility.ToJson(CurrentFile);
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + CurrentFile.fileName + ".json");
        sw.Write(js);
        sw.Close();
        Instance.UpdateSaveFliesInfo();
        Log.Info("{0}Save SaveFile {1}", LogColor.SaveSystem, CurrentFile.fileName);
    }


    /// <summary>
    /// 从实际文件中载入存档，并反序列化为SaveFile
    /// </summary>
    /// <param name="fileName">不用加.json后缀。</param>
    /// <returns></returns>
    public static bool Load(string fileName)
    {
        if (CurrentFile != null && CurrentFile.fileName == fileName)
        {
            CurrentFile.AfterLoad();
            return true;
        }
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        if (File.Exists(path))
        {
            Log.Info("{0}Load SaveFile {1}", LogColor.SaveSystem, path);
            StreamReader sr = new StreamReader(path);
            CurrentFile = JsonUtility.FromJson<SaveFile>(sr.ReadToEnd());
            sr.Close();
            CurrentFile.AfterLoad();
            return true;
        }
        Log.Error("{0}Load SaveFile Error, path {1} is not exist", LogColor.SaveSystem, path);
        return false;
    }

    public static bool CheckContinue()
    {
        if (Instance.saveFilesInfoList.ContinueFileInfo == null || string.IsNullOrEmpty(Instance.saveFilesInfoList.ContinueFileInfo.name))
            return false;
        return true;
    }
    public static bool ContinueGame()
    {
        if (string.IsNullOrEmpty(Instance.saveFilesInfoList.ContinueFileInfo.name))
            return false;
        return Load(Instance.saveFilesInfoList.ContinueFileInfo.name);
    }
    public static bool Load(int index)
    {
        if (index > Instance.saveFilesInfo.Count)
        {
            Log.Error("{0}Load SaveFile Error, index {1} is not exist", LogColor.SaveSystem, index);
            return false;
        }
        return Load(Instance.saveFilesInfo[index].name);
    }
    /// <summary>
    /// 从 savefile 里面读取场景中的保存数据，并同步到场景,建议每次加载场景的时候调用
    /// </summary>
    public static void SyncCurrentSceneData()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        var sceneData = CurrentFile.sceneData;
        if (sceneData.ContainsKey(sceneName) == false)
        {
            sceneData.Add(sceneName, new SaveData_Scene(sceneName));
        }
        sceneData[sceneName].AfterLoad();
        Log.Info("{0}Load scene data {1}", LogColor.SaveSystem, sceneName);
    }
    /// <summary>
    /// 将当前场景中的某物品的存档数据添加到存档中
    /// </summary>
    /// <param name="data"></param>
    public static void AddSceneObjectSaveData(SceneObjectSaveData.Base data)
    {
        var sceneData = CurrentFile.sceneData;
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneData.ContainsKey(sceneName) == false)
            sceneData.Add(sceneName, new SaveData_Scene(sceneName));

        if (sceneData[sceneName].sceneObjects.ContainsKey(data.go) == false)
            sceneData[sceneName].sceneObjects.Add(data.go, data);
        else
            sceneData[sceneName].sceneObjects[data.go] = data;
    }
}
