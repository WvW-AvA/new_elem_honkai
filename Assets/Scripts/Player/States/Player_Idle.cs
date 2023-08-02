using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Player_Idle : PlayerFSMBaseState
{
    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        CameraManager.mainVirtualCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void OnCollisionStay2D(PlayerFSM playerFSM, Collision2D collision)
    {
        base.OnCollisionEnter2D(playerFSM, collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            playerFSM.isAllowDash = true;
        }
    }
}

public class Player_Apperance : PlayerFSMBaseState
{
    public SfxSO apperanceSfx;
    public Vector2 PositionCorrect;
    public float jumpForce;
    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        Player.instance.rigidbody.velocity += new Vector2(0, jumpForce);
        if (apperanceSfx != null)
            SfxManager.CreateSfx(apperanceSfx, PositionCorrect, playerFSM.gameObject);
    }
}

public class Player_Die : PlayerFSMBaseState
{
    public float delayTime = 2f;
    public override void InitState(PlayerFSM playerFSM)
    {
        base.InitState(playerFSM);
        defaultAnimation.Transition.Events.Clear();
        defaultAnimation.Transition.Events.Add(0.9f, PlayerDie);
    }

    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        BuffManager.RemoveAllBuff(Player.instance.gameObject);
        BuffManager.SetBuffAcceptable(false, Player.instance.gameObject);
        Player.instance.curr.isInvincible = true;
    }
    private void PlayerDie()
    {
        if (delayTime == 0)
            Player.instance.PlayerDie();
        else
            QuickCoroutineSystem.StartCoroutine(PlayerDie_co(delayTime));

    }
    IEnumerator PlayerDie_co(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Player.instance.PlayerDie();
    }
}