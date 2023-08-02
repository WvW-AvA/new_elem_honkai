using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JumpHighterArea : Interactible
{
    public float addForce = 20;

    protected override void OnPlayerTouch()
    {
        base.OnPlayerTouch();
        Player.instance.parameterDict[EElement.NONE].jumpForce += addForce;
        Player.instance.parameterDict[EElement.FIRE].jumpForce += addForce;
    }
    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();
        Player.instance.parameterDict[EElement.NONE].jumpForce -= addForce;
        Player.instance.parameterDict[EElement.FIRE].jumpForce -= addForce;
    }
}
