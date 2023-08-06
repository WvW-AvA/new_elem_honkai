using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalParamConfig", menuName = "ScriptableObjects/GlobalParam配置", order = 2)]
public class GlobalParamConfig_SO : SerializedScriptableObject
{
    [FoldoutGroup("ElementGlobalConfig")]
    public Dictionary<EElement, Color> ElementFloatStringColor;
    [FoldoutGroup("ElementGlobalConfig")]
    public Dictionary<EElement, int> ElementMaxAttachLayer;
    [FoldoutGroup("ElementGlobalConfig")]
    public Dictionary<EElement, Sprite> ElementIcons;
    [FoldoutGroup("ElementGlobalConfig")]
    public Dictionary<EElementReaction, Color> ElementReactionFloatStringColor;
    [FoldoutGroup("ElementGlobalConfig")]
    public Dictionary<EElementReaction, List<BuffBase>> PlayerElementReactionBuffs;
    [FoldoutGroup("ElementGlobalConfig")]
    public Dictionary<EElementReaction, List<BuffBase>> MonsterElementReactionBuffs;
    [Range(0, 1)]
    public float CameraLookForwardSpeed = 0.3f;
    public float CameraLookForwardMaxDistance = 5f;

    public Dictionary<KeyCode, Sprite> keySpriteDictionary;

    public Dictionary<ECommand, float> commandKeepTime;
    public static void GlobalParamConfigInit(GlobalParamConfig_SO config_SO)
    {
        Element.ElementFloatStringColor = config_SO.ElementFloatStringColor;
        Element.ElementIcons = config_SO.ElementIcons;
        Element.ElementMaxAttachLayer = config_SO.ElementMaxAttachLayer;
        Element.ElementReactionFloatStringColor = config_SO.ElementReactionFloatStringColor;
        Element.MonsterElementReactionBuffs = config_SO.MonsterElementReactionBuffs;
        Element.PlayerElementReactionBuffs = config_SO.PlayerElementReactionBuffs;
    }
}
