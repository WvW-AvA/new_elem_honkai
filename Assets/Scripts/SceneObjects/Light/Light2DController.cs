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
[RequireComponent(typeof(Light2D))]
public class Light2DController : Interactible
{
    private Light2D light;
    private Light2DEffect effect;
    private void Awake()
    {
        light = gameObject.GetComponent<Light2D>();
    }

    private void Update()
    {
        if (effect)
            effect.LightEffectUpdate(light);
    }

    public void AddLightEffect(Light2DEffect lightEffect)
    {
        effect = lightEffect;
        effect.LightEffectEnable(light);
    }
}
