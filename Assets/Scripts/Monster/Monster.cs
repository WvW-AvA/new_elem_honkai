using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
public class Monster : Pawn, Iinjured
{
    [SerializeField]
    protected EElement m_element;
    public virtual EElement element { get { return m_element; } set { m_element = value; } }
    [Range(1, 0x3f3f3f3f)]
    public int maxHp;
    [ShowInInspector]
    protected int m_currentHp;
    public int currentHp { get { return m_currentHp; } set { m_currentHp = value; } }

    public int baseDamage;

    private float m_moveSpeedRate = 1;
    public float MoveSpeedRate { get { return m_moveSpeedRate; } set { m_moveSpeedRate = value; } }
    public Collider2D bodyDamageCollider;
    public UnityEvent onMonsterDie;
    public void BodyAttackEnable(float delayTime, float keepTime)
    {
        if (bodyDamageCollider == null)
            return;
        QuickCoroutineSystem.StartCoroutine(BodyAttackEnable_co(delayTime, keepTime));
    }
    IEnumerator BodyAttackEnable_co(float delayTime, float keepTime)
    {
        bodyDamageCollider.enabled = false;
        yield return new WaitForSeconds(delayTime);
        if (bodyDamageCollider != null)
            bodyDamageCollider.enabled = true;
        yield return new WaitForSeconds(keepTime);
        if (bodyDamageCollider != null)
            bodyDamageCollider.enabled = false;
    }

    public virtual void GetHurt(DamageToken damage)
    {
        Log.Info(LogColor.Monster + "Get hurt {0}", damage.Calculate());
        AttachElement(damage.hitElement, damage.Giver);
        var damageValue = damage.Calculate();
        UIManager.CreateFloatStringIcon(damageValue.ToString(), transform.position, Element.ElementFloatStringColor[damage.hitElement]);
        currentHp -= damageValue;
    }

    public void GetHeal(int value)
    {
        Log.Info(LogColor.Monster + "Get Heal {0}", value);
        UIManager.CreateFloatStringIcon("+" + value.ToString(), transform.position, Element.ElementFloatStringColor[EElement.WATER]);
        currentHp += value;
    }
    private void Awake()
    {
        currentHp = maxHp;
    }

    public virtual void StatusReset()
    {
        currentHp = maxHp;
    }
    #region ElementReactionOverwrite
    protected override void ElementReaction_BURST_Invoke(GameObject invoker)
    {
        base.ElementReaction_BURST_Invoke(invoker);
        BuffManager.AddBuffList(Element.MonsterElementReactionBuffs[EElementReaction.BURST], invoker, gameObject);
    }
    protected override void ElementReaction_HEAL_Invoke(GameObject invoker)
    {
        base.ElementReaction_HEAL_Invoke(invoker);
        BuffManager.AddBuffList(Element.MonsterElementReactionBuffs[EElementReaction.HEAL], invoker, gameObject);
    }
    protected override void ElementReaction_LAVA_Invoke(GameObject invoker)
    {
        base.ElementReaction_LAVA_Invoke(invoker);
        BuffManager.AddBuffList(Element.MonsterElementReactionBuffs[EElementReaction.LAVA], invoker, gameObject);
    }
    protected override void ElementReaction_MUDDY_Invoke(GameObject invoker)
    {
        base.ElementReaction_MUDDY_Invoke(invoker);
        BuffManager.AddBuffList(Element.MonsterElementReactionBuffs[EElementReaction.MUDDY], invoker, gameObject);
    }
    protected override void ElementReaction_NONE_Invoke(GameObject invoker)
    {
        base.ElementReaction_NONE_Invoke(invoker);
        BuffManager.AddBuffList(Element.MonsterElementReactionBuffs[EElementReaction.NONE], invoker, gameObject);
    }
    protected override void ElementReaction_PETRIFIED_Invoke(GameObject invoker)
    {
        base.ElementReaction_PETRIFIED_Invoke(invoker);
        BuffManager.AddBuffList(Element.MonsterElementReactionBuffs[EElementReaction.PETRIFIED], invoker, gameObject);
    }
    protected override void ElementReaction_STEAM_Invoke(GameObject invoker)
    {
        base.ElementReaction_STEAM_Invoke(invoker);
        BuffManager.AddBuffList(Element.MonsterElementReactionBuffs[EElementReaction.STEAM], invoker, gameObject);
    }
    #endregion

}
