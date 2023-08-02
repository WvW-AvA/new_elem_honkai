using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Animancer;
public class Player_Walk : PlayerFSMBaseState
{
    [ReadOnly]
    [OdinSerialize]
    private Vector2 m_currentMoveSpeed;
    public bool isMixer = true;
    [ShowIf("isMixer")]
    public LinearMixerTransition mixTransition;
    private float last_velocity;
    public Vector2 currentMoveSpeed
    {
        set
        {
            m_currentMoveSpeed = value;
            Player.instance.rigidbody.velocity = m_currentMoveSpeed;
        }
        get
        {
            m_currentMoveSpeed = Player.instance.rigidbody.velocity;
            return m_currentMoveSpeed;
        }
    }

    protected override bool isHideDefaultAnimation() { return isMixer; }

    public override void InitState(PlayerFSM playerFSM)
    {
        base.InitState(playerFSM);
    }
    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        if (isMixer)
            playerFSM.AnimationPlay(mixTransition);
    }
    public override void FixAct_State(PlayerFSM playerFSM)
    {
        base.FixAct_State(playerFSM);
        currentMoveSpeed = new Vector2(Player.parameter.moveSpeed * InputManager.Axises[EAxis.Horizontal].value, Player.instance.rigidbody.velocity.y);

        if (isMixer == false)
            return;
        float dv = Mathf.Sign(playerFSM.curr_x_scale) * (currentMoveSpeed.x - last_velocity);
        Log.ConsoleLog("dv:{0}", dv);
        last_velocity = currentMoveSpeed.x;
        mixTransition.Transition.State.Parameter = Mathf.Clamp(dv, -1, 1);
    }
    public override bool ExitState(PlayerFSM playerFSM)
    {
        return true;
    }
    public override void OnCollisionStay2D(PlayerFSM playerFSM, Collision2D collision)
    {
        base.OnCollisionEnter2D(playerFSM, collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            playerFSM.isAllowDash = true;
        }
    }

}
