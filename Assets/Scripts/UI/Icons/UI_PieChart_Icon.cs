using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class UI_PieChart_Icon : UI_Icon
{
    public Image maskImage;
    public int maxValue;
    [ReadOnly]
    private int m_currentValue;
    public int currentValue
    {
        get { return m_currentValue; }
        set
        {
            if (value > maxValue)
                m_currentValue = maxValue;
            else
                m_currentValue = value;
            maskImage.fillAmount = (float)m_currentValue / (float)maxValue;
        }
    }


}
