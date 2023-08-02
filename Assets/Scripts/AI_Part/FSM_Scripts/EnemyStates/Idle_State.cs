using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Sirenix.OdinInspector;
[Serializable]
public class Idle_State : EnemyFSMBaseState
{
    public bool isflying = false;
    [ShowIf("isflying")]
    public bool isFlyContinueWhenStateExit = false;
    [ShowIf("isflying")]
    public float flyingMinHight;
    private float lastGravity;
    public override void EnterState(EnemyFSM fSM_Manager)
    {
        base.EnterState(fSM_Manager);
        fSM_Manager.GetPlayerDirection();
        fSM_Manager.rigidbody.velocity = Vector3.zero;
        m_enemyFSM = fSM_Manager;
        if (isflying)
        {
            lastGravity = m_enemyFSM.rigidbody.gravityScale;
            m_enemyFSM.rigidbody.gravityScale = 0;
            RaycastHit2D tem = Physics2D.Raycast(fSM_Manager.transform.position, Vector2.down, 300, LayerMask.GetMask("Ground"));
            //Debug.Log(tem.distance);
            if (tem.distance < flyingMinHight)
            {
                Vector3 t = m_enemyFSM.transform.position + new Vector3(0, flyingMinHight - tem.distance, 0);
                DOTweenModulePhysics2D.DOMove(fSM_Manager.rigidbody, t, 1f);
            }
        }
    }

    public override bool ExitState(EnemyFSM EnemyFSM)
    {
        if (isflying && !isFlyContinueWhenStateExit)
        {
            m_enemyFSM.rigidbody.gravityScale = lastGravity;
        }
        return true;
    }

    public override void InitState(EnemyFSM fSM_Manager)
    {
        base.InitState(fSM_Manager);
    }
}

public class HideState : Idle_State
{
    public override void EnterState(EnemyFSM fSM_Manager)
    {
        base.EnterState(fSM_Manager);
        fSM_Manager.collider.enabled = false;
    }

    public override bool ExitState(EnemyFSM EnemyFSM)
    {
        EnemyFSM.collider.enabled = true;
        return base.ExitState(EnemyFSM);
    }
}

public class WakeUpState : EnemyFSMBaseState
{
    public override void EnterState(EnemyFSM enemyFSM)
    {
        base.EnterState(enemyFSM);
        GameManager.ChangeGameMode(GameMode.EGameMode.BattleMode);
        ((GameMode.BattleMode)(GameManager.Instance.currentMode)).targetBoss = enemyFSM.gameObject.GetComponent<Boss>();
    }
}
