using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander_State : EnemyFSMBaseState
{
    public float wanderRange = 3;//随机移动范围
    private Vector2 wanderCenter;
    public float forceToCenter = 2;
    public float wanderRadiu = 1;//每次随机移动的距离
    public float wanderJitter = 1;//越大就越不随机移动
    public float maxSpeed;
    public bool isLockX = false;
    public bool isLockY = false;
    private Vector2 targetPos = Vector2.right;
    public override void InitState(EnemyFSM fSM)
    {
        base.InitState(fSM);
        m_enemyFSM = fSM;
        //wanderCenter = fsmManager.transform.position;
    }
    public override void EnterState(EnemyFSM fSM_Manager)
    {
        base.EnterState(fSM_Manager);
        wanderCenter = m_enemyFSM.transform.position;
    }
    public override void Act_State(EnemyFSM fSM_Manager)
    {
        base.Act_State(fSM_Manager);
        Force(m_enemyFSM);
        if (Vector2.Distance(wanderCenter, m_enemyFSM.transform.position) > wanderRange)
        {
            m_enemyFSM.rigidbody.AddForce((wanderCenter - (Vector2)m_enemyFSM.transform.position).normalized * forceToCenter);
        }
    }
    public void Force(EnemyFSM fsmManager)
    {
        if (!isLockX)
            targetPos.x += Random.Range(-1, 2);
        if (!isLockY)
            targetPos.y += Random.Range(-1, 2);
        targetPos.Normalize();
        targetPos *= wanderRadiu;

        Vector2 target = fsmManager.rigidbody.velocity.normalized * wanderJitter + targetPos;
        Vector2 desiredVelocity = target.normalized * maxSpeed;
        fsmManager.rigidbody.AddForce(desiredVelocity - fsmManager.rigidbody.velocity);
    }
}
