using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Animancer;
public class Jump : PlayerFSMBaseState
{
    public override void InitState(PlayerFSM playerFSM)
    {
        base.InitState(playerFSM);
        defaultAnimation.Transition.Events.Clear();
        defaultAnimation.Transition.Events.Add(1f, JumpOnce);
    }
    public float fullJumpPressTime = 0.2f;

    public void JumpOnce()
    {
        float jumpForce = Player.parameter.jumpForce * Mathf.Clamp((InputManager.Keys[EKey.SPACE].pressDuration / fullJumpPressTime), 0.5f, 1f);
        //Log.Info(LogColor.Dye(LogColor.EColor.magenta, "jump force {0}"), jumpForce);
        Player.instance.rigidbody.velocity += new Vector2(0, jumpForce);
    }
    public override void FixAct_State(PlayerFSM playerFSM)
    {
        base.FixAct_State(playerFSM);
        Player.instance.rigidbody.velocity = new Vector2(Player.parameter.moveSpeed * InputManager.Axises[EAxis.Horizontal].value, Player.instance.rigidbody.velocity.y);
    }
}
