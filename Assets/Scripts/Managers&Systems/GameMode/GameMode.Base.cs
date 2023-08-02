using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEditor;
public partial class GameMode
{
    [Serializable]
    public enum EGameMode
    {
        Base,
        BeginMenuMode,
        NormalMode,
        DialogMode,
        BattleMode
    }
    [Serializable]
    public class Base
    {
        public Base() { OnGameModeInit(); }
        [ReadOnly]
        public EGameMode modeType;
        public bool isEnable = true;
        public virtual void OnGameModeInit() { }
        public virtual void OnGameModeEnter() { if (isEnable == false) return; }
        public virtual void OnGameModeUpdate() { if (isEnable == false) return; }
        public virtual void OnGameModeExit() { if (isEnable == false) return; }
    }

}
