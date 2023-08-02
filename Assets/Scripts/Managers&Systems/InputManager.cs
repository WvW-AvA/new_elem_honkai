using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
public enum EKey
{
    W, A, S, D, E, Q, SPACE, _1, _2, _3, _4, J, K, F, ESC, TAB
}
public enum EAxis
{
    Vertical, Horizontal
}
public class Key
{
    public bool isEnable = true;
    //key base fields
    public bool isKeyDown
    {
        get
        {
            if (isEnable == false) return false;
            if (Input.GetKeyDown(keyCode))
            {
                pressDuration = 0f;
                return true;
            }
            if (m_isMockKeyDown)
            {
                pressDuration = 0f;
                m_isMockKeyDown = false;
                m_isMockPress = true;
                return true;
            }
            return false;
        }
    }
    public bool isKeyPressing
    {
        get
        {
            if (isEnable == false) return false;
            if (Input.GetKey(keyCode) || m_isMockPress)
            {
                pressDuration += Time.deltaTime;
                return true;
            }
            return false;
        }
    }
    public bool isKeyUp
    {
        get
        {
            if (isEnable == false) return false;

            if (Input.GetKeyUp(keyCode))
            {
                if (pressDuration > longPressTimeThreshold)
                    longPressEvent.Invoke();
                else
                    shortPressEvent.Invoke();
                return true;
            }

            if (m_isMockKeyUp)
            {
                m_isMockKeyUp = false;
                if (pressDuration > longPressTimeThreshold)
                    longPressEvent.Invoke();
                else
                    shortPressEvent.Invoke();
                return true;
            }

            return false;
        }
    }

    private float m_pressDuration = 0f;
    public float pressDuration
    {
        get
        {
            if (isEnable == false) return 0f;
            return m_pressDuration;
        }
        set
        {
            m_pressDuration = value;
        }
    }
    public KeyCode keyCode;

    //key event trigger fields
    public float longPressTimeThreshold = 0.2f;
    public UnityEvent shortPressEvent = new UnityEvent();
    public UnityEvent longPressEvent = new UnityEvent();
    public Key(KeyCode keyCode) { this.keyCode = keyCode; }

    private bool m_isMockPress = false;
    private bool m_isMockKeyDown = false;
    private bool m_isMockKeyUp = false;
    private float mockTime = 0;
    private float mockTimer = 0;
    public bool Update()
    {
        if (m_isMockPress)
        {
            if (mockTimer < mockTime)
                mockTimer += Time.deltaTime;
            else
            {
                m_isMockPress = false;
                m_isMockKeyUp = true;
            }
        }
        //Log.UILog(keyCode.ToString(), "   isDown:{0}   isPressing:{1}\nisUp:{2}  duration:{3}", isKeyDown, isKeyPressing, isKeyUp, pressDuration); ;
        return isKeyDown || isKeyPressing || isKeyUp;
    }

    public void Mock(float pressTime)
    {
        m_isMockKeyDown = true;
        mockTime = pressTime;
        mockTimer = 0;
    }
}
public class Axis
{
    public bool isEnable = true;
    public float value
    {
        get
        {
            if (isEnable == false)
                return 0f;
            if (m_isMock)
                return m_mockValue;
            return Input.GetAxis(axis.ToString());
        }
    }
    public EAxis axis;

    private bool m_isMock = false;
    private float m_mockValue = 0;
    private float m_mockTime = 0;
    private float m_mockTimer = 0;

    public float Update()
    {
        if (m_isMock)
        {
            if (m_mockTimer < m_mockTime)
                m_mockTimer += Time.deltaTime;
            else
            {
                m_isMock = false;
            }
        }
        return value;
    }

    public void Mock(float mockValue, float mockTime)
    {
        m_isMock = true;
        m_mockValue = mockValue;
        m_mockTime = mockTime;
    }

    public Axis(EAxis axis) { this.axis = axis; }

}

public class InputManager : ManagerBase<InputManager>
{
    public bool isEnable = true;

    [ShowInInspector, ReadOnly]
    private Dictionary<EKey, Key> m_keys = new Dictionary<EKey, Key>();
    public static Dictionary<EKey, Key> Keys { get { return Instance.m_keys; } }

    [ShowInInspector, ReadOnly]
    private Dictionary<EAxis, Axis> m_axis = new Dictionary<EAxis, Axis>();
    public static Dictionary<EAxis, Axis> Axises { get { return Instance.m_axis; } }

    private static Dictionary<EKey, KeyCode> keys_keyCode_map = new Dictionary<EKey, KeyCode>();
    private static List<Key> keys_map = new List<Key>();
    override protected void Awake()
    {
        base.Awake();
        //´Ë´¦ÐÞ¸ÄÓ³Éä
        keys_keyCode_map.Add(EKey.A, KeyCode.A);
        keys_keyCode_map.Add(EKey.W, KeyCode.W);
        keys_keyCode_map.Add(EKey.S, KeyCode.S);
        keys_keyCode_map.Add(EKey.D, KeyCode.D);
        keys_keyCode_map.Add(EKey.Q, KeyCode.Q);
        keys_keyCode_map.Add(EKey.E, KeyCode.E);
        keys_keyCode_map.Add(EKey.J, KeyCode.J);
        keys_keyCode_map.Add(EKey.K, KeyCode.K);
        keys_keyCode_map.Add(EKey.F, KeyCode.F);
        keys_keyCode_map.Add(EKey._1, KeyCode.Alpha1);
        keys_keyCode_map.Add(EKey._2, KeyCode.Alpha2);
        keys_keyCode_map.Add(EKey._3, KeyCode.Alpha3);
        keys_keyCode_map.Add(EKey._4, KeyCode.Alpha4);
        keys_keyCode_map.Add(EKey.SPACE, KeyCode.Space);
        keys_keyCode_map.Add(EKey.ESC, KeyCode.Escape);
        keys_keyCode_map.Add(EKey.TAB, KeyCode.Tab);
        foreach (var key in keys_keyCode_map.Keys)
        {
            keys_map.Add(new Key(keys_keyCode_map[key]));
            Keys.Add(key, keys_map[keys_map.Count - 1]);
        }
        Axises.Add(EAxis.Horizontal, new Axis(EAxis.Horizontal));
        Axises.Add(EAxis.Vertical, new Axis(EAxis.Vertical));

    }

    void Start()
    {

    }
    private void Update()
    {
        foreach (var k in keys_map)
            k.Update();
        Axises[EAxis.Horizontal].Update();
        Axises[EAxis.Vertical].Update();
    }

    public static void MockKey(EKey key, float time)
    {
        Keys[key].Mock(time);
    }
    public static void MockAxis(EAxis axis, float mockValue, float time)
    {
        Axises[axis].Mock(mockValue, time);
    }

    public static void SetPlayerBehaviousActive(bool value)
    {
        Keys[EKey.W].isEnable = value;
        Keys[EKey.A].isEnable = value;
        Keys[EKey.S].isEnable = value;
        Keys[EKey.D].isEnable = value;
        Keys[EKey._1].isEnable = value;
        Keys[EKey._2].isEnable = value;
        Keys[EKey._3].isEnable = value;
        Keys[EKey._4].isEnable = value;
        Keys[EKey.J].isEnable = value;
        Keys[EKey.K].isEnable = value;
        Keys[EKey.F].isEnable = value;
        Keys[EKey.E].isEnable = value;
        Keys[EKey.Q].isEnable = value;
        Keys[EKey.SPACE].isEnable = value;
    }
    public static bool IsAnyKeyDown()
    {
        return Input.anyKeyDown;
    }
    public static KeyCode getKeyMap(EKey key)
    {
        return keys_keyCode_map[key];
    }
}
