using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Shoot_State : EnemyFSMBaseState
{
    public string shootType;
    private ShootSystem shootSystem;
    private Vector2 dir;
    public override void InitState(EnemyFSM fSM_Manager)
    {
        base.InitState(fSM_Manager);
        m_enemyFSM = fSM_Manager;
        defaultAnimation.Transition.Events.Add(0f, () =>
         {
             if (fSM_Manager == null)
                 return;
             if (shootSystem == null)
                 shootSystem = fSM_Manager.gameObject.GetComponent<ShootSystem>();
             shootSystem.Shoot(shootType);
             for (int i = 0; i < shootSystem.shootModes.Count; i++)
             {
                 ShootSystem.shootParam p = shootSystem.shootModes[i];
                 p.target = Player.instance.gameObject;
                 shootSystem.shootModes[i] = p;
             }
         });
    }

    public override void EnterState(EnemyFSM EnemyFSM)
    {
        base.EnterState(EnemyFSM);
        EnemyFSM.rigidbody.velocity = Vector2.zero;
        dir = EnemyFSM.GetPlayerDirection().normalized;
        EnemyFSM.rigidbody.DORotate(Mathf.Asin(dir.y), 0.5f);
    }
    public override bool ExitState(EnemyFSM EnemyFSM)
    {
        EnemyFSM.rigidbody.DORotate(-Mathf.Asin(dir.y), 1f);
        return true;
    }

}
