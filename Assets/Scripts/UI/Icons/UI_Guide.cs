using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public struct GuideUIParam
{
    public Sprite background;
    public string title;
    public Sprite img;
    public string discription0;
    public string discription1;
    public EKey key;
    public string discription2;
}
public class UI_Guide : UI_Icon
{
    public Image background;
    public Text title;
    public Image img;
    public Text discription0;
    public Text discription1;
    public Image keyImg;
    public Text discription2;
    public float anyKeyContinueDelay;
    public GameObject anyKeyContinue;
    private bool isAnyKeyContinue;
    private float anyKeyContinueTimer;
    public void SetValue(GuideUIParam param)
    {
        isAnyKeyContinue = false;
        anyKeyContinueTimer = 0;
        title.text = param.title;
        if (param.background != null)
        {
            background.sprite = param.background;
            background.SetNativeSize();
        }
        img.sprite = param.img;
        img.SetNativeSize();
        discription0.text = param.discription0;
        discription1.text = param.discription1;
        keyImg.sprite = GameManager.globalParam.keySpriteDictionary[InputManager.getKeyMap(param.key)];
        keyImg.SetNativeSize();
        discription2.text = param.discription2;
    }

    protected override void OnUIEnable()
    {
        base.OnUIEnable();
        GameManager.ChangeGameMode(GameMode.EGameMode.DialogMode);
    }
    protected override void OnUIUpdate()
    {
        base.OnUIUpdate();
        if (isAnyKeyContinue == false)
        {
            anyKeyContinueTimer += Time.deltaTime;
            if (anyKeyContinueTimer > anyKeyContinueDelay)
            {
                isAnyKeyContinue = true;
                anyKeyContinue.SetActive(true);
            }
        }
        if (isAnyKeyContinue && InputManager.IsAnyKeyDown())
        {
            anyKeyContinue.SetActive(false);
            IsEnable = false;
        }
    }
    protected override void OnUIDisable()
    {
        base.OnUIDisable();
        GameManager.ChangeGameMode(GameMode.EGameMode.NormalMode);
    }
}
