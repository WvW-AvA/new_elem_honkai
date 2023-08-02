using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using Sirenix.OdinInspector;
public class UI_PlayerDocker : UI_Icon
{
    public AudioClip ElementChangeUIClip;
    public List<RectTransform> CycleTransform;
    public List<RectTransform> ElementsTransform;
    public AnimationCurve cycleRotateCurve;
    public float cycleRotateTime;


    public Material grainMaterial;
    public AnimationCurve grainColorChangeCurve;
    public AnimationCurve grainColorIntensityChangeCurve;
    public float grainColorChangeTime;
    private float rotateTimer;
    private bool isAudioClipPlayed = true;
    private float m_targetRotation;
    private float m_currentRotation;
    private float targetRotation
    {
        get { return m_targetRotation; }
        set
        {
            m_targetRotation = angleClam(value);
        }
    }
    private float currentRotation
    {
        get { return m_currentRotation; }
        set
        {
            m_currentRotation = angleClam(value);
        }
    }


    private float grainTimer;
    private Color ori;
    private Color tar;

    public Dictionary<EElement, Color> grainColor;
    public UI_ElementAttach_Icon ElementAttach_Icon;

    private EElement m_UIElement = EElement.NONE;
    public EElement UIElement
    {
        set
        {
            grainTimer = 0;
            rotateTimer = 0;
            isAudioClipPlayed = false;
            targetRotation += calculateRotateAngle(value);
            ori = grainColor[m_UIElement];
            tar = grainColor[value];
            m_UIElement = value;
        }
    }

    public Image HPBar;
    public float HPBarChangeTime;
    private int m_lastHP;
    private int m_currentHP;
    private int m_maxHP;
    private float HPTimer;
    public int currentHP
    {
        set
        {
            m_lastHP = m_currentHP;
            m_currentHP = Mathf.Clamp(value, 0, m_maxHP);
            HPTimer = 0;
        }
    }

    public int maxHP { set { m_maxHP = value; } }


    private void Start()
    {
        ElementAttach_Icon.target = Player.instance;
    }
    private float angleClam(float value)
    {
        if (value > 180)
            return -180 + (value - 180);
        else if (value < -180)
            return 180 + (value + 180);
        return value;
    }
    protected override void OnUIUpdate()
    {
        base.OnUIUpdate();
        if (grainTimer < grainColorChangeTime)
        {
            grainTimer += Time.deltaTime;
            var p = grainTimer / grainColorChangeTime;
            var col = Color.Lerp(ori, tar, grainColorChangeCurve.Evaluate(p));
            var inten = grainColorIntensityChangeCurve.Evaluate(p);
            grainMaterial.SetColor("_Color", col);
            grainMaterial.SetFloat("_Intensity", inten);
        }
        if (HPTimer < HPBarChangeTime)
        {
            HPTimer += Time.deltaTime;
            var temp = Mathf.Lerp(m_lastHP, m_currentHP, HPTimer / HPBarChangeTime);
            HPBar.fillAmount = (float)temp / (float)m_maxHP;
            //HPGarin1.fillAmount = HPGarinValue((float)temp / (float)m_maxHP);
        }
        if (rotateTimer < cycleRotateTime)
        {
            rotateTimer += Time.deltaTime;
            float sam = cycleRotateCurve.Evaluate(rotateTimer / cycleRotateTime);
            if (rotateTimer / cycleRotateTime >= 0.8 && isAudioClipPlayed == false)
            {
                isAudioClipPlayed = true;
                AudioManager.PlayOneShot(ElementChangeUIClip, Player.instance.gameObject.transform.position);
            }
            foreach (var t in CycleTransform)
                t.localEulerAngles = new Vector3(0, 0, RotationLerp(currentRotation, targetRotation, sam));
            foreach (var t in ElementsTransform)
                t.localEulerAngles = new Vector3(0, 0, -RotationLerp(currentRotation, targetRotation, sam));
        }
        else
        {
            currentRotation = targetRotation;
        }


    }
    protected override void OnUIInit()
    {
        base.OnUIInit();
    }
    private float invalidZone = 1.0f / 6.0f;
    private float validZone;
    private float HPGarinValue(float value)
    {
        validZone = (1.0f / 3.0f) - invalidZone;
        if (value <= (1.0f / 3.0f))
        {
            return (float)(invalidZone / 2.0 + value * validZone / (1.0f / 3.0f));
        }
        else if (value <= (2.0f / 3.0f))
        {
            return (float)(invalidZone * 1.5 + value * validZone / (1.0f / 3.0f));
        }
        else if (value <= 1f)
        {
            return (float)(invalidZone * 2.5 + value * validZone / (1.0f / 3.0f));
        }
        else return 1f;
    }

    private float calculateRotateAngle(EElement target)
    {
        float r = 0;
        if (target == EElement.FIRE)
            r = 0 - currentRotation;
        else if (target == EElement.TERRA)
            r = 120 - currentRotation;
        else if (target == EElement.WATER)
            r = -120 - currentRotation;
        else if (target == EElement.NONE)
            return 60;
        if (Mathf.Abs(r) > 180)
            r = (-1) * Mathf.Sign(r) * (360 - Mathf.Abs(r));
        return r;

    }

    private float RotationLerp(float ori, float target, float duration)
    {
        float dis = target - ori;
        if (Mathf.Abs(dis) > 180)
            dis = (-1) * Mathf.Sign(dis) * (360 - Mathf.Abs(dis));
        return angleClam(Mathf.Lerp(ori, ori + dis, duration));
    }
}
