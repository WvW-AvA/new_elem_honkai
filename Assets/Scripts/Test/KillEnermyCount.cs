using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillEnermyCount : MonoBehaviour
{
    public string baseText;
    public Text text;
    public void Start()
    {
        text = GetComponent<Text>();
        baseText = text.text;
    }
    private void Update()
    {
        text.text = baseText + CountManager.Instance.Find("KillEnermyCount").count;
    }
}
