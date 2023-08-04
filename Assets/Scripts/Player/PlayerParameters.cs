using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Animancer;
[Serializable]
[CreateAssetMenu(fileName = "PlayerParameters", menuName = "ScriptableObjects/PlayerParameter", order = 1)]
public class PlayerParameters : SerializedScriptableObject
{
    public EElement element;
    [Header("Base Parameters")]
    public float moveSpeed;
    [Range(1, 0x3f3f3f3f)]
    public int maxHP;
    public int baseDamage;
    public float jumpForce;
    public float fullJumpPressTime = 0.2f;
    public float gravityRate = 0.1f;
    public bool Q_Skill_IsEnable = false;
    public bool E_Skill_IsEnable = false;

    public Player_FSM_SO_Config FSMConfig;
    //....

}
