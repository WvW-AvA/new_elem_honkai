using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : ManagerBase<SfxManager>
{
    private GameObject m_sfxFather;
    public GameObject sfxFather
    {
        get
        {
            if (m_sfxFather == null)
            {
                m_sfxFather = new GameObject("Sfx");
                var r = m_sfxFather.AddComponent<SpriteRenderer>();
                r.sortingLayerName = "sfx";
                DontDestroyOnLoad(m_sfxFather);
            }
            return m_sfxFather;
        }
    }

    public static GameObject CreateSfx(SfxSO sfxConfig, Vector3 pos, GameObject owner, float x_scale = 1f, bool isSetParentToOwner = true)
    {
        if (owner == null)
            owner = Instance.sfxFather;
        GameObject parent = owner;
        if (isSetParentToOwner == false)
            parent = Instance.sfxFather;
        var s = sfxConfig.GetInstance(owner, pos, x_scale, parent);
        ResourceManager.Release(s, sfxConfig.effectDuration);
        return s;
    }

    public static void CreateSfxDelay(float delayTime, SfxSO sfxConfig, Vector3 pos, GameObject owner, float x_scale = 1f, bool isSetParentToOwner = true)
    {
        if (delayTime == 0)
            CreateSfx(sfxConfig, pos, owner, x_scale, isSetParentToOwner);
        else
            QuickCoroutineSystem.StartCoroutine(CreateSfxDelay_co(delayTime, sfxConfig, pos, owner, x_scale));
    }

    static IEnumerator CreateSfxDelay_co(float delayTime, SfxSO sfxConfig, Vector3 pos, GameObject owner, float x_scale = 1f, bool isSetParentToOwner = true)
    {
        yield return new WaitForSeconds(delayTime);
        CreateSfx(sfxConfig, pos, owner, x_scale, isSetParentToOwner);
    }
}
