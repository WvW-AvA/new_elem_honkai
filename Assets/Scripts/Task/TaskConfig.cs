using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
[CreateAssetMenu(fileName = "TaskConfig", menuName = "ScriptableObjects/TaskConfig")]

public class TaskConfig : ScriptableObject
{
    public int _taskID;
    public string _taskName;
    public string _taskDescription;
    public taskType _taskType;
    public taskState _taskState;
    public TaskDetail _taskDetail;
    [SerializeField]
    public List<Goods> taskRequestGoodsList = new List<Goods>();
    [SerializeField]
    public List<Goods> taskRewardGoodsList = new List<Goods>();
    [SerializeField]
    public List<Goods> taskAcceptConsumeGoodsList = new List<Goods>();
    [SerializeField]
    public List<Goods> taskSubmitConsumeGoodsList = new List<Goods>();
}
#if UNITY_EDITOR
[CustomEditor(typeof(TaskConfig))]
public class TaskConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
#endif
