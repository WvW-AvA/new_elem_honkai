using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Sirenix.OdinInspector;
[Serializable]
public class Map_Idle_State : MapObjectFSMBaseState
{
    public bool isflying = false;
    [ShowIf("isflying")]
    public bool isFlyContinueWhenStateExit = false;
    [ShowIf("isflying")]
    public float flyingMinHight;
    private float lastGravity;
    public override void EnterState(MapObjectFSM fSM_Manager)
    {
        base.EnterState(fSM_Manager);
        fSM_Manager.GetPlayerDirection();
        fSM_Manager.rigidbody.velocity = Vector3.zero;
        m_mapObjectFSM = fSM_Manager;
        if (isflying)
        {
            lastGravity = m_mapObjectFSM.rigidbody.gravityScale;
            m_mapObjectFSM.rigidbody.gravityScale = 0;
            RaycastHit2D tem = Physics2D.Raycast(fSM_Manager.transform.position, Vector2.down, 300, LayerMask.GetMask("Ground"));
            //Debug.Log(tem.distance);
            if (tem.distance < flyingMinHight)
            {
                Vector3 t = m_mapObjectFSM.transform.position + new Vector3(0, flyingMinHight - tem.distance, 0);
                DOTweenModulePhysics2D.DOMove(fSM_Manager.rigidbody, t, 1f);
            }
        }
    }

    public override bool ExitState(MapObjectFSM MapObjectFSM)
    {
        if (isflying && !isFlyContinueWhenStateExit)
        {
            m_mapObjectFSM.rigidbody.gravityScale = lastGravity;
        }
        return true;
    }

    public override void InitState(MapObjectFSM fSM_Manager)
    {
        base.InitState(fSM_Manager);
    }
}

public class Map_HideState : Map_Idle_State
{
    public override void EnterState(MapObjectFSM fSM_Manager)
    {
        base.EnterState(fSM_Manager);
        fSM_Manager.collider.enabled = false;
    }

    public override bool ExitState(MapObjectFSM MapObjectFSM)
    {
        MapObjectFSM.collider.enabled = true;
        return base.ExitState(MapObjectFSM);
    }
}
