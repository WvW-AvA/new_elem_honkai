using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine.UI;
using UnityEngine;
public class UI_ESkill_Icon : UI_Icon
{
    // View fields
    public Text CDText;
    [SerializeField]
    private Image CDMaskImage;
    public Image CDImage;

    // Logic fields
    private float m_CDTimer;
    [HideInInspector]
    public float CD;
    public float CDTimer
    {
        get { return m_CDTimer; }
        set
        {
            if (value > 0)
            {
                m_CDTimer = value;
                CDText.text = ((int)m_CDTimer).ToString();
                CDMaskUpdate(m_CDTimer / CD);
            }
            else
            {
                m_CDTimer = 0;
                CDText.text = "";
            }
        }
    }

    private void CDMaskUpdate(float rate)
    {
        CDMaskImage.fillAmount = rate;
    }
}

