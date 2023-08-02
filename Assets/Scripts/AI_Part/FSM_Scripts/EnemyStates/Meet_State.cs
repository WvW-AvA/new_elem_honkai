using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meet_State : EnemyFSMBaseState
{
    public override void Act_State(EnemyFSM fSM_Manager)
    {

    }

    public override void EnterState(EnemyFSM fSM_Manager)
    {
        base.EnterState(fSM_Manager);
    }
    public override void InitState(EnemyFSM fSM_Manager)
    {
        base.InitState(fSM_Manager);
        m_enemyFSM = fSM_Manager;
    }


}
