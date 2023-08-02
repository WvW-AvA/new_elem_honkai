using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
public class UI_Base : SerializedMonoBehaviour
{
    private bool m_isSetEnable = false;
    protected bool m_isEnable = false;
    [ReadOnly]
    public bool IsEnable
    {
        get { return m_isEnable; }
        set
        {
            if (value == m_isSetEnable)
                return;
            m_isSetEnable = value;
            if (value)
            {
                OnUIEnable();
            }
            else
            {
                OnUIDisable();
            }
        }
    }
    protected virtual void OnUIInit()
    {
        IsEnable = true;
    }
    protected virtual void OnUIEnable()
    {
        m_isEnable = true;
    }
    protected virtual void OnUIUpdate() { }
    protected virtual void OnUIDisable()
    {
        m_isEnable = false;
    }

    private void Awake()
    {
        OnUIInit();
    }
    public virtual void Update()
    {
        if (IsEnable == false)
            return;
        OnUIUpdate();
    }


}
