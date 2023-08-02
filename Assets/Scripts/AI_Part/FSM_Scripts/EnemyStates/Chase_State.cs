using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Chase_State : EnemyFSMBaseState
{
    public float chaseSpeed;
    public bool isFaceWithSpeed;
    public bool isFlying = false;
    private Vector2 m_chaseVelocity;
    protected Vector2 ChaseVelovity
    {
        get
        {
            if (m_enemyFSM != null)
                return m_chaseVelocity * m_enemyFSM.monster.MoveSpeedRate;

            return m_chaseVelocity;
        }
        set
        {
            if (m_enemyFSM != null)
                m_enemyFSM.rigidbody.velocity += value - m_chaseVelocity;
            m_chaseVelocity = value;
        }
    }
    public override void InitState(EnemyFSM EnemyFSM)
    {
        base.InitState(EnemyFSM);

    }
    public override void EnterState(EnemyFSM EnemyFSM)
    {
        base.EnterState(EnemyFSM);
        if (isFlying)
        {
            EnemyFSM.rigidbody.gravityScale = 0;
        }
    }
    public override void FixAct_State(EnemyFSM fSM_Manager)
    {
        Vector2 v;
        v = fSM_Manager.GetPlayerDirection();
        v = v.normalized;
        VelocityChange(fSM_Manager, v);
    }
    protected virtual void VelocityChange(EnemyFSM fSM, Vector2 v)
    {
        if (!isFlying)
        {
            if (v.x > 0)
                v = new Vector2(1, 0);
            else
                v = new Vector2(-1, 0);
        }
        ChaseVelovity = chaseSpeed * v;
    }
    public override bool ExitState(EnemyFSM fSM)
    {
        ChaseVelovity = Vector2.zero;
        return true;
    }
}

public class Complex_Chase_State : Chase_State
{
    public bool lock_x_move = false;
    public bool lock_y_move = false;
    public bool isMoveWithCurve = false;
    public float curveCycle;
    public AnimationCurve curve;

    protected override void VelocityChange(EnemyFSM fSM, Vector2 v)
    {
        if (lock_x_move)
            v.x = 0;
        if (lock_y_move)
            v.y = 0;
        if (isMoveWithCurve)
        {
            var vv = Vector2.Lerp(Vector3.zero, v, curve.Evaluate((Time.time / (curveCycle + 0.000001f)) % 1.0f));
            ChaseVelovity = chaseSpeed * vv;
        }
        else
            ChaseVelovity = chaseSpeed * v;
    }
}
public class Jump_Chase_State : Chase_State
{
    public float jumpForce = 0;

    public override void InitState(EnemyFSM EnemyFSM)
    {
        base.InitState(EnemyFSM);
        defaultAnimation.Transition.Events.Add(0f, () =>
         {
             EnemyFSM.rigidbody.AddForce(Vector2.up * jumpForce);
         });
    }
    protected override void VelocityChange(EnemyFSM fSM, Vector2 v)
    {
        base.VelocityChange(fSM, v);
    }

}