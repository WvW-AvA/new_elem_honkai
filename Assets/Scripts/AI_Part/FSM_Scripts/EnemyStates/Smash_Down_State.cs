using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
/// <summary>
/// 这个是先上飞再下砸的行为
/// 没有控制运动时间的参数，因为是根据动画的时间来算的，要减小运动时间，请调大动画播放速度。
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

