using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public partial class GameMode
{
    public class NormalMode : Base
    {
        public NormalMode() : base() { }
        public override void OnGameModeInit()
        {
            base.OnGameModeInit();
            modeType = EGameMode.NormalMode;
        }
        public override void OnGameModeEnter()
        {
            base.OnGameModeEnter();
            UIManager.playingUI.IsEnable = true;
            InputManager.SetPlayerBehaviousActive(true);
            Log.Info("{0}Player in pos {1}", LogColor.GameManager, Player.instance.transform.position);
        }

        public override void OnGameModeUpdate()
        {
            base.OnGameModeUpdate();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                InputManager.SetPlayerBehaviousActive(false);
                UIManager.playingUI.IsEnable = false;
                UIManager.pauseUI.IsEnable = true;
            }
        }

        public override void OnGameModeExit()
        {
            base.OnGameModeExit();
        }
    }
}
