using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
public class CountManager : ManagerBase<CountManager>
{
    public string countManagerConfigPath = "Config/CountManagerConfig";
    private CountConfig cc;
    private List<CountData> m_countDataList;
    public List<CountData> countDataList
    {
        get
        {
            if (m_countDataList is null)
                m_countDataList = new List<CountData>();
            return m_countDataList;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        cc=ResourceManager.GetInstance<CountConfig>(countManagerConfigPath);
        foreach(CountData data in cc.countList)
        {
            countDataList.Add(data);
        }
    }
    //返回对应名称统计数据
    public CountData Find(string name)
    {
        return countDataList.Find(countDate =>
        {
            if (countDate.countName == name) return true;
            return false;
        });
    }
    public void AddCount(string name)
    {
        CountData cd=this.Find(name);
        cd.count++;
    }
    public void AddCount(string name,int num)
    {
        CountData cd = this.Find(name);
        cd.count+=num;
    }
}
