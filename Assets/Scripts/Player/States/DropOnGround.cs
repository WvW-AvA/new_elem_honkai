using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DropOnGround : PlayerFSMBaseState
{
    public string passByStateName;
    public float speedThresould;

    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        playerFSM.isAllowDash = true;
        if (Mathf.Abs(playerFSM.verticalMaxNegativeSpeed) < speedThresould)
        {
            playerFSM.ChangeState(passByStateName);
        }
    }
    public override void FixAct_State(PlayerFSM playerFSM)
    {
        base.FixAct_State(playerFSM);
        Player.instance.rigidbody.velocity = new Vector2(Player.parameter.moveSpeed * InputManager.Axises[EAxis.Horizontal].value, Player.instance.rigidbody.velocity.y);
    }
}
