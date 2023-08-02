using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Animancer;
public class DoTween_State : EnemyFSMBaseState
{
    public float duringTime;


    public bool isDoMove;
    [ShowIfGroup("DoTween Move Config", Condition = "isDoMove")]
    public bool isSelfMove;
    [ShowIfGroup("DoTween Move Config", Condition = "isDoMove")]
    public bool isRandomMoves;
    [ShowIfGroup("DoTween Move Config", Condition = "isDoMove")]
    public float speed;
    [ShowIfGroup("DoTween Move Config", Condition = "isDoMove")]
    public Vector2 dir;
    [ShowIfGroup("DoTween Move Config", Condition = "isDoMove")]
    public bool isTurnWhenHitWall;
    [ShowIfGroup("DoTween Move Config", Condition = "isDoMove")]
    public UnityEvent HitWallEvent;
    [ShowIfGroup("DoTween Move Config", Condition = "isDoMove")]
    private float distance;

    public bool isDoScale;
    [ShowIfGroup("DoTween Scale Config", Condition = "isDoScale")]
    public Vector2 begScaleValue;
    [ShowIfGroup("DoTween Scale Config", Condition = "isDoScale")]
    public Vector2 endScaleValue;

    public bool isDoRotation;
    [ShowIf("DoTween Rotataion Config", Condition = "isDoRotation")]
    public float angle;

    public override void InitState(EnemyFSM EnemyFSM)
    {
        base.InitState(EnemyFSM);
    }
    public override void EnterState(EnemyFSM EnemyFSM)
    {
        base.EnterState(EnemyFSM);

        if (isDoScale)
        {
            EnemyFSM.transform.localScale = begScaleValue;
            EnemyFSM.transform.DOScale(endScaleValue, duringTime);

        }
        if (isSelfMove == false)
        {
            dir = EnemyFSM.rigidbody.velocity.normalized;
            distance = GetDistance(dir, EnemyFSM);
            return;
        }
        if (isDoMove)
        {
            float moveTime = duringTime;
            if (isRandomMoves)
            {
                dir = Random.insideUnitCircle;
                dir = dir.normalized;
            }
            distance = GetDistance(dir, EnemyFSM);
            if (speed != 0)
            {
                moveTime = distance / speed;
            }
            //Debug.Log("Distance" + distance+" movetime"+moveTime);
            DOTweenModulePhysics2D.DOMove(EnemyFSM.rigidbody, (Vector2)EnemyFSM.transform.position + dir * distance, moveTime);
        }
    }

    private float GetDistance(Vector2 dir, EnemyFSM EnemyFSM)
    {
        var res = Physics2D.Raycast(EnemyFSM.transform.position, dir, 100000, LayerMask.GetMask("Ground", "Wall"));
        return res.distance;
    }
    public override void OnTriggerEnter2D(EnemyFSM EnemyFSM, Collider2D collision)
    {
        base.OnTriggerEnter2D(EnemyFSM, collision);
        if (isTurnWhenHitWall && isDoMove && (((1 << collision.gameObject.layer) & LayerMask.GetMask("Ground", "Wall")) != 0))
        {
            HitWallEvent.Invoke();
            Vector2 nor = (EnemyFSM.collider.bounds.center - collision.bounds.ClosestPoint(EnemyFSM.collider.bounds.center)).normalized;
            dir = dir.normalized;
            var dot = Vector2.Dot(nor, dir);
            dir -= 2 * dot * nor;
            dir = dir.normalized;
            var dis = GetDistance(dir, EnemyFSM);
            var time = dis / speed;
            DOTween.Kill(EnemyFSM.rigidbody);
            DOTweenModulePhysics2D.DOMove(EnemyFSM.rigidbody, (Vector2)EnemyFSM.transform.position + dir * dis, time);

        }
    }
}

public class DoJumpState : EnemyFSMBaseState
{
    public Vector2 jumpDirection;
    private Vector2 realDir;
    public float jumpForce;

    public override void InitState(EnemyFSM enemyFSM)
    {
        base.InitState(enemyFSM);
        defaultAnimation.Transition.Events.Add(1f, () =>
        {
            if (enemyFSM != null)
                enemyFSM.rigidbody.velocity += realDir.normalized * jumpForce;
        });
    }
    public override void EnterState(EnemyFSM enemyFSM)
    {
        realDir = jumpDirection;
        realDir.x *= Mathf.Sign(enemyFSM.curr_x_scale);
        base.EnterState(enemyFSM);
    }
}

public class InTheAirState : EnemyFSMBaseState
{
    protected override bool isHideDefaultAnimation() { return true; }
    public ClipTransition transition;

    public int frameCount;
    public List<int> animAreaDivPoint;
    private List<float> animAreaDivPos;
    public List<float> speedAreaDivPoint;

    public float inTheAirLinearDrag = 1;
    public float onTheGroundlinearDrag = 15;
    public override void EnterState(EnemyFSM enemyFSM)
    {
        base.EnterState(enemyFSM);
        enemyFSM.rigidbody.drag = inTheAirLinearDrag;

        enemyFSM.AnimationPlay(transition);
        animAreaDivPos = new List<float>();
        for (int i = 0; i < animAreaDivPoint.Count; i++)
            animAreaDivPos.Add((float)((animAreaDivPoint[i] + 0.2) / frameCount));
    }
    public override void Act_State(EnemyFSM enemyFSM)
    {
        base.Act_State(enemyFSM);
        float v = -enemyFSM.rigidbody.velocity.y;
        int currentSpeedAreaIndex;
        for (currentSpeedAreaIndex = 0; currentSpeedAreaIndex < speedAreaDivPoint.Count; currentSpeedAreaIndex++)
            if (speedAreaDivPoint[currentSpeedAreaIndex] > v)
                break;
        float temp = enemyFSM.animancerCurrentPlaying.NormalizedTime;

        if (currentSpeedAreaIndex == 0 && temp > animAreaDivPos[currentSpeedAreaIndex])
        {
            enemyFSM.animancerCurrentPlaying.NormalizedTime = 0;
        }
        else if (currentSpeedAreaIndex == speedAreaDivPoint.Count && temp > 1)
        {
            enemyFSM.animancerCurrentPlaying.NormalizedTime = animAreaDivPos[currentSpeedAreaIndex - 1];
        }
        else if (currentSpeedAreaIndex != 0 && currentSpeedAreaIndex != speedAreaDivPoint.Count && (temp < animAreaDivPos[currentSpeedAreaIndex - 1] || temp > animAreaDivPos[currentSpeedAreaIndex]))
        {
            enemyFSM.animancerCurrentPlaying.NormalizedTime = animAreaDivPos[currentSpeedAreaIndex - 1];
        }
        //Log.ConsoleLog("curr ind {0} animation {1} v {2}", currentSpeedAreaIndex, temp, v);
    }


    public override bool ExitState(EnemyFSM enemyFSM)
    {
        enemyFSM.rigidbody.drag = onTheGroundlinearDrag;
        return base.ExitState(enemyFSM);
    }
}