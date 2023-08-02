using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;


[CreateAssetMenu(fileName = "newLightEffect", menuName = "ScriptableObjects/LightEffect配置")]
public class Light2DEffect : SerializedScriptableObject
{
    public float effectTime;
    public bool isLoop;
    private float duration = 0;
    private bool isTimerRuning = false;

    public bool isIntensityChange = true;
    [ShowIf("isIntensityChange")]
    public float maxIntensity;
    [ShowIf("isIntensityChange")]
    public AnimationCurve intensityCurve;

    public bool isRadiusChange = false;
    [ShowIf("isRadiusChange")]
    public float maxOutRadius;
    [ShowIf("isRadiusChange")]
    public AnimationCurve OutRadiusCurve;
    [ShowIf("isRadiusChange")]
    public float maxInnerRadius;
    [ShowIf("isRadiusChange")]
    public AnimationCurve InnerRadiusCurve;

    public bool isColorChange = false;
    [ShowIf("isColorChange")]
    public Color beginColor;
    [ShowIf("isColorChange")]
    public Color endColor;
    [ShowIf("isColorChange")]
    public AnimationCurve colorBlendCurve;

    public Light2DEffect(Light2DEffect effect)
    {
        effectTime = effect.effectTime;
        isLoop = effect.isLoop;
        isIntensityChange = effect.isIntensityChange;
        maxIntensity = effect.maxIntensity;
        intensityCurve = effect.intensityCurve;
        isColorChange = effect.isColorChange;
        beginColor = effect.beginColor;
        endColor = effect.endColor;
        colorBlendCurve = effect.colorBlendCurve;
        isRadiusChange = effect.isRadiusChange;
        maxOutRadius = effect.maxOutRadius;
        OutRadiusCurve = effect.OutRadiusCurve;
        maxInnerRadius = effect.maxInnerRadius;
        InnerRadiusCurve = effect.InnerRadiusCurve;
    }

    public virtual void LightEffectEnable(Light2D light)
    {
        if (isColorChange)
            light.color = beginColor;
        duration = 0;
        isTimerRuning = true;
    }

    public virtual void LightEffectUpdate(Light2D light)
    {
        if (isTimerRuning == false)
            return;
        duration += Time.deltaTime;
        if (duration > effectTime)
            if (isLoop)
                duration = 0;
            else
                isTimerRuning = false;
        if (isIntensityChange)
            light.intensity = maxIntensity * intensityCurve.Evaluate(duration / effectTime);
        if (isColorChange)
            light.color = Color.Lerp(beginColor, endColor, colorBlendCurve.Evaluate(duration / effectTime));
        if (isRadiusChange)
        {
            light.pointLightOuterRadius = maxOutRadius * OutRadiusCurve.Evaluate(duration / effectTime);
            light.pointLightInnerRadius = maxInnerRadius * InnerRadiusCurve.Evaluate(duration / effectTime);
        }
    }
}
