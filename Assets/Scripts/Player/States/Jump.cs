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
        defaultAnimation.Transition.Events.Add(0.8f, JumpOnce);
    }
    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        playerFSM.isJumpReleaseKey = false;
    }
    public void JumpOnce()
    {
        float jumpForce = Player.parameter.jumpForce;
        //Log.Info(LogColor.Dye(LogColor.EColor.magenta, "jump force {0}"), jumpForce);
        Player.instance.rigidbody.velocity += new Vector2(0, jumpForce);
    }
    public override void FixAct_State(PlayerFSM playerFSM)
    {
        base.FixAct_State(playerFSM);
        Player.instance.rigidbody.velocity = new Vector2(Player.parameter.moveSpeed * InputManager.Axises[EAxis.Horizontal].value, Player.instance.rigidbody.velocity.y);
    }

    public override void Act_State(PlayerFSM playerFSM)
    {
        base.Act_State(playerFSM);
        if (playerFSM.isJumpReleaseKey == false && InputManager.Keys[EKey.SPACE].isKeyUp)
        {
            playerFSM.isJumpReleaseKey = true;
            float y = Player.instance.rigidbody.velocity.y * Mathf.Clamp(InputManager.Keys[EKey.SPACE].pressDuration / Player.parameter.fullJumpPressTime, 0.5f, 1f);
            Player.instance.rigidbody.velocity = new Vector2(Player.instance.rigidbody.velocity.x, y);
        }
    }
}
