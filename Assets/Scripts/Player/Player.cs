using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Animancer;
public class Player : Pawn, Iinjured
{
    /// <summary>
    /// 单例模式
    /// </summary>
    private static Player m_instance;

    public static Player instance
    {
        get
        {
            if (m_instance != null)
                return m_instance;
            else
            {
                GameObject p;
                p = GameObject.FindGameObjectWithTag("Player");
                if (p == null)
                {
                    p = ResourceManager.GetInstance(GameConst.PlayerPrefab, PlayerStaticParamater.playerBornPos);
                }
                DontDestroyOnLoad(p);
                CameraManager.BindCameraFollow(p);
                m_instance = p.GetComponent<Player>();
                return m_instance;
            }
        }
    }

    /// <summary>
    /// 不同元素的参数配置字典
    /// </summary>
    [ShowInInspector]
    [NonSerialized, OdinSerialize]
    public Dictionary<EElement, PlayerParameters> parameterDict = new Dictionary<EElement, PlayerParameters>();
    /// <summary>
    /// 当前元素的参数
    /// </summary>
    [ShowInInspector]
    [ReadOnly]
    private PlayerParameters m_parameter;
    public static PlayerParameters parameter
    {
        get
        {
            if (instance.m_parameter == null)
                instance.m_parameter = instance.parameterDict[instance.curr.element];
            return instance.m_parameter;
        }
    }

    /// <summary>
    /// 玩家状态机组件
    /// </summary>
    [HideInInspector]
    public PlayerFSM playerFSM;
    [HideInInspector]
    public Rigidbody2D rigidbody;
    [HideInInspector]
    public AnimancerComponent animancer;
    [HideInInspector]
    public BuffManager buffManager;

    public PlayerRuntimeParameter curr;

    [NonSerialized, OdinSerialize]
    public List<BuffBase> OnInjureEffect;
    private void Awake()
    {
        PlayerComponentInit();
        curr.Init();
        InputManager.Keys[EKey._1].shortPressEvent.AddListener(() => { ChangeElement(EElement.NONE); });
        InputManager.Keys[EKey._2].shortPressEvent.AddListener(() => { ChangeElement(EElement.FIRE); });
        InputManager.Keys[EKey._3].shortPressEvent.AddListener(() => { ChangeElement(EElement.WATER); });
        InputManager.Keys[EKey._4].shortPressEvent.AddListener(() => { ChangeElement(EElement.TERRA); });
    }

    private void Start()
    {
        ChangeElement(EElement.NONE);
    }

    private void PlayerComponentInit()
    {
        playerFSM = GetComponent<PlayerFSM>();
        if (playerFSM == null) playerFSM = gameObject.AddComponent<PlayerFSM>();
        rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null) rigidbody = gameObject.AddComponent<Rigidbody2D>();
        animancer = GetComponent<AnimancerComponent>();
        if (animancer == null) animancer = gameObject.AddComponent<AnimancerComponent>();
        buffManager = GetComponent<BuffManager>();
        if (buffManager == null) buffManager = gameObject.AddComponent<BuffManager>();
    }

    public void ChangeElement(EElement element)
    {
        if (element == curr.element && string.IsNullOrEmpty(playerFSM.currentStateName) == false)
            return;
        if (curr.unlockedElement.Contains(element) == false)
            return;

        float HPPercent = (float)(curr.hp) / parameter.maxHP;
        curr.element = element;
        m_parameter = parameterDict[curr.element];
        curr.hp = Mathf.Max(1, (int)(HPPercent * parameter.maxHP));
        rigidbody.gravityScale = 9.8f * parameter.gravityRate;
        playerFSM.ConfigWithScriptableObject(parameter.FSMConfig);
    }

    private void FixedUpdate()
    {
    }

    private void Update()
    {

    }

    public void GetHurt(DamageToken damage)
    {
        if (curr.isInvincible)
            return;
        BuffManager.AddBuffList(OnInjureEffect, gameObject, gameObject);
        AttachElement(damage.hitElement, damage.Giver);
        var damageValue = damage.Calculate();
        UIManager.CreateFloatStringIcon(damageValue.ToString(), transform.position, Element.ElementFloatStringColor[damage.hitElement]);
        curr.hp -= damageValue;
        Log.Info(LogColor.Player + "Get hurt {0}", damage.Calculate());
    }
    public void GetHeal(int value)
    {
        if (curr.isInvincible)
            return;
        Log.Info(LogColor.Player + "Get Heal {0}", value);
        UIManager.CreateFloatStringIcon("+" + value.ToString(), transform.position, Element.ElementFloatStringColor[EElement.WATER]);
        curr.hp += value;
    }
    public void PlayerDie()
    {
        GameManager.ChangeGameMode(GameMode.EGameMode.NormalMode);
        gameObject.SetActive(false);
        SaveSystem.CurrentFile.playerData.curr_HP = parameter.maxHP;
        SaveSystem.Load(SaveSystem.CurrentFile.fileName);
        BuffManager.SetBuffAcceptable(true, gameObject);
        curr.isInvincible = false;
        Log.Info(LogColor.Player + "Died");
    }

    public void UnlockElement(EElement element)
    {
        if (curr.unlockedElement.Contains(element) == false)
            curr.unlockedElement.Add(element);
    }

    #region ElementReactionOverwrite
    protected override void ElementReaction_BURST_Invoke(GameObject invoker)
    {
        base.ElementReaction_BURST_Invoke(invoker);
        BuffManager.AddBuffList(Element.PlayerElementReactionBuffs[EElementReaction.BURST], invoker, gameObject);
    }
    protected override void ElementReaction_HEAL_Invoke(GameObject invoker)
    {
        base.ElementReaction_HEAL_Invoke(invoker);
        BuffManager.AddBuffList(Element.PlayerElementReactionBuffs[EElementReaction.HEAL], invoker, gameObject);
    }
    protected override void ElementReaction_LAVA_Invoke(GameObject invoker)
    {
        base.ElementReaction_LAVA_Invoke(invoker);
        BuffManager.AddBuffList(Element.PlayerElementReactionBuffs[EElementReaction.LAVA], invoker, gameObject);
    }
    protected override void ElementReaction_MUDDY_Invoke(GameObject invoker)
    {
        base.ElementReaction_MUDDY_Invoke(invoker);
        BuffManager.AddBuffList(Element.PlayerElementReactionBuffs[EElementReaction.MUDDY], invoker, gameObject);
    }
    protected override void ElementReaction_NONE_Invoke(GameObject invoker)
    {
        base.ElementReaction_NONE_Invoke(invoker);
        BuffManager.AddBuffList(Element.PlayerElementReactionBuffs[EElementReaction.NONE], invoker, gameObject);
    }
    protected override void ElementReaction_PETRIFIED_Invoke(GameObject invoker)
    {
        base.ElementReaction_PETRIFIED_Invoke(invoker);
        BuffManager.AddBuffList(Element.PlayerElementReactionBuffs[EElementReaction.PETRIFIED], invoker, gameObject);
    }
    protected override void ElementReaction_STEAM_Invoke(GameObject invoker)
    {
        base.ElementReaction_STEAM_Invoke(invoker);
        BuffManager.AddBuffList(Element.PlayerElementReactionBuffs[EElementReaction.STEAM], invoker, gameObject);
    }
    #endregion
}
[Serializable]
public struct PlayerRuntimeParameter
{
    private UI_Playing ui;
    [SerializeField]
    private int m_hp;
    [SerializeField]
    private int m_fireEnergy;
    [SerializeField]
    private int m_waterEnergy;
    [SerializeField]
    private int m_terrainEnergy;

    public bool isFireEnchantment;
    public bool isInvincible;
    public List<EElement> unlockedElement;

    [SerializeField]
    private EElement m_element;
    public void Init()
    {
        unlockedElement = new List<EElement>();
        unlockedElement.Add(EElement.NONE);
        ui = UIManager.playingUI;
        ui.Fire_Energy_Icon.maxValue = PlayerStaticParamater.maxFireEnergy;
        ui.Water_Energy_Icon.maxValue = PlayerStaticParamater.maxWaterEnergy;
        ui.Terrain_Energy_Icon.maxValue = PlayerStaticParamater.maxTerrainEnergy;
        isFireEnchantment = false;
        isInvincible = false;
        hp = m_hp;
    }
    public int hp
    {
        get { return m_hp; }
        set
        {
            if (value < 0)
            {
                m_hp = 0;
            }
            if (value > Player.parameter.maxHP)
                m_hp = Player.parameter.maxHP;
            else
                m_hp = value;
            ui.playerDocker.maxHP = Player.parameter.maxHP;
            ui.playerDocker.currentHP = m_hp;
        }
    }
    public int fireEnergy
    {
        get { return m_fireEnergy; }
        set
        {
            m_fireEnergy = Mathf.Clamp(value, 0, PlayerStaticParamater.maxFireEnergy);
            ui.Fire_Energy_Icon.currentValue = m_fireEnergy;
        }
    }
    public int waterEnergy
    {
        get { return m_waterEnergy; }
        set
        {
            m_waterEnergy = Mathf.Clamp(value, 0, PlayerStaticParamater.maxWaterEnergy);
            ui.Water_Energy_Icon.currentValue = m_waterEnergy;
        }
    }

    public int terrainEnergy
    {
        get { return m_terrainEnergy; }
        set
        {
            m_terrainEnergy = Mathf.Clamp(value, 0, PlayerStaticParamater.maxTerrainEnergy);
            ui.Terrain_Energy_Icon.currentValue = m_terrainEnergy;
        }
    }

    public EElement element
    {
        get { return m_element; }
        set
        {
            m_element = value;
            ui.playerDocker.UIElement = value;
        }
    }
}
