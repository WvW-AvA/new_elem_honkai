using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Serialization;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Player_FSM_SO_Config", menuName = "ScriptableObjects/PlayerFSM配置")]
public class Player_FSM_SO_Config : SerializedScriptableObject
{

    public string defaultStateName;
    public EElement element;
    [NonSerialized, OdinSerialize]
    public List<Player_State_SO_Config> stateConfigs = new List<Player_State_SO_Config>();
    [NonSerialized, OdinSerialize]
    public Player_State_SO_Config anyStateConfig;
}
