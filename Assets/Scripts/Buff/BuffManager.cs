using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BuffManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, BuffBase> m_buffDic;
    public Dictionary<string, BuffBase> BuffDic
    {
        get
        {
            if (m_buffDic == null)
                m_buffDic = new Dictionary<string, BuffBase>();
            return m_buffDic;
        }
        set { m_buffDic = value; }
    }
    [ShowInInspector]
    private List<BuffBase> m_buffs;
    public List<BuffBase> Buffs
    {
        get
        {
            if (m_buffs == null)
                m_buffs = new List<BuffBase>();
            return m_buffs;
        }
    }
    public bool isBuffsAcceptable = true;
    void Start()
    {
    }

    void Update()
    {
        for (int i = 0; i < Buffs.Count; i++)
        {
            Buffs[i].Update();
        }
    }

    public static void AddBuffList(List<BuffBase> buffs, GameObject giver, GameObject bearer)
    {
        if (buffs == null)
            return;
        foreach (var b in buffs)
            AddBuff(b, giver, bearer);
    }

    public static void AddBuff(BuffBase buff, GameObject giver, GameObject bearer)
    {
        if (bearer == null)
            return;
        if (giver == null)
        {
            Log.Error(LogColor.BuffManager + "buff giver is null!");
            return;
        }
        buff.Bearer = bearer;
        buff.Giver = giver;
        if (bearer.GetComponent<BuffManager>() == null)
            bearer.AddComponent<BuffManager>();
        var tem = bearer.GetComponent<BuffManager>();
        if (tem.isBuffsAcceptable == false)
            return;
        if (tem.BuffDic.ContainsKey(buff.buffName))
            RemoveBuff(buff.buffName, tem.gameObject);
        tem.Buffs.Add(buff);
        tem.BuffDic.Add(buff.buffName, buff);
        tem.BuffDic[buff.buffName].Enter();
    }
    public static void RemoveBuff(BuffBase buff, GameObject target)
    {
        if (target == null)
            return;
        if (target.GetComponent<BuffManager>() == null)
            target.AddComponent<BuffManager>();
        var tem = target.GetComponent<BuffManager>();
        if (tem.BuffDic.ContainsKey(buff.buffName))
        {
            tem.BuffDic[buff.buffName].OnBuffExit();
            tem.Buffs.Remove(buff);
            tem.BuffDic.Remove(buff.buffName);
        }
    }
    public static void RemoveBuff(string buff, GameObject target)
    {
        if (target == null)
            return;
        if (target.GetComponent<BuffManager>() == null)
            target.AddComponent<BuffManager>();
        var tem = target.GetComponent<BuffManager>();
        if (tem.BuffDic.ContainsKey(buff))
        {
            tem.BuffDic[buff].OnBuffExit();
            tem.Buffs.Remove(tem.BuffDic[buff]);
            tem.BuffDic.Remove(buff);
        }
    }
    public static void RemoveAllBuff(GameObject target)
    {
        if (target == null)
            return;
        if (target.GetComponent<BuffManager>() == null)
            target.AddComponent<BuffManager>();
        var tem = target.GetComponent<BuffManager>();
        for (int i = 0; i < tem.Buffs.Count; i++)
            tem.Buffs[i].OnBuffExit();
        tem.Buffs.Clear();
        tem.BuffDic.Clear();
    }
    public static void SetBuffAcceptable(bool value, GameObject target)
    {
        if (target == null)
            return;
        if (target.GetComponent<BuffManager>() == null)
            target.AddComponent<BuffManager>();
        var tem = target.GetComponent<BuffManager>();
        tem.isBuffsAcceptable = value;
    }
}
