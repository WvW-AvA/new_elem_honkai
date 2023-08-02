using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : ManagerBase<AudioManager>
{
    public int soundEffectListSize = 20;
    public float canHearRadius = 100;

    private AudioSource m_bgmSource;
    private AudioSource bgmSource
    {
        get
        {
            if (m_bgmSource == null)
            {
                m_bgmSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
                if (m_bgmSource == null)
                    m_bgmSource = GameObject.Find("GameManager").AddComponent<AudioSource>();
                m_bgmSource.maxDistance = 10000000;
            }
            return m_bgmSource;
        }
    }

    private List<GameObject> m_SoundEffectList;
    public List<GameObject> SoundEffectList
    {
        get
        {
            if (m_SoundEffectList is null)
                m_SoundEffectList = new List<GameObject>();
            return m_SoundEffectList;
        }
    }
    private Vector2 playerPos;

    protected override void Awake()
    {
        base.Awake();
    }

    public static void BgmChange(AudioClip clip)
    {
        Instance.bgmSource.Pause();
        Instance.bgmSource.clip = clip;
        Instance.bgmSource.loop = true;
        Instance.bgmSource.Play();
    }

    public static void PlayOneShot(string audioName, Vector3 pos)
    {
        if (string.IsNullOrEmpty(audioName))
            return;
        if (Instance.SoundEffectList.Count <= Instance.soundEffectListSize && Instance.IsInArea(pos))
        {
            Instance.AddSoundEffectList(Instance.PlaySoundAtGO(audioName, pos));
        }
    }
    public static void PlayOneShot(AudioClip clip, Vector3 pos)
    {
        if (clip == null)
            return;
        if (Instance.SoundEffectList.Count <= Instance.soundEffectListSize && Instance.IsInArea(pos))
        {
            Instance.AddSoundEffectList(Instance.PlaySoundAtGO(clip, pos));
        }
    }
    public static void PlayOneShot(AudioClip clip, Vector3 pos, float delayTime)
    {
        if (clip == null)
            return;
        if (delayTime == 0)
            PlayOneShot(clip, pos);
        else
            QuickCoroutineSystem.StartCoroutine(Instance.PlayOnsShot_co(clip, pos, delayTime));
    }

    private IEnumerator PlayOnsShot_co(AudioClip clip, Vector3 pos, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        PlayOneShot(clip, pos);
    }

    public GameObject PlaySoundAtGO(string resPath, Vector3 pos)
    {
        var tem = new GameObject("SoundEffect");
        tem.transform.position = pos;
        var aud = tem.AddComponent<AudioSource>();
        aud.clip = ResourceManager.GetInstance<AudioClip>("Sound/" + resPath);
        aud.Play();
        return tem;
    }
    public GameObject PlaySoundAtGO(AudioClip clip, Vector3 pos)
    {
        var tem = new GameObject("SoundEffect");
        tem.transform.position = pos;
        var aud = tem.AddComponent<AudioSource>();
        aud.clip = clip;
        aud.Play();
        return tem;
    }


    private bool IsInArea(Vector3 pos)
    {
        playerPos = Player.instance.transform.position;
        if (Vector3.Distance(playerPos, pos) > canHearRadius)
            return false;
        return true;
    }

    private void AddSoundEffectList(GameObject go)
    {
        SoundEffectList.Add(go);
        var eve = AudioEvent.AddComponentToGameObject(go);
        eve.EventPlayEnd += (AudioEvent eve) => { SoundEffectList.Remove(go); ResourceManager.Release(go); };
    }
}
