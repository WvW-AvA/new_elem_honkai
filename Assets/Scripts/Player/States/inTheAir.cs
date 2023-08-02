using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animancer;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
public class inTheAir : PlayerFSMBaseState
{
    protected override bool isHideDefaultAnimation() { return true; }
    public ClipTransition transition;

    public int frameCount;
    public List<int> animAreaDivPoint;
    private List<float> animAreaDivPos;
    public List<float> speedAreaDivPoint;

    public float inTheAirLinearDrag = 1;
    public float onTheGroundlinearDrag = 10;
    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        playerFSM.rigidbody.drag = inTheAirLinearDrag;
        playerFSM.jumpMaxHight = 0;
        playerFSM.horizontalMaxNegativeSpeed = 0;
        playerFSM.verticalMaxPositiveSpeed = 0;
        playerFSM.horizontalMaxPositiveSpeed = 0;
        playerFSM.verticalMaxNegativeSpeed = 0;

        playerFSM.AnimationPlay(transition);
        animAreaDivPos = new List<float>();
        for (int i = 0; i < animAreaDivPoint.Count; i++)
            animAreaDivPos.Add((float)((animAreaDivPoint[i] + 0.2) / frameCount));
    }
    public override void Act_State(PlayerFSM playerFSM)
    {
        base.Act_State(playerFSM);
        float v = -playerFSM.rigidbody.velocity.y;
        int currentSpeedAreaIndex;
        for (currentSpeedAreaIndex = 0; currentSpeedAreaIndex < speedAreaDivPoint.Count; currentSpeedAreaIndex++)
            if (speedAreaDivPoint[currentSpeedAreaIndex] > v)
                break;
        float temp = playerFSM.animancerCurrentPlaying.NormalizedTime;

        if (currentSpeedAreaIndex == 0 && temp > animAreaDivPos[currentSpeedAreaIndex])
        {
            playerFSM.animancerCurrentPlaying.NormalizedTime = 0;
        }
        else if (currentSpeedAreaIndex == speedAreaDivPoint.Count && temp > 1)
        {
            playerFSM.animancerCurrentPlaying.NormalizedTime = animAreaDivPos[currentSpeedAreaIndex - 1];
        }
        else if (currentSpeedAreaIndex != 0 && currentSpeedAreaIndex != speedAreaDivPoint.Count && (temp < animAreaDivPos[currentSpeedAreaIndex - 1] || temp > animAreaDivPos[currentSpeedAreaIndex]))
        {
            playerFSM.animancerCurrentPlaying.NormalizedTime = animAreaDivPos[currentSpeedAreaIndex - 1];
        }
        //Log.ConsoleLog("curr ind {0} animation {1} v {2}", currentSpeedAreaIndex, temp, v);
    }

    public override void FixAct_State(PlayerFSM playerFSM)
    {
        base.FixAct_State(playerFSM);
        playerFSM.rigidbody.velocity = new Vector2(Player.parameter.moveSpeed * InputManager.Axises[EAxis.Horizontal].value, Player.instance.rigidbody.velocity.y);
        playerFSM.verticalMaxPositiveSpeed = Mathf.Max(playerFSM.rigidbody.velocity.y, playerFSM.verticalMaxPositiveSpeed);
        playerFSM.verticalMaxNegativeSpeed = Mathf.Min(playerFSM.rigidbody.velocity.y, playerFSM.verticalMaxNegativeSpeed);
        playerFSM.horizontalMaxPositiveSpeed = Mathf.Max(playerFSM.rigidbody.velocity.x, playerFSM.horizontalMaxPositiveSpeed);
        playerFSM.horizontalMaxNegativeSpeed = Mathf.Min(playerFSM.rigidbody.velocity.x, playerFSM.horizontalMaxNegativeSpeed);
        var ray = Physics2D.Raycast(playerFSM.transform.position, Vector2.down);
        playerFSM.jumpMaxHight = Mathf.Max(playerFSM.jumpMaxHight, ray.distance);
    }

    public override bool ExitState(PlayerFSM playerFSM)
    {
        playerFSM.rigidbody.drag = onTheGroundlinearDrag;
        return base.ExitState(playerFSM);
    }


}
