using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss_Shoot_State : EnemyFSMBaseState
{
    public List<string> shootSequence;
    private int index = 0;
    public string ShootGameObjName;
    private ShootSystem shootPoint;
    public override void InitState(EnemyFSM fSM_Manager)
    {
        base.InitState(fSM_Manager);
        fSM_Manager.GetPlayerDirection();
        shootPoint = GameObject.Find(ShootGameObjName).GetComponent<ShootSystem>();
        if (shootPoint == null)
            Debug.LogError("GameObject Needs Compoment ShootSystem!");
        else
            defaultAnimation.Transition.Events.Add(0f, () =>
             {
                 shootPoint.Shoot(shootSequence[index]);
                 index++;
                 if (index >= shootSequence.Count)
                     index = 0;
             });
    }

}
