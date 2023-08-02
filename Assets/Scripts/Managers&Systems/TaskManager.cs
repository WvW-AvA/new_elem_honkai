using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
public class TaskManager : ManagerBase<CountManager>
{
    public string taskConfigPath = "Config/Task";
    private List<BaseTask> m_LockedList;
    public List<BaseTask> LockedList
    {
        get
        {
            if (m_LockedList is null)
                m_LockedList = new List<BaseTask>();
            return m_LockedList;
        }
    }
    private List<BaseTask> m_UnacceptedList;
    public List<BaseTask> UnacceptedList
    {
        get
        {
            if (m_UnacceptedList is null)
                m_UnacceptedList = new List<BaseTask>();
            return m_UnacceptedList;
        }
    }
    private List<BaseTask> m_InprogressList;
    public List<BaseTask> InprogressList
    {
        get
        {
            if (m_InprogressList is null)
                m_InprogressList = new List<BaseTask>();
            return m_InprogressList;
        }
    }
    private List<BaseTask> m_CompletedList;
    public List<BaseTask> CompletedList
    {
        get
        {
            if (m_CompletedList is null)
                m_CompletedList = new List<BaseTask>();
            return m_CompletedList;
        }
    }
    private List<BaseTask> m_OverList;
    public List<BaseTask> OverList
    {
        get
        {
            if (m_OverList is null)
                m_OverList = new List<BaseTask>();
            return m_OverList;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        TaskConfig[] taskConfigs=null;
        ResourceManager.GetAllResources<TaskConfig>(taskConfigPath,ref taskConfigs);
        if (taskConfigs != null)
            foreach (TaskConfig taskConfig in taskConfigs)
            {
                Debug.Log(taskConfig._taskID);
            }
        else Debug.Log("taskConfigs is null");
    }
}
