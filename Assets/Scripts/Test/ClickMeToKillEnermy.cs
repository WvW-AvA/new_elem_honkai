using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMeToKillEnermy : MonoBehaviour
{
    public int num;
    public void Start()
    {
        Button bt = GetComponent<Button>();
        bt.onClick.AddListener(Click);
    }
    public void Click()
    {
        if (num == 1) CountManager.Instance.AddCount("KillEnermyCount");
        else CountManager.Instance.AddCount("KillEnermyCount",num);
    }
}
