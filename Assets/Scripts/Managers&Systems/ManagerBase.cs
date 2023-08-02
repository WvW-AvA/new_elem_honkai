using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase<T> : MonoBehaviour where T : ManagerBase<T>
{
    private static T m_instance;
    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                var go = GameObject.Find("GameManager");
                if (go == null)
                {
                    go = new GameObject("GameManager");
                    DontDestroyOnLoad(go);
                }
                m_instance = go.GetComponent<T>();
                if (m_instance == null)
                    m_instance = go.AddComponent<T>();
            }
            return m_instance;
        }
        set { if (m_instance == null) m_instance = value; }
    }

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
