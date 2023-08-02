using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[Serializable]

[CreateAssetMenu(fileName = "Base", menuName = "ScriptableObjects/GameMode配置", order = 2)]
public class GameMode_SO : ScriptableObject
{
    [SerializeReference]
    public GameMode.EGameMode gameMode = GameMode.EGameMode.Base;
    [SerializeReference]
    public GameMode.Base modeConfig;
}

