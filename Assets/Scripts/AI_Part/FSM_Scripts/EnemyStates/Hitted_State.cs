using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitted_State : EnemyFSMBaseState
{
    //private  Vector2 hittedBack;
    // private AnimatorStateInfo info;
    // private Vector3 currPos;
    public float HittedBackDistance;
    private float hittedVelocity;//被击退时的速度
    public override void InitState(EnemyFSM fSM)
    {
        base.InitState(fSM);
        //  hittedVelocity = HittedBackDistance;
        //EventsManager.Instance.AddListener(fSM.gameObject, EventType.onTakeDamage, UpdateHittedBack);
    }

    public override void EnterState(EnemyFSM fSM_Manager)
    {
        //Vector2 dir = new Vector2(1,0);
        //if (fsmManager.damageable.damageDirection.x>0)
        //{
        //    dir.x=-1;
        //}

        //fsmManager.rigidbody2d.velocity = dir * hittedVelocity ;
        //Debug.Log(fsmManager.rigidbody2d.velocity);

    }
    public override bool ExitState(EnemyFSM fSM_Manager)
    {
        //fsmManager.rigidbody2d.velocity = Vector2.zero;
        return true;
    }

    /* public override void Act_State( EnemyFSM fSM_Manager)
     {

         // base.Act_State(fSM_Manager);  
         // fSM_Manager.transform.position = Vector3.Lerp(currPos, currPos + (Vector3)hittedBack, info.normalizedTime);
         Debug.Log(fsmManager.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
         Debug.Log(Time.frameCount);

     }*/
}
