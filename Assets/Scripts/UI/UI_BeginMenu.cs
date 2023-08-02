using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

class UI_BeginMenu : UI_Base
{
    public AudioClip beginMenuBGM;
    public Button gameStartButton;
    public Button gameContinueButton;
    public Button gameExitButton;

    public float delayTime;
    private ShaderControl tuanziShaderControler { get { return GameObject.FindObjectOfType<ShaderControl>(); } }
    protected override void OnUIInit()
    {
        base.OnUIInit();
        if (SaveSystem.CheckContinue() == false)
            gameContinueButton.gameObject.SetActive(false);
        gameStartButton.onClick.AddListener(() => { GameStart(); });
        gameContinueButton.onClick.AddListener(() => { GameContinue(); });
        gameExitButton.onClick.AddListener(() => { GameExit(); });
    }

    protected override void OnUIEnable()
    {
        base.OnUIEnable();
        AudioManager.BgmChange(beginMenuBGM);
    }
    public void GameStart()
    {
        QuickCoroutineSystem.StartCoroutine(GameStart_co());
    }
    IEnumerator GameStart_co()
    {
        tuanziShaderControler.StartGame();
        yield return new WaitForSeconds(delayTime);
        string file = SaveSystem.CreateNewSaveFile();
        UIManager.ReleaseUI(UIConst.BeginMenu);
        SaveSystem.Load(file);
    }
    public void GameContinue()
    {
        QuickCoroutineSystem.StartCoroutine(GameContinue_co());
    }
    IEnumerator GameContinue_co()
    {
        tuanziShaderControler.StartGame();
        yield return new WaitForSeconds(delayTime);
        UIManager.ReleaseUI(UIConst.BeginMenu);
        SaveSystem.ContinueGame();
    }

    public void GameExit()
    {
        GameManager.GameExit();
    }
}
