using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
public class Bump_State : EnemyFSMBaseState//Í»½ø³å×²
{
    public float BumpSpeed;

    public float accumulateTime;
    public float bumpTime;
    public float brakeTime;
    private float timer;
    private Vector3 tem = new Vector3(1, 1, 1);
    private Vector2 dir;
    public ClipTransition accumulateAnimation;
    public ClipTransition bumpAnimation;
    public ClipTransition brakeAnimation;

    protected override bool isHideDefaultAnimation()
    {
        return true;
    }
    public override void InitState(EnemyFSM fSM)
    {
        base.InitState(fSM);
        m_enemyFSM = fSM;
    }
    public override void EnterState(EnemyFSM fSM_Manager)
    {
        //Debug.Log("bump");
        dir = (m_enemyFSM as EnemyFSM).GetPlayerDirection();
    }
    public override void Act_State(EnemyFSM EnemyFSM)
    {
        base.Act_State(EnemyFSM);
        timer += Time.deltaTime;
        if (timer > 0 && timer <= accumulateTime && tem[0] == 1)
        {
            tem[0] = 0;
            EnemyFSM.AnimationPlay(accumulateAnimation);

        }
        else if (timer > accumulateTime && timer <= accumulateTime + bumpTime && tem[1] == 1)
        {
            tem[1] = 0;
            float speed = BumpSpeed;
            if (dir.x < 0)
            {
                speed *= -1;
            }
            EnemyFSM.rigidbody.velocity = new Vector2(speed, 0);
            EnemyFSM.AnimationPlay(bumpAnimation);
        }
        else if (timer > accumulateTime + bumpTime && timer <= accumulateTime + bumpTime + brakeTime && tem[2] == 1)
        {
            tem[2] = 0;
            EnemyFSM.AnimationPlay(brakeAnimation);
        }
    }

    public override bool ExitState(EnemyFSM enemyFSM)
    {
        timer = 0;
        tem = new Vector3(1, 1, 1);
        enemyFSM.rigidbody.velocity = Vector2.zero;
        return true;
    }
}
