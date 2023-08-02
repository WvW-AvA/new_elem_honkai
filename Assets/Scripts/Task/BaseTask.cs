using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum taskType
{
    mainline,
    sideline
}
public enum taskState
{
    locked,
    unaccepted,
    inprogress,
    completed,
    over
}
[Serializable]
public class Goods
{
    public string name;
    public int num;
}

public class BaseTask : MonoBehaviour
{
    public int _taskID;
    public string _taskName;
    public string _taskDescription;
    public taskType _taskType;
    public taskState _taskState;
    public TaskDetail _taskDetail;
    public List<Goods> taskRequestGoodsList = new List<Goods>();
    public List<Goods> taskRewardGoodsList = new List<Goods>();
    public List<Goods> taskAcceptConsumeGoodsList = new List<Goods>();
    public List<Goods> taskSubmitConsumeGoodsList = new List<Goods>();
}
