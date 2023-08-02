using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
public class UI_QSkill_Icon : UI_Icon
{
    // View fields
    public Text CDText;
    [SerializeField]
    private Image CDMaskImage;
    [SerializeField]
    private Image PowerMaskImage;

    public Image CDImage;
    public Image PowerImage;
    // Logic fields
    private float m_CDTimer;
    private float m_currentPower;
    [HideInInspector]
    public float CD;
    [HideInInspector]
    public float PowerConsume;
    public float CDTimer
    {
        get { return m_CDTimer; }
        set
        {
            if (value > 0)
            {
                m_CDTimer = value;
                CDText.text = ((int)(m_CDTimer)).ToString();
                CDMaskUpdate(m_CDTimer / CD);
            }
            else
            {
                m_CDTimer = 0;
                CDText.text = "";
            }
        }
    }

    public float currentPower
    {
        get { return currentPower; }
        set
        {
            m_currentPower = value < PowerConsume ? value : PowerConsume;
            PowerMaskUpdate(m_currentPower / PowerConsume);
        }
    }

    private void PowerMaskUpdate(float rate)
    {

    }
    private void CDMaskUpdate(float rate)
    {
        CDMaskImage.fillAmount = rate;
    }
}
