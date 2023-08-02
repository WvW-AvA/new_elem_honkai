using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
public enum EElement
{
    NONE = 0,
    FIRE = 1,
    WATER = 2,
    TERRA = 3
}

public enum EElementReaction
{
    NONE = 0,//无反应
    BURST = 1,//炎爆
    HEAL = 2,//治愈
    PETRIFIED = 3,//固化
    STEAM = 4,//蒸发
    LAVA = 5,//熔岩
    MUDDY = 6//泥泞
}

public class Element
{


    public static Dictionary<EElement, Color> ElementFloatStringColor;
    public static Dictionary<EElement, int> ElementMaxAttachLayer;
    public static Dictionary<EElement, Sprite> ElementIcons;
    public static Dictionary<EElementReaction, Color> ElementReactionFloatStringColor;
    public static Dictionary<EElementReaction, List<BuffBase>> PlayerElementReactionBuffs;
    public static Dictionary<EElementReaction, List<BuffBase>> MonsterElementReactionBuffs;


    private static float FireRate = 1f;
    private static float WaterRate = 1f;
    private static float TerraRate = 1f;
    private static float restrainRate = 2f;
    private static float normalRate = 1f;
    private static float resistRate = 0.5f;

    private static List<List<float>> resistRateMatrix = new List<List<float>>()
    {
        new List<float>(){normalRate,resistRate,resistRate,resistRate },
        new List<float>(){normalRate,normalRate,restrainRate,resistRate },
        new List<float>(){normalRate,restrainRate,normalRate,resistRate },
        new List<float>(){normalRate,normalRate,normalRate,restrainRate}
    };

    private static List<List<EElementReaction>> reactionMatrix = new List<List<EElementReaction>>()
    {
        new List<EElementReaction>(){EElementReaction.NONE,EElementReaction.NONE,EElementReaction.NONE,EElementReaction.NONE},
        new List<EElementReaction>(){EElementReaction.NONE,EElementReaction.BURST,EElementReaction.STEAM,EElementReaction.LAVA},
        new List<EElementReaction>(){EElementReaction.NONE,EElementReaction.STEAM,EElementReaction.HEAL,EElementReaction.MUDDY},
        new List<EElementReaction>(){EElementReaction.NONE,EElementReaction.LAVA,EElementReaction.MUDDY,EElementReaction.PETRIFIED}

    };
    public static float GetElementResistRate(EElement attackEle, EElement attackedEle)
    {
        return resistRateMatrix[((int)attackEle)][((int)attackedEle)];
    }

    public static EElementReaction GetElementReaction(EElement baseElement, EElement invokeElement)
    {
        return reactionMatrix[((int)baseElement)][((int)invokeElement)];
    }
    public static float ElementRate(EElement elements)
    {
        if (elements == EElement.WATER)
            return WaterRate;
        else if (elements == EElement.FIRE)
            return FireRate;
        else if (elements == EElement.TERRA)
            return TerraRate;
        return 1f;
    }

    public static EElement GetElement(GameObject target)
    {
        if (target.tag == "Player")
        {
            return target.GetComponent<Player>().curr.element;
        }
        else if (target.tag == "Monster")
        {
            return target.GetComponent<Monster>().element;
        }
        else if (target.tag == "Bullet")
        {
            return target.GetComponent<Bullet>().element;
        }
        return EElement.NONE;
    }
}
