using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class GameMode
{
    public class DialogMode : Base
    {
        public DialogMode() : base() { }
        public override void OnGameModeInit()
        {
            base.OnGameModeInit();
            modeType = EGameMode.DialogMode;
        }
        public override void OnGameModeEnter()
        {
            base.OnGameModeEnter();
            UIManager.playingUI.IsEnable = false;
            InputManager.SetPlayerBehaviousActive(false);
        }
    }
}