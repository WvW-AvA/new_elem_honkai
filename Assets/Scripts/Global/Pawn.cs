using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
public class Pawn : SerializedMonoBehaviour
{
    [ShowInInspector, ReadOnly]
    private EElement m_attachedEleement;
    public EElement attachedElement
    {
        get { return m_attachedEleement; }
        set
        {
            m_attachedEleement = value;
        }
    }
    public int attachedElementMaxCount = 0;
    [ShowInInspector, ReadOnly]
    private int m_attachedElementCount;
    public int attachedElementCount
    {
        get { return m_attachedElementCount; }
        set
        {
            m_attachedElementCount = value;
        }
    }
    public virtual void AttachElement(EElement element, GameObject invoker)
    {
        if (element == EElement.NONE)
            return;
        if (attachedElement == EElement.NONE)
        {
            attachedElement = element;
            attachedElementCount++;
            return;
        }
        if (attachedElementMaxCount == 0)
            attachedElementMaxCount = Element.ElementMaxAttachLayer[attachedElement];

        EElementReaction reaction = EElementReaction.NONE;
        if (attachedElement == element && attachedElementCount < attachedElementMaxCount)
        {
            attachedElementCount++;
            return;
        }
        else
        {
            reaction = Element.GetElementReaction(attachedElement, element);//attached 已附着的元素 element 将要附着的元素
            attachedElement = EElement.NONE;
            attachedElementCount = 0;
        }
        switch (reaction)
        {
            case EElementReaction.NONE:
                ElementReaction_NONE_Invoke(invoker);
                break;
            case EElementReaction.BURST:
                ElementReaction_BURST_Invoke(invoker);
                break;
            case EElementReaction.HEAL:
                ElementReaction_HEAL_Invoke(invoker);
                break;
            case EElementReaction.PETRIFIED:
                ElementReaction_PETRIFIED_Invoke(invoker);
                break;
            case EElementReaction.STEAM:
                ElementReaction_STEAM_Invoke(invoker);
                break;
            case EElementReaction.LAVA:
                ElementReaction_LAVA_Invoke(invoker);
                break;
            case EElementReaction.MUDDY:
                ElementReaction_MUDDY_Invoke(invoker);
                break;
        }
    }

    protected virtual void ElementReaction_NONE_Invoke(GameObject invoker)
    {
    }
    protected virtual void ElementReaction_BURST_Invoke(GameObject invoker)
    {
        UIManager.CreateFloatStringIcon("炎爆", transform.position + Vector3.up * 3, Element.ElementReactionFloatStringColor[EElementReaction.BURST]);
    }
    protected virtual void ElementReaction_HEAL_Invoke(GameObject invoker)
    {
        UIManager.CreateFloatStringIcon("治愈", transform.position + Vector3.up * 3, Element.ElementReactionFloatStringColor[EElementReaction.HEAL]);
    }
    protected virtual void ElementReaction_PETRIFIED_Invoke(GameObject invoker)
    {
        UIManager.CreateFloatStringIcon("固化", transform.position + Vector3.up * 3, Element.ElementReactionFloatStringColor[EElementReaction.PETRIFIED]);
    }
    protected virtual void ElementReaction_STEAM_Invoke(GameObject invoker)
    {
        UIManager.CreateFloatStringIcon("蒸发", transform.position + Vector3.up * 3, Element.ElementReactionFloatStringColor[EElementReaction.STEAM]);
    }
    protected virtual void ElementReaction_LAVA_Invoke(GameObject invoker)
    {
        UIManager.CreateFloatStringIcon("熔化", transform.position + Vector3.up * 3, Element.ElementReactionFloatStringColor[EElementReaction.LAVA]);
    }
    protected virtual void ElementReaction_MUDDY_Invoke(GameObject invoker)
    {
        UIManager.CreateFloatStringIcon("粘滞", transform.position + Vector3.up * 3, Element.ElementReactionFloatStringColor[EElementReaction.MUDDY]);
    }
}
