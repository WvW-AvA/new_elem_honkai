using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class ElementGetArea : FKeyTriggerBase
{
    public EElement unLockElement;
    public GameObject guide;
    public override void OnFKeyDown()
    {
        base.OnFKeyDown();
        guide.SetActive(true);
        Player.instance.UnlockElement(unLockElement);
    }
}
