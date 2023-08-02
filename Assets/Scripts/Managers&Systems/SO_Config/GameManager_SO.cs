using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "GameManagerConfig", menuName = "ScriptableObjects/GameManager配置", order = 2)]
public class GameManager_SO : ScriptableObject
{
    public GlobalParamConfig_SO GlobalParamConfig;
    public GameMode_SO BeginMenuConfig;
    public GameMode_SO BattleModeConfig;
    public GameMode_SO DialogModeConfig;
    public GameMode_SO NormalModeConfig;
}

