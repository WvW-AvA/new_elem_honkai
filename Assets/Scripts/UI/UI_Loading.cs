using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class UI_Loading : UI_Base
{
    public Slider loadingBar;
    public float LoadingPercent { get { return loadingBar.value; } set { loadingBar.value = value; } }
    public float fadeTime;
    public Image ima;
    private float m_fadeTimer;
    protected override void OnUIDisable()
    {
        base.OnUIDisable();
        QuickCoroutineSystem.StartCoroutine(Effect(1));
    }
    protected override void OnUIEnable()
    {
        base.OnUIEnable();
        QuickCoroutineSystem.StartCoroutine(Effect(0));
    }
    IEnumerator Effect(int inOrOut)
    {
        Log.Info("Loading ui fade:{0}", inOrOut);
        m_fadeTimer = 0;
        while (fadeTime > m_fadeTimer)
        {
            m_fadeTimer += Time.deltaTime;
            var col = ima.color;
            if (inOrOut == 0)
                col.a = m_fadeTimer / fadeTime;
            else
                col.a = 1.0f - m_fadeTimer / fadeTime;
            ima.color = col;
            yield return new WaitForFixedUpdate();
        }
    }
}
