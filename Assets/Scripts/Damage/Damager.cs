using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine.Serialization;
using System;
/// <summary>
/// 具有造成伤害的功能,传递buff。
/// </summary>
/// 
public class Damager : SerializedMonoBehaviour
{
    public LayerMask triggerLayer;
    public GameObject owner;
    [NonSerialized, OdinSerialize]
    public List<BuffBase> buffList;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((triggerLayer & (0x01 << collision.gameObject.layer)) == 0 || buffList == null)
            return;
        foreach (var b in buffList)
        {
            BuffManager.AddBuff(b, owner, collision.gameObject);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if ((triggerLayer & (0x01 << collision.gameObject.layer)) == 0 || buffList == null)
            return;
        foreach (var b in buffList)
        {
            BuffManager.AddBuff(b, owner, collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((triggerLayer & (0x01 << collision.gameObject.layer)) == 0 || buffList == null)
            return;
        foreach (var b in buffList)
        {
            if (b is ElementDamageBuff)
            {
                if ((b as ElementDamageBuff).activeOnlyOnArea)
                    b.CurrTime = 0f;
            }
            if (b is HealBuff)
            {
                if ((b as HealBuff).activeOnlyOnArea)
                    b.CurrTime = 0f;
            }
            if (b is RestrainBuff)
            {
                if ((b as RestrainBuff).activeOnlyOnArea)
                    b.CurrTime = 0f;
            }
        }
    }
}
