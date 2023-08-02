using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
public class Attack_State : EnemyFSMBaseState
{
    public SfxSO attackSfx;
    public Vector2 positionOffset;

    public float SfxDelay = 0;
    public bool isDoMove = false;
    [ShowIf("isDoMove"), FoldoutGroup("DoMoveParam")]
    public Vector2 moveDir;
    [ShowIf("isDoMove"), FoldoutGroup("DoMoveParam")]
    public float moveDistance;
    [ShowIf("isDoMove"), FoldoutGroup("DoMoveParam")]
    public float moveTime;
    [ShowIf("isDoMove"), FoldoutGroup("DoMoveParam")]
    public float delayTime;

    public bool isEnableBodyAttack = false;
    [ShowIf("isEnableBodyAttack"), FoldoutGroup("BodyAttackParam")]
    public float bodyAttackDelayTime;
    [ShowIf("isEnableBodyAttack"), FoldoutGroup("BodyAttackParam")]
    public float bodyAttackKeepTime;

    private GameObject sfx;
    public override void EnterState(EnemyFSM enemyFSM)
    {
        enemyFSM.FaceMode = faceMode;
        if (defaultAnimation != null)
        {
            enemyFSM.AnimationPlay(defaultAnimation);
        }
        if (isEnableBodyAttack)
        {
            enemyFSM.monster.BodyAttackEnable(bodyAttackDelayTime, bodyAttackKeepTime);
        }
        if (SfxDelay != 0)
            QuickCoroutineSystem.StartCoroutine(sfx_co(enemyFSM));
        else
        {
            sfx = SfxManager.CreateSfx(attackSfx, (Vector3)positionOffset + enemyFSM.transform.position, enemyFSM.gameObject, enemyFSM.curr_x_scale, false);
            AudioManager.PlayOneShot(defaultAnimationSoundEffect, enemyFSM.transform.position);
        }
        if (isDoMove)
        {
            if (delayTime == 0)
                doMove();
            else
                QuickCoroutineSystem.StartCoroutine(doMove_co());
        }
    }
    private IEnumerator sfx_co(EnemyFSM enemyFSM)
    {
        yield return new WaitForSeconds(SfxDelay);
        if (enemyFSM != null)
        {
            sfx = SfxManager.CreateSfx(attackSfx, (Vector3)positionOffset + enemyFSM.transform.position, enemyFSM.gameObject, enemyFSM.curr_x_scale, false);
            AudioManager.PlayOneShot(defaultAnimationSoundEffect, enemyFSM.transform.position);
        }
    }

    private IEnumerator doMove_co()
    {
        yield return new WaitForSeconds(delayTime);
        if (m_enemyFSM != null)
        {
            doMove();
        }

    }
    private void doMove()
    {
        var real_dir = moveDir;
        real_dir.x *= Mathf.Sign(m_enemyFSM.curr_x_scale);
        m_enemyFSM.transform.DOMove(m_enemyFSM.transform.position + (Vector3)real_dir.normalized * moveDistance, moveTime);
        if (sfx)
            sfx.transform.DOMove((Vector3)positionOffset + m_enemyFSM.transform.position + (Vector3)real_dir.normalized * moveDistance, moveTime);
    }
}

