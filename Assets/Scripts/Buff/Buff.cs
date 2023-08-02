using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using DG.Tweening;
using Cinemachine;
public enum BuffType
{
    ElementDamageBuff,
    SpeedChangeBuff
}

[Serializable]
public class BuffBase
{
    [ReadOnly]
    public BuffType buffType;
    public string buffName;
    [SerializeField]
    private float m_duringTime;
    public float DuringTime { get { return m_duringTime; } set { m_duringTime = value; } }
    private float m_currTime;
    public float CurrTime { get { return m_currTime; } set { m_currTime = value; } }

    private GameObject m_giver;
    public GameObject Giver { get { return m_giver; } set { m_giver = value; } }
    private GameObject m_bearer;
    public GameObject Bearer { get { return m_bearer; } set { m_bearer = value; } }

    public void Enter()
    {
        CurrTime = DuringTime;
        OnBuffEnter();
    }
    public void Update()
    {
        CurrTime -= Time.deltaTime;
        if (CurrTime <= 0)
        {
            Exit();
        }
        OnBuffStay();
    }

    public void Exit()
    {
        BuffManager.RemoveBuff(this, Bearer);
    }
    public virtual void OnBuffEnter() { }
    public virtual void OnBuffStay() { }
    public virtual void OnBuffExit() { }
}
public class SpeedChangeBuff : BuffBase
{
    public float speedChangeRate;

    public override void OnBuffEnter()
    {
        base.OnBuffEnter();
        if (Bearer.tag == "Player")
        {
            foreach (var p in Player.instance.parameterDict.Values)
                p.moveSpeed *= speedChangeRate;
        }
        if (Bearer.tag == "Monster")
            Bearer.GetComponent<Monster>().MoveSpeedRate *= speedChangeRate;

    }

    public override void OnBuffExit()
    {
        base.OnBuffExit();
        if (Bearer.tag == "Player")
        {
            foreach (var p in Player.instance.parameterDict.Values)
                p.moveSpeed /= speedChangeRate;
        }
        if (Bearer.tag == "Monster")
            Bearer.GetComponent<Monster>().MoveSpeedRate /= speedChangeRate;
    }
}
public class RestrainBuff : SpeedChangeBuff
{
    public bool activeOnlyOnArea;
    public override void OnBuffEnter()
    {
        speedChangeRate = 0.001f;
        base.OnBuffEnter();
        InputManager.SetPlayerBehaviousActive(false);
    }
    public override void OnBuffExit()
    {
        base.OnBuffExit();
        InputManager.SetPlayerBehaviousActive(true);
    }
}
public class GravityChangeBuff : BuffBase
{
    public float gravityChangeRate;

    public override void OnBuffEnter()
    {
        base.OnBuffEnter();
        if (Bearer.tag == "Player")
        {
            Player.instance.parameterDict[EElement.NONE].gravityRate *= gravityChangeRate;
            Player.instance.parameterDict[EElement.FIRE].gravityRate *= gravityChangeRate;
            Player.instance.parameterDict[EElement.TERRA].gravityRate *= gravityChangeRate;
            Player.instance.parameterDict[EElement.WATER].gravityRate *= gravityChangeRate;
            Player.instance.rigidbody.gravityScale = 9.8f * Player.parameter.gravityRate;
        }
        else
            Bearer.GetComponent<Rigidbody2D>().gravityScale *= gravityChangeRate;
        Debug.Log("Enter");

    }

    public override void OnBuffExit()
    {
        base.OnBuffExit();
        if (Bearer.tag == "Player")
        {
            Player.instance.parameterDict[EElement.NONE].gravityRate /= gravityChangeRate;
            Player.instance.parameterDict[EElement.FIRE].gravityRate /= gravityChangeRate;
            Player.instance.parameterDict[EElement.TERRA].gravityRate /= gravityChangeRate;
            Player.instance.parameterDict[EElement.WATER].gravityRate /= gravityChangeRate;
            Player.instance.rigidbody.gravityScale = 9.8f * Player.parameter.gravityRate;
        }
        else
            Bearer.GetComponent<Rigidbody2D>().gravityScale /= gravityChangeRate;

        Debug.Log("Exit");
    }
}

public class AddForceBuff : BuffBase
{
    public enum EAddForceDirectionType
    {
        Horizental,
        Vertical,
        FromGiver,
        Custom,
        CollisionNormal
    }
    public float forceValue;
    public float forceDuration = 0.5f;
    public EAddForceDirectionType directionType;
    public bool IsShowDirection { get { return directionType != EAddForceDirectionType.Custom; } }
    [HideIf("IsShowDirection")]
    public Vector2 direction;
    public override void OnBuffEnter()
    {
        base.OnBuffEnter();
        switch (directionType)
        {
            case EAddForceDirectionType.Horizental:
                direction = new Vector2(1, 0);
                break;
            case EAddForceDirectionType.Vertical:
                direction = new Vector2(0, 1);
                break;
            case EAddForceDirectionType.FromGiver:
                direction = ((Vector2)(Bearer.transform.position - Giver.transform.position)).normalized;
                break;
            case EAddForceDirectionType.CollisionNormal:
                {
                    var nearPoint = Giver.GetComponent<Collider2D>().ClosestPoint(Bearer.transform.position);
                    direction = ((Vector2)Bearer.transform.position - nearPoint).normalized;
                }
                break;
        }
        Log.Info("{0} GameObject:{1} Buff:{2} Enter add force {3}",
            LogColor.BuffManager, LogColor.Dye(LogColor.EColor.blue, Bearer.name), LogColor.Dye(LogColor.EColor.green, buffName), direction * forceValue);
        Bearer.GetComponent<Rigidbody2D>().DOMove((Vector2)Bearer.transform.position + direction * forceValue, 0.5f);
    }
}
public interface Iinjured
{
    abstract void GetHurt(DamageToken damage);
    abstract void GetHeal(int value);
}
public class ElementDamageBuff : BuffBase
{
    private enum DamageMode
    {
        Suddently,
        Continue
    }
    public EElement element;
    public float buffDamageRate;
    public EDamageBaseOn damageBaseOn;
    [ShowIf("isConstDamageValue")]
    public int damageValue;
    [SerializeField]
    private DamageMode mode;
    [ShowIf("isContinue")]
    public bool activeOnlyOnArea;//wabmf
    public int times;
    private float piece = 999;
    private float piece_timer = 0;

    public bool isConstDamageValue() { return damageBaseOn == EDamageBaseOn.constValue; }
    public bool isContinue() { return mode == DamageMode.Continue; }

    public override void OnBuffEnter()
    {
        base.OnBuffEnter();
        Log.Info("{0} GameObject:{1} Buff:{2} Enter",
            LogColor.BuffManager, LogColor.Dye(LogColor.EColor.blue, Bearer.name), LogColor.Dye(LogColor.EColor.green, buffName));
        if (mode == DamageMode.Continue)
        {
            piece = DuringTime / times;
        }
        if (mode == DamageMode.Suddently)
        {
            for (int i = 0; i < times; i++)
            {
                makeDamage();
            }
        }
    }

    public override void OnBuffStay()
    {
        base.OnBuffStay();

        if (mode == DamageMode.Continue)
        {
            piece_timer += Time.deltaTime;
            if (piece_timer >= piece)
            {
                makeDamage();
                piece_timer = 0;
            }
        }
    }
    private void makeDamage()
    {
        DamageToken damage = new DamageToken();
        damage.Giver = Giver;
        damage.hitElement = element;
        damage.rate = buffDamageRate;
        if (Bearer.tag == "Monster")
        {
            if (damageBaseOn == EDamageBaseOn.SelfDamage)
            {
                damage.baseDam = Player.parameter.baseDamage;
            }
            else if (damageBaseOn == EDamageBaseOn.SelfHP)
            {
                damage.baseDam = Player.instance.curr.hp;
            }
            else if (damageBaseOn == EDamageBaseOn.TargetHP)
            {
                damage.baseDam = Bearer.GetComponent<Monster>().currentHp;
            }
            else if (damageBaseOn == EDamageBaseOn.constValue)
            {
                damage.baseDam = damageValue;
            }
            Bearer.GetComponent<Monster>().GetHurt(damage);
        }
        else
        if (Bearer.tag == "Player")
        {
            if (damageBaseOn == EDamageBaseOn.SelfDamage)
            {
                damage.baseDam = Giver.GetComponent<Monster>().baseDamage;
            }
            else if (damageBaseOn == EDamageBaseOn.SelfHP)
            {
                damage.baseDam = Giver.GetComponent<Monster>().currentHp;
            }
            else if (damageBaseOn == EDamageBaseOn.TargetHP)
            {
                damage.baseDam = Player.instance.curr.hp;
            }
            else if (damageBaseOn == EDamageBaseOn.constValue)
            {
                damage.baseDam = damageValue;
            }
            Player.instance.GetHurt(damage);
        }
        else if (Bearer.tag == "ElementClock")

        {
            Bearer.GetComponent<ElementClock>().GetHurt(damage);
        }
    }
}
public class HealBuff : BuffBase
{
    private enum EHealMode
    {
        Suddently,
        Continue
    }
    private enum EHealValueMode
    {
        ConstValue,
        PercentValue
    }
    private enum EHealTargetMode
    {
        Bearer,
        Giver
    }
    [SerializeField]
    private EHealTargetMode healTargetMode;
    [SerializeField]
    private EHealValueMode healValueMode;
    [ShowIf("isConstHealValue")]
    public int healValue;
    [HideIf("isConstHealValue")]
    public float healPercent;
    [SerializeField]
    private EHealMode healMode;
    [ShowIf("isContinue")]
    public bool activeOnlyOnArea;
    public int times;
    private float piece = 999;
    private float piece_timer = 0;
    private GameObject healTarget;

    public bool isConstHealValue() { return healValueMode == EHealValueMode.ConstValue; }
    public bool isContinue() { return healMode == EHealMode.Continue; }

    public override void OnBuffEnter()
    {
        base.OnBuffEnter();
        Log.Info("{0} GameObject:{1} Buff:{2} Enter",
            LogColor.BuffManager, LogColor.Dye(LogColor.EColor.blue, Bearer.name), LogColor.Dye(LogColor.EColor.green, buffName));
        if (healTargetMode == EHealTargetMode.Bearer)
            healTarget = Bearer;
        else if (healTargetMode == EHealTargetMode.Giver)
            healTarget = Giver;

        if (healMode == EHealMode.Continue)
        {
            piece = DuringTime / times;
        }
        else if (healMode == EHealMode.Suddently)
        {
            for (int i = 0; i < times; i++)
            {
                makeHeal();
            }
        }

    }

    public override void OnBuffStay()
    {
        base.OnBuffStay();

        if (healMode == EHealMode.Continue)
        {
            piece_timer += Time.deltaTime;
            if (piece_timer >= piece)
            {
                makeHeal();
                piece_timer = 0;
            }
        }
    }
    private void makeHeal()
    {
        int heal = 0;
        if (healTarget.tag == "Monster")
        {
            if (healValueMode == EHealValueMode.ConstValue)
                heal = healValue;
            else if (healValueMode == EHealValueMode.PercentValue)
                heal = (int)(healTarget.GetComponent<Monster>().maxHp * healPercent);
            healTarget.GetComponent<Monster>().GetHeal(heal);
        }
        else
        if (healTarget.tag == "Player")
        {
            if (healValueMode == EHealValueMode.ConstValue)
                heal = healValue;
            else if (healValueMode == EHealValueMode.PercentValue)
                heal = (int)(Player.parameter.maxHP * healPercent);
            Player.instance.GetHeal(heal);
        }
    }
}

public class TimeScaleBuff : BuffBase
{
    public float scale = 0;
    [HideIf("isFrameCount")]
    public float keepTime = 0.05f;

    public bool isFrameCount = false;
    [ShowIf("isFrameCount")]
    public int frameCount = 2;
    public override void OnBuffEnter()
    {
        base.OnBuffEnter();
        if (isFrameCount)
            TimeScaleManager.changeTimeScaleForFrames(frameCount, scale);
        else
            TimeScaleManager.changeTimeScaleForSeconds(keepTime, scale);
    }
}

public class CameraShakeBuff : BuffBase
{
    public SignalSourceAsset signalAssest;

    public bool customForce = false;
    [ShowIf("customForce")]
    public float force = 1;
    [ShowIf("customForce")]
    public bool customDirection = false;
    [ShowIf("customDirection")]
    public Vector2 direction = Vector2.one;

    public override void OnBuffEnter()
    {
        base.OnBuffEnter();
        if (customForce)
        {
            if (customDirection)
                CameraManager.cameraShake(Bearer, signalAssest, direction * force);
            else
                CameraManager.cameraShake(Bearer, signalAssest, force);
        }
        else
            CameraManager.cameraShake(Bearer, signalAssest);
    }
}

public class HitSfxBuff : BuffBase
{
    public List<SfxSO> damagedSfx;
    public List<SfxSO> resistSfx;
    public List<SfxSO> weakSfx;

    public float sfxPositionLimit = 4;
    public override void OnBuffEnter()
    {
        base.OnBuffEnter();
        if (Bearer.tag == "Monster")
        {
            RandomSelectSfx(damagedSfx);
        }
        else if (Bearer.tag == "ElementClock")
        {
            RandomSelectSfx(resistSfx);
        }
        else
        {
            RandomSelectSfx(weakSfx);
        }
    }

    private void RandomSelectSfx(List<SfxSO> sfxes)
    {
        if (sfxes == null || sfxes.Count == 0)
            return;
        var direction = (Vector2)(Bearer.transform.position - Giver.transform.position);
        var pos = direction.sqrMagnitude > (sfxPositionLimit * sfxPositionLimit) ? (direction.normalized * sfxPositionLimit) : (direction);
        direction = direction.normalized;
        int ind = UnityEngine.Random.Range(0, sfxes.Count);
        pos += (Vector2)Giver.transform.position;
        SfxManager.CreateSfx(sfxes[ind], pos, null, Mathf.Sign(Giver.transform.localScale.x));
    }
}

public class SpriteBlinkBuff : BuffBase
{
    public float blinkTimes = 5;
    [Range(0, 1)]
    public float alpha = 0;

    private float blinkGap;
    private Color color;

    private float beforeAlpha;
    public override void OnBuffEnter()
    {
        base.OnBuffEnter();
        blinkGap = DuringTime / blinkTimes;
        color = Bearer.GetComponent<SpriteRenderer>().color;
        beforeAlpha = color.a;
    }

    public override void OnBuffStay()
    {
        base.OnBuffStay();
        if ((int)(CurrTime / blinkGap) % 2 == 0)
        {
            color.a = beforeAlpha;
        }
        else
        {
            color.a = alpha;
        }
        Bearer.GetComponent<SpriteRenderer>().color = color;
    }

    public override void OnBuffExit()
    {
        base.OnBuffExit();
        color.a = beforeAlpha;
        Bearer.GetComponent<SpriteRenderer>().color = color;
    }
}

public class InvincibleBuff : BuffBase
{
    public override void OnBuffEnter()
    {
        base.OnBuffEnter();
        if (Bearer.tag != "Player")
            return;
        Player.instance.curr.isInvincible = true;
    }

    public override void OnBuffExit()
    {
        base.OnBuffExit();
        if (Bearer.tag != "Player")
            return;
        Player.instance.curr.isInvincible = false;
    }
}

public class AudioPlayBuff : BuffBase
{
    public AudioClip clip;
    public float delayTime;

    public override void OnBuffEnter()
    {
        base.OnBuffEnter();
        AudioManager.PlayOneShot(clip, Bearer.transform.position, delayTime);

    }
}

public class CreatSfxBuff : BuffBase
{
    public enum ESfxCreateMode
    {
        Giver,
        Bearer,
        CustomPos
    }

    public ESfxCreateMode createMode;
    public SfxSO sfx;
    public bool isSetParent = false;
    [HideIf("isSetParent")]
    public float x_scale;
    public float delayTime;

    [ShowIf("isCustomPos")]
    public Vector2 Pos;

    public bool isCustomPos { get { return createMode == ESfxCreateMode.CustomPos; } }

    public override void OnBuffEnter()
    {
        base.OnBuffEnter();
        GameObject sfxOwner = Giver;
        if (isSetParent == true && createMode != ESfxCreateMode.CustomPos)
        {
            Pos = Vector2.zero;
            SfxManager.CreateSfxDelay(delayTime, sfx, Pos, sfxOwner, 1);
        }
        else
        {
            if (createMode == ESfxCreateMode.Bearer)
                Pos = Bearer.transform.position;
            else if (createMode == ESfxCreateMode.Giver)
                Pos = Giver.transform.position;
            SfxManager.CreateSfxDelay(delayTime, sfx, Pos, sfxOwner, Mathf.Sign(x_scale), false);
        }
    }
}
