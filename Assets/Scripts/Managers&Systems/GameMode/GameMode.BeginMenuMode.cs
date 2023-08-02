using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
public partial class GameMode
{
    public class BeginMenuMode : Base
    {
        public static bool isReleaseAll = false;
        public string BeginMenuSceneName;
        public BeginMenuMode() : base() { }
        public override void OnGameModeInit()
        {
            base.OnGameModeInit();
            modeType = EGameMode.BeginMenuMode;
        }

        public override void OnGameModeEnter()
        {
            base.OnGameModeEnter();
            if (isReleaseAll)
            {
                GameManager.LoadSceneAsync(BeginMenuSceneName, () =>
                {
                    UIManager.ReleaseAllUI();
                    ResourceManager.Release(CameraManager.cameraObject);
                    ResourceManager.Release(Player.instance.gameObject);
                    UIManager.CreateUI(UIConst.BeginMenu, 1);
                });
            }
            else
            {
                GameManager.LoadSceneAsync(BeginMenuSceneName);
                UIManager.CreateUI(UIConst.BeginMenu, 1);
            }
            isReleaseAll = false;
        }
        public override void OnGameModeExit()
        {
            base.OnGameModeExit();
        }
    }
}
