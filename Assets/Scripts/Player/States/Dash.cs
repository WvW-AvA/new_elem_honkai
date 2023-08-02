using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class Dash : SkillBase
{
    public float dashDistance;
    public bool autoDashTime = true;
    [HideIf("autoDashTime")]
    public float dashTime;

    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        if (autoDashTime)
        {
            dashTime = defaultAnimation.Transition.Clip.length;
        }
        Vector2 target = new Vector2(playerFSM.curr_x_scale, 0).normalized * dashDistance + (Vector2)playerFSM.transform.position;
        playerFSM.rigidbody.DOMove(target, dashTime);
        playerFSM.isAllowDash = false;
    }

    public override void FixAct_State(PlayerFSM playerFSM)
    {
        base.FixAct_State(playerFSM);
        playerFSM.rigidbody.velocity = new Vector2(Player.parameter.moveSpeed * InputManager.Axises[EAxis.Horizontal].value, Player.instance.rigidbody.velocity.y);
    }
    public override bool ExitState(PlayerFSM playerFSM)
    {
        return base.ExitState(playerFSM);
    }
}
