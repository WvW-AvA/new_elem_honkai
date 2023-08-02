using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class ElementClock : Pawn, Iinjured
{
    public class BuffList {[OdinSerialize] public List<BuffBase> buffs; }
    [OdinSerialize]
    public Dictionary<EElementReaction, BuffList> elementClockReactionBuffs;
    private GameObject cGO;

    private void Awake()
    {
        foreach(var child in elementClockReactionBuffs)
        {
            cGO=transform.Find(StringCheck(child.Key.ToString())).gameObject;
            if(cGO!=null)
            {
                cGO.GetComponent<Damager>().buffList = child.Value.buffs;
            }
        }
    }
    public void GetHeal(int value)
    {

    }

    public void GetHurt(DamageToken damage)
    {
        Log.Info(LogColor.MapObjectFSM + "Get hurt {0}", damage.Calculate());
        AttachElement(damage.hitElement, damage.Giver);
    }
    private string StringCheck(string s)
    {
        s = s.ToLower();
        return s[0].ToString().ToUpper() + s.Substring(1);
    }
}

