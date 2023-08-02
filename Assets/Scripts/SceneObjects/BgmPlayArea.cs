using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class BgmPlayArea : Interactible
{
    public AudioClip areaBGM;
    protected override void OnPlayerStay()
    {
        base.OnPlayerStay();
        AudioManager.BgmChange(areaBGM);
    }
}
