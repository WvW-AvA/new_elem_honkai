using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
/// <summary>
/// Patrol״̬����ˮƽ�ƶ�����û���ڷ�����ƽ̨��Ե��ͷ����
/// ����ײǽ��ͷ��
/// </summary>�����ڵ������ 
public class Patrol_State : EnemyFSMBaseState
{
    public float moveSpeed;
    [Range(0, 1)]
    public float turnPossibility;
    public LinearMixerTransition patrolAnimationMixer;
    public AnimationCurve speedCurve;
    public float turnTime;
    private float timer;
    private int timerDir;

    private Vector2 m_patrolSpeed;
    private float last_velocity;
    protected override bool isHideDefaultAnimation()
    {
        return true;
    }
    public override void InitState(EnemyFSM enemyFSM)
    {
        base.InitState(enemyFSM);
        timerDir = 1;
        timer = turnTime / 2;
    }

    protected Vector2 PatrolSpeed
    {
        get
        {
            if (m_enemyFSM != null)
                return m_patrolSpeed * m_enemyFSM.monster.MoveSpeedRate;
            else
                return m_patrolSpeed;
        }
        set
        {
            if (m_enemyFSM != null)
                m_enemyFSM.rigidbody.velocity = m_enemyFSM.rigidbody.velocity - m_patrolSpeed + value;
            m_patrolSpeed = value;
        }
    }

    public override void FixAct_State(EnemyFSM enemyFSM)
    {
        if (timerDir == 1 && timer < turnTime)
        {
            timer += Time.deltaTime;
        }
        else if (timerDir == -1 && timer > 0)
        {
            timer -= Time.deltaTime;
        }
        PatrolSpeed = moveSpeed * m_enemyFSM.monster.MoveSpeedRate * new Vector2(speedCurve.Evaluate(timer / turnTime), 0);
        float dv = Mathf.Sign(enemyFSM.curr_x_scale) * (enemyFSM.rigidbody.velocity.x - last_velocity);
        last_velocity = enemyFSM.rigidbody.velocity.x;
        Log.ConsoleLog("dv:{0}", dv);
        patrolAnimationMixer.Transition.State.Parameter = Mathf.Clamp(dv, -1, 1);

        if (m_enemyFSM.rigidbody.velocity.y < -0.1) //���������ƽ̨ ��������
            return;
        DetectionPlatformBoundary(enemyFSM);
    }

    public override void EnterState(EnemyFSM enemyFSM)
    {
        base.EnterState(enemyFSM);
        enemyFSM.AnimationPlay(patrolAnimationMixer);
        if (Random.Range(0, 1.0f) < turnPossibility)
            Turn();
    }

    public override bool ExitState(EnemyFSM fSM_Manager)
    {
        PatrolSpeed = Vector2.zero;
        timer = turnTime / 2;
        return true;
    }

    private void Turn()
    {
        Log.Info(LogColor.Monster + "{0} Turn", m_enemyFSM.gameObject.name);
        timerDir *= -1;
    }
    private void DetectionPlatformBoundary(EnemyFSM fSM_Manager)
    {
        if (NearPlatformBorderTrigger.nearPlatformBoundary(fSM_Manager, m_enemyFSM.rigidbody.velocity))
        {
            Turn();

        }
        else if (HitWallTrigger.hitWall(fSM_Manager))
        {
            Turn();
        }
    }
}
