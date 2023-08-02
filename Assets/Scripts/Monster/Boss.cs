using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : Monster
{
    public List<EElement> immunityElement;
    public override void GetHurt(DamageToken damage)
    {
        if (immunityElement.Contains(damage.hitElement))
            return;
        base.GetHurt(damage);
    }
}
