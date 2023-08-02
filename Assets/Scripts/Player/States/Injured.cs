using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Animancer;
[Serializable]
public class Injured : PlayerFSMBaseState
{
    public float toughness;
    private float m_currentToughness;
    public float CurrentToughness
    {
        get { return m_currentToughness; }
        set
        {
            if (value > toughness)
            {
                m_currentToughness = 0;
                Hurt();
            }
            else
            {
                m_currentToughness = value;
            }
        }
    }
    private void Hurt()
    {

    }
}
