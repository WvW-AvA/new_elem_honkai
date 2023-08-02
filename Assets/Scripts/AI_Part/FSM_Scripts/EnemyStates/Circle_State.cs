using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Circle_State : Idle_State
{
    public float angleSpeed;
    public float radio;
    [Tooltip("0为绕自身当前位置，1为绕玩家")]
    public int centreOfCircle = 0;
    public bool lock_x = false, lock_y = false;
    public bool isFaceToPlayer = true;
    private Vector2 centre = new Vector2(99999, 99999);
    private float timer;
    public override void Act_State(EnemyFSM EnemyFSM)
    {
        base.Act_State(EnemyFSM);
        EnemyFSM.GetPlayerDirection();
        if (DOTween.IsTweening(EnemyFSM.rigidbody))
            return;
        GetCentre(EnemyFSM);
        var endPos = GetTargetPos();
        var pos = Vector2.Lerp(EnemyFSM.transform.position, endPos, 0.3f);
        if (lock_x)
            EnemyFSM.rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        if (lock_y)
            EnemyFSM.rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        EnemyFSM.rigidbody.MovePosition(pos);
    }

    public override bool ExitState(EnemyFSM EnemyFSM)
    {
        EnemyFSM.rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        return true;
    }

    private void GetCentre(EnemyFSM fsm)
    {
        if (centreOfCircle == 0)
        {
            if (centre.Equals(new Vector2(99999, 99999)))
            {
                centre = fsm.transform.position;
            }

        }
        else if (centreOfCircle == 1)
        {
            centre = Player.instance.transform.position;
        }
    }
    private Vector2 GetTargetPos()
    {
        timer += Time.deltaTime;
        var angle = angleSpeed * timer;
        return (centre + radio * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));

    }
}
