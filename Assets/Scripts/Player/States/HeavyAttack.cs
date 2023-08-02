using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Animancer;
using Cinemachine;
public class HeavyAttack : SkillBase
{
    public float maxAccumulateTime = 1f;
    public SignalSourceAsset accuCameraShakeSignalAsset;
    public ClipTransition accuLoop;

    public SignalSourceAsset lunchCameraShakeSignalAsset;
    public float shakeForce;

    public ClipTransition lunch;
    public SfxSO lunchSfx;
    public Vector2 lunchSfxOffset;

    protected float enterTime = 0;
    protected bool is_lunched = false;
    protected override bool isHideDefaultAnimation()
    {
        return true;
    }

    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        enterTime = InputManager.Keys[EKey.J].pressDuration;
        isEnableChangeState = false;
        is_lunched = false;
        playerFSM.AnimationPlay(accuLoop);
        if (accuCameraShakeSignalAsset != null)
            CameraManager.cameraShake(playerFSM.gameObject, accuCameraShakeSignalAsset);
    }

    public override void Act_State(PlayerFSM playerFSM)
    {
        base.Act_State(playerFSM);
        float dt = InputManager.Keys[EKey.J].pressDuration - enterTime;
        if (is_lunched == false && (dt > maxAccumulateTime || InputManager.Keys[EKey.J].isKeyUp))
        {
            is_lunched = true;
            isEnableChangeState = true;
            playerFSM.AnimationPlay(lunch);
            if (lunchCameraShakeSignalAsset != null)
                CameraManager.cameraShake(playerFSM.gameObject, lunchCameraShakeSignalAsset, shakeForce);
            LunchSfx(playerFSM);
        }
    }

    protected virtual void LunchSfx(PlayerFSM playerFSM)
    {
        SfxManager.CreateSfx(lunchSfx, lunchSfxOffset, playerFSM.gameObject);
    }
    public override bool ExitState(PlayerFSM playerFSM)
    {
        return base.ExitState(playerFSM);
    }

}
public class Fire_HeavyAttack : HeavyAttack
{
    public ClipTransition prepare;
    public SfxSO prepareSfx;
    public Vector2 prepareOffset;
    public SfxSO FireEnchantmentLunchSfx;
    public override void InitState(PlayerFSM playerFSM)
    {
        base.InitState(playerFSM);
        prepare.Transition.Events.Clear();
        prepare.Transition.Events.Add(new AnimancerEvent(0.98f, () => { playerFSM.AnimationPlay(accuLoop); }));
    }
    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        playerFSM.AnimationPlay(prepare);
        if (prepareSfx != null)
            SfxManager.CreateSfx(prepareSfx, prepareOffset, playerFSM.gameObject);
        if (accuCameraShakeSignalAsset != null)
            CameraManager.cameraShake(playerFSM.gameObject, accuCameraShakeSignalAsset);
    }

    protected override void LunchSfx(PlayerFSM playerFSM)
    {
        if (Player.instance.curr.isFireEnchantment == false)
        {
            base.LunchSfx(playerFSM);
            return;
        }
        SfxManager.CreateSfx(FireEnchantmentLunchSfx, lunchSfxOffset, playerFSM.gameObject);
    }
}
