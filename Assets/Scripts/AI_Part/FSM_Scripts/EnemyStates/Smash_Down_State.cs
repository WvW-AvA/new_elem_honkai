using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
/// <summary>
/// ��������Ϸ������ҵ���Ϊ
/// û�п����˶�ʱ��Ĳ�������Ϊ�Ǹ��ݶ�����ʱ������ģ�Ҫ��С�˶�ʱ�䣬����󶯻������ٶȡ�
/// </summary>
public class Smash_Down_State : EnemyFSMBaseState
{
    public float jumpHight;
    public ClipTransition jumpAnimation;
    public ClipTransition smashDownAnimation;
    public bool isMoveWithCurve;
    public AnimationCurve jumpCurve;

    private Vector3 beginPos, endPos;
    private AnimatorStateInfo stateInfo;
    public override void EnterState(EnemyFSM EnemyFSM)
    {
        base.EnterState(EnemyFSM);
        m_enemyFSM = EnemyFSM;
        beginPos = EnemyFSM.transform.position;
        endPos = new Vector2(Player.instance.transform.position.x, beginPos.y);
        if (jumpAnimation != null)
            EnemyFSM.AnimationPlay(jumpAnimation);
    }

    public override void Act_State(EnemyFSM EnemyFSM)
    {
    }
}

