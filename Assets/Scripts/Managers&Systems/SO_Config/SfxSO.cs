using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEditor;
using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Animancer;
[CreateAssetMenu(fileName = "new Sfx_SO", menuName = "ScriptableObjects/Sfx≈‰÷√")]
public class SfxSO : SerializedScriptableObject
{
    [ReadOnly]
    public string sfxName;
    public bool isUseAnimacer = true;
    [HideIf("isUseAnimacer")]
    public GameObject effectPrefab;
    [ShowIf("isUseAnimacer")]
    public ClipTransition animationClip;
    [ShowIf("isUseAnimacer")]
    public Sprite sprite;

    [ShowIf("isUseAnimacer")]
    public int spriteLayer;
    public bool isAutoEffectDuration = true;
    [HideIf("isAutoEffectDuration")]
    public float effectDuration = 0.5f;

    [ShowIf("isUseAnimacer")]
    public bool isCauseDamage = false;
    [ShowIf("isCauseDamage")]
    public float delayTime = 0;
    [ShowIf("isCauseDamage")]
    public float damageBoxKeepTime = 0.2f;
    [ShowIf("isCauseDamage")]
    public LayerMask targetLayer;
    [ShowIf("isCauseDamage"), NonSerialized, OdinSerialize]
    public List<BuffBase> buffList;
    [ShowIf("isCauseDamage")]
    public Vector2 triggerBoxSize;
    [ShowIf("isCauseDamage")]
    public Vector2 triggerBoxOffset;


    [ShowIf("isUseAnimacer")]
    public bool isAddLight2D = false;
    [ShowIf("isAddLight2D"), NonSerialized, OdinSerialize]
    public GameObject light2DPrefab;
    [ShowIf("isAddLight2D"), NonSerialized, OdinSerialize]
    public Light2DEffect lightEffect;

    public bool isAddSoundEffect = false;
    [ShowIf("isAddSoundEffect"), NonSerialized, OdinSerialize]
    public AudioClip soundEffectClip;

    public GameObject GetInstance(GameObject owner, Vector3 pos, float x_scale, GameObject parent)
    {
        GameObject temp;

        if (isUseAnimacer == false)
        {
            temp = ResourceManager.GetInstance(effectPrefab, pos);
            temp.name = sfxName;
            temp.transform.SetParent(parent.transform);
            temp.transform.localPosition = pos;
            var s = temp.transform.localScale;
            s.x *= Mathf.Sign(x_scale);
            temp.transform.localScale = s;
            if (isAutoEffectDuration)
                effectDuration = 999;
        }
        else
        {
            temp = new GameObject(sfxName);
            temp.transform.SetParent(parent.transform);
            var s = Vector3.one;
            s.x *= Mathf.Sign(x_scale);
            temp.transform.localScale = s;

            temp.transform.localPosition = pos;
            var sr = temp.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;
            var P_sr = temp.transform.parent.GetComponent<SpriteRenderer>();
            if (P_sr)
                sr.sortingLayerID = P_sr.sortingLayerID;
            temp.AddComponent<Animator>();
            var an = temp.AddComponent<AnimancerComponent>();
            an.Play(animationClip);
            if (isAutoEffectDuration)
            {
                effectDuration = animationClip.Transition.State.Length;
            }
            temp.GetComponent<SpriteRenderer>().sortingOrder = spriteLayer;
            if (isCauseDamage)
            {
                if (delayTime <= 0)
                {
                    CreateDamageCollider(temp, owner);
                }
                else
                {
                    QuickCoroutineSystem.StartCoroutine(CreateDamageCollider_co(temp, owner));
                }
            }
            if (isAddLight2D)
            {
                var light = ResourceManager.GetInstance(light2DPrefab);
                var le = new Light2DEffect(lightEffect);
                le.effectTime = effectDuration;
                light.transform.SetParent(temp.transform);
                light.transform.localPosition = Vector3.zero;
                light.GetComponent<Light2DController>().AddLightEffect(le);
            }
        }
        if (isAddSoundEffect)
        {
            AudioManager.PlayOneShot(soundEffectClip, temp.transform.position);
        }
        return temp;
    }

    IEnumerator CreateDamageCollider_co(GameObject target, GameObject owner)
    {
        yield return new WaitForSeconds(delayTime);
        CreateDamageCollider(target, owner);
    }
    void CreateDamageCollider(GameObject target, GameObject owner)
    {
        var col = target.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        if (triggerBoxSize != Vector2.zero)
            col.size = triggerBoxSize;
        col.offset = triggerBoxOffset;
        var b = target.GetComponent<Damager>();
        if (b == null)
            b = target.AddComponent<Damager>();
        b.buffList = buffList;
        b.triggerLayer = targetLayer;
        b.owner = owner;
        QuickCoroutineSystem.StartCoroutine(DamageBoxDisable(damageBoxKeepTime, col));
    }

    IEnumerator DamageBoxDisable(float keepTime, Collider2D collider)
    {
        yield return new WaitForSeconds(keepTime);
        if (collider != null)
            collider.enabled = false;
    }

    public void UpdateBuffsName()
    {
        if (buffList == null)
            return;
        for (int i = 0; i < buffList.Count; i++)
        {
            buffList[i].buffName = sfxName + "_" + buffList[i].GetType().Name;
        }
    }
    private void OnValidate()
    {
        sfxName = name;
        UpdateBuffsName();
    }
}


