using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class GameMode
{
    public class BattleMode : Base
    {
        private Boss m_targetBoss;
        public Boss targetBoss
        {
            get { return m_targetBoss; }
            set
            {
                m_targetBoss = value;
                m_targetBoss.currentHp = m_targetBoss.maxHp;
                UIManager.bossDocker.SetTargetBoss(value);
            }
        }
        public BattleMode() : base() { }
        public override void OnGameModeInit()
        {
            base.OnGameModeInit();
            modeType = EGameMode.BattleMode;
        }

        public override void OnGameModeEnter()
        {
            base.OnGameModeEnter();
            InputManager.SetPlayerBehaviousActive(true);
            UIManager.playingUI.IsEnable = true;
            UIManager.bossDocker.IsEnable = true;
        }

        public override void OnGameModeExit()
        {
            base.OnGameModeExit();
            UIManager.bossDocker.IsEnable = false;
            targetBoss.StatusReset();
        }
    }
}
