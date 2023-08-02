using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Animancer;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
public class NormalAttack : SkillBase
{
    public float ATKIndexResetTime;
    public List<ClipTransition> ATKAimation;
    /// <summary>
    /// default InternalCD is 0.3
    /// </summary>
    public List<float> ATKInternalCD;
    public List<SfxSO> ATKSfx;

    public bool isCorrectSfxPos;
    [ShowIf("isCorrectSfxPos")]
    public List<Vector2> sfxPosCorrect;

    private int m_currentATKIndex = 0;
    public int currentATKIndex
    {
        get { return m_currentATKIndex; }
        set
        {
            if (value >= ATKAimation.Count)
                value = 0;
            m_currentATKIndex = value;
        }
    }
    protected bool isTimimg = false;
    private float m_internalCDTimer;
    protected float InternalCDTimer
    {
        get { return m_internalCDTimer; }
        set
        {
            m_internalCDTimer = value;
            int temp = (currentATKIndex - 1) < 0 ? ATKInternalCD.Count - 1 : currentATKIndex - 1;
            if (m_internalCDTimer > ATKInternalCD[temp])
            {
                isEnableChangeState = true;
                isTimimg = false;
            }
        }
    }
    protected override bool isHideDefaultAnimation()
    {
        return true;
    }
    protected override bool isShowCDField()
    {
        return false;
    }

    public override void InitState(PlayerFSM playerFSM)
    {
        base.InitState(playerFSM);
        Timer = TimerManager.Schedule(name, ATKIndexResetTime, 0, true, TimeoutCallBack);
        for (int i = ATKInternalCD.Count; i < ATKAimation.Count; i++)
            ATKInternalCD.Add(0.3f);
        if (isCorrectSfxPos)
            for (int i = sfxPosCorrect.Count; i < ATKSfx.Count; i++)
                sfxPosCorrect.Add(Vector2.zero);
    }
    public override void TimeoutCallBack()
    {
        base.TimeoutCallBack();
        Log.Info("timer call back");
        currentATKIndex = 0;
    }
    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        ATK(playerFSM);
    }
    public override void Act_State(PlayerFSM playerFSM)
    {
        base.Act_State(playerFSM);
        if (isTimimg)
            InternalCDTimer += Time.deltaTime;
    }
    public virtual void ATK(PlayerFSM playerFSM)
    {
        InternalCDTimer = 0;
        isTimimg = true;
        Timer.Reload();
        isEnableChangeState = false;
        if (ATKAimation[currentATKIndex] != null)
            playerFSM.AnimationPlay(ATKAimation[currentATKIndex]);
        if (currentATKIndex < ATKSfx.Count)
        {
            Vector2 pos = Vector3.zero;
            if (isCorrectSfxPos)
                pos += sfxPosCorrect[currentATKIndex];
            SfxManager.CreateSfx(ATKSfx[currentATKIndex], pos, playerFSM.gameObject);
        }
        Log.ConsoleLog("Attack {0}", currentATKIndex);
        currentATKIndex++;
    }
}

public class Fire_NormalAttack : NormalAttack
{
    public float secondATKJumpForce;
    public List<SfxSO> FireEnchantmentATKSfx;

    public float inTheAirLinearDrag = 1;
    public float onTheGroundlinearDrag = 10;
    public override void ATK(PlayerFSM playerFSM)
    {
        if (currentATKIndex == 1)
        {
            playerFSM.rigidbody.drag = inTheAirLinearDrag;
            Player.instance.rigidbody.velocity += new Vector2(0, secondATKJumpForce);
        }
        if (Player.instance.curr.isFireEnchantment == false)
        {
            base.ATK(playerFSM);
            return;
        }
        InternalCDTimer = 0;
        isTimimg = true;
        Timer.Reload();
        isEnableChangeState = false;
        if (ATKAimation[currentATKIndex] != null)
            playerFSM.AnimationPlay(ATKAimation[currentATKIndex]);
        if (currentATKIndex < FireEnchantmentATKSfx.Count)
        {
            Vector2 pos = Vector3.zero;
            if (isCorrectSfxPos)
                pos += sfxPosCorrect[currentATKIndex];
            SfxManager.CreateSfx(FireEnchantmentATKSfx[currentATKIndex], pos, playerFSM.gameObject);
        }
        Log.ConsoleLog("Attack {0}", currentATKIndex);
        currentATKIndex++;
    }

    public override bool ExitState(PlayerFSM playerFSM)
    {
        playerFSM.rigidbody.drag = onTheGroundlinearDrag;
        return base.ExitState(playerFSM);
    }
    public override void OnCollisionEnter2D(PlayerFSM playerFSM, Collision2D collision)
    {
        base.OnCollisionEnter2D(playerFSM, collision);
        if (LayerMask.NameToLayer("Ground") == collision.gameObject.layer && currentATKIndex == 2)
        {
            currentATKIndex = 0;
        }
    }
}
