using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_State : EnemyFSMBaseState
{
    public override bool ExitState(EnemyFSM enemyFSM)
    {
        enemyFSM.curr_x_scale *= -1;
        return base.ExitState(enemyFSM);
    }
}
