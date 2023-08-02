using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit_State : EnemyFSMBaseState
{
    public float avoidForce = 2;//�����Ĵ�С
    public float MAX_SEE_AHEAD = 2.0f;//��Ұ��Χ
    public float maxSpeed = 2;
    public float maxForce = 2;
    public bool noTurnState;//���У���turn state��ת��
    //private GameObject target;
    public override void InitState(EnemyFSM fSM)
    {
        base.InitState(fSM);
        m_enemyFSM = fSM;
    }
    public override void Act_State(EnemyFSM fSM_Manager)
    {
        base.Act_State(fSM_Manager);
        m_enemyFSM.rigidbody.AddForce(Project4(m_enemyFSM));
        Force(m_enemyFSM);
    }
    public void Force(EnemyFSM fsmManager)
    {

        Vector2 desiredVelocity = fsmManager.GetPlayerDirection().normalized * maxSpeed;
        desiredVelocity.y += 0.5f;//���������̫������һЩ�˶����⣬��ʱ���룬��ɾȥ
        Vector2 steeringForce = (desiredVelocity - fsmManager.rigidbody.velocity);
        if (steeringForce.magnitude > maxForce) steeringForce = steeringForce.normalized * maxForce;
        // Debug.DrawLine(fsmManager.transform.position, (Vector2)fsmManager.transform.position + steeringForce, Color.green);
        fsmManager.rigidbody.AddForce(steeringForce);
    }
    public Vector2 Project4(EnemyFSM fsmManager)
    {
        Vector2 steeringForce = Vector2.zero;
        Vector2 toward = fsmManager.rigidbody.velocity.normalized;
        Vector2 vetical = new Vector2(-toward.y, toward.x);
        Vector2 pos = fsmManager.transform.position;
        Vector2 pointA = pos - (toward + vetical) * fsmManager.GetComponent<Collider2D>().bounds.extents.magnitude;
        Vector2 pointB = pos + toward * MAX_SEE_AHEAD + vetical * fsmManager.GetComponent<Collider2D>().bounds.extents.magnitude;
        Collider2D wall = Physics2D.OverlapArea(pointA, pointB, 1 << LayerMask.NameToLayer("Ground"));
        // Debug.DrawLine(pointA, pointB, Color.red);
        if (wall)
        {
            Vector2 ahead = pos + fsmManager.rigidbody.velocity.normalized * MAX_SEE_AHEAD;
            steeringForce = (ahead - (Vector2)wall.transform.position).normalized;
            steeringForce *= avoidForce;
        }
        //   Debug.DrawLine(pos, pos + steeringForce);
        return steeringForce;
    }
}
