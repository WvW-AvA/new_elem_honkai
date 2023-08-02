using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Animancer;
public class Map_DoTween_State : MapObjectFSMBaseState
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

    private float distance;

    public bool isDoScale;
    [ShowIfGroup("DoTween Scale Config", Condition = "isDoScale")]
    public Vector2 begScaleValue;
    [ShowIfGroup("DoTween Scale Config", Condition = "isDoScale")]
    public Vector2 endScaleValue;

    public bool isDoRotation;
    [ShowIf("DoTween Rotataion Config", Condition = "isDoRotation")]
    public float angle;

    public override void InitState(MapObjectFSM MapObjectFSM)
    {
        base.InitState(MapObjectFSM);
    }
    public override void EnterState(MapObjectFSM MapObjectFSM)
    {
        base.EnterState(MapObjectFSM);

        if (isDoScale)
        {
            MapObjectFSM.transform.localScale = begScaleValue;
            MapObjectFSM.transform.DOScale(endScaleValue, duringTime);

        }
        if (isSelfMove == false)
        {
            dir = MapObjectFSM.rigidbody.velocity.normalized;
            distance = GetDistance(dir, MapObjectFSM);
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
            dir.x *= Mathf.Sign(MapObjectFSM.curr_x_scale);
            distance = moveTime * speed;
            //Debug.Log("Distance" + distance+" movetime"+moveTime);
            DOTweenModulePhysics2D.DOMove(MapObjectFSM.rigidbody, (Vector2)MapObjectFSM.transform.position + dir * distance, moveTime);
        }
    }

    private float GetDistance(Vector2 dir, MapObjectFSM MapObjectFSM)
    {
        var res = Physics2D.Raycast(MapObjectFSM.transform.position, dir, 100000, LayerMask.GetMask("Ground", "Wall"));
        return res.distance;
    }
    public override void OnTriggerEnter2D(MapObjectFSM MapObjectFSM, Collider2D collision)
    {
        base.OnTriggerEnter2D(MapObjectFSM, collision);
        if (isTurnWhenHitWall && isDoMove && (((1 << collision.gameObject.layer) & LayerMask.GetMask("Ground", "Wall")) != 0))
        {
            HitWallEvent.Invoke();
            Vector2 nor = (MapObjectFSM.collider.bounds.center - collision.bounds.ClosestPoint(MapObjectFSM.collider.bounds.center)).normalized;
            dir = dir.normalized;
            var dot = Vector2.Dot(nor, dir);
            dir -= 2 * dot * nor;
            dir = dir.normalized;
            var dis = GetDistance(dir, MapObjectFSM);
            var time = dis / speed;
            DOTween.Kill(MapObjectFSM.rigidbody);
            DOTweenModulePhysics2D.DOMove(MapObjectFSM.rigidbody, (Vector2)MapObjectFSM.transform.position + dir * dis, time);

        }
    }
}

public class Map_DoJumpState : MapObjectFSMBaseState
{
    public Vector2 jumpDirection;
    private Vector2 realDir;
    public float jumpForce;

    public override void InitState(MapObjectFSM mapObjectFSM)
    {
        base.InitState(mapObjectFSM);
        defaultAnimation.Transition.Events.Clear();
        defaultAnimation.Transition.Events.Add(1f, () =>
        {
            if (mapObjectFSM != null)
                mapObjectFSM.rigidbody.velocity += realDir.normalized * jumpForce;
        });
    }
    public override void EnterState(MapObjectFSM mapObjectFSM)
    {
        realDir = jumpDirection;
        realDir.x *= Mathf.Sign(mapObjectFSM.curr_x_scale);
        base.EnterState(mapObjectFSM);
    }
}

public class Map_InTheAirState : MapObjectFSMBaseState
{
    protected override bool isHideDefaultAnimation() { return true; }
    public ClipTransition transition;

    public int frameCount;
    public List<int> animAreaDivPoint;
    private List<float> animAreaDivPos;
    public List<float> speedAreaDivPoint;

    public float inTheAirLinearDrag = 1;
    public float onTheGroundlinearDrag = 15;
    public override void EnterState(MapObjectFSM mapObjectFSM)
    {
        base.EnterState(mapObjectFSM);
        mapObjectFSM.rigidbody.drag = inTheAirLinearDrag;

        mapObjectFSM.AnimationPlay(transition);
        animAreaDivPos = new List<float>();
        for (int i = 0; i < animAreaDivPoint.Count; i++)
            animAreaDivPos.Add((float)((animAreaDivPoint[i] + 0.2) / frameCount));
    }
    public override void Act_State(MapObjectFSM mapObjectFSM)
    {
        base.Act_State(mapObjectFSM);
        float v = -mapObjectFSM.rigidbody.velocity.y;
        int currentSpeedAreaIndex;
        for (currentSpeedAreaIndex = 0; currentSpeedAreaIndex < speedAreaDivPoint.Count; currentSpeedAreaIndex++)
            if (speedAreaDivPoint[currentSpeedAreaIndex] > v)
                break;
        float temp = mapObjectFSM.animancerCurrentPlaying.NormalizedTime;

        if (currentSpeedAreaIndex == 0 && temp > animAreaDivPos[currentSpeedAreaIndex])
        {
            mapObjectFSM.animancerCurrentPlaying.NormalizedTime = 0;
        }
        else if (currentSpeedAreaIndex == speedAreaDivPoint.Count && temp > 1)
        {
            mapObjectFSM.animancerCurrentPlaying.NormalizedTime = animAreaDivPos[currentSpeedAreaIndex - 1];
        }
        else if (currentSpeedAreaIndex != 0 && currentSpeedAreaIndex != speedAreaDivPoint.Count && (temp < animAreaDivPos[currentSpeedAreaIndex - 1] || temp > animAreaDivPos[currentSpeedAreaIndex]))
        {
            mapObjectFSM.animancerCurrentPlaying.NormalizedTime = animAreaDivPos[currentSpeedAreaIndex - 1];
        }
        //Log.ConsoleLog("curr ind {0} animation {1} v {2}", currentSpeedAreaIndex, temp, v);
    }


    public override bool ExitState(MapObjectFSM mapObjectFSM)
    {
        mapObjectFSM.rigidbody.drag = onTheGroundlinearDrag;
        return base.ExitState(mapObjectFSM);
    }
}