using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

class AudioEvent : MonoBehaviour
{

    /// <summary>
    /// ����ű����ڵ������ϵ�audioSource
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// ���ſ�ʼ�¼�
    /// </summary>
    public event Action<AudioEvent> EventPlayStart;

    /// <summary>
    /// ���Ž����¼�
    /// </summary>
    public event Action<AudioEvent> EventPlayEnd;

    /// <summary>
    /// ���ŵ���ֵ�¼�
    /// </summary>
    public event Action<AudioEvent> EventPlayToValue;
    /// <summary>
    /// ��ز���״̬
    /// </summary>
    bool _lastPlayStatus;
    float _playTime;
    /// <summary>
    /// ��һ���������������¼�������
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static AudioEvent AddComponentToGameObject(GameObject obj)
    {
        AudioEvent com = obj.GetComponent<AudioEvent>();
        if (com == null)
        {
            com = obj.AddComponent<AudioEvent>();
        }
        return com;
    }

    void Awake()
    {
        //���û�����AudioSource�����Ǿ�Ҫ���һ��
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.Stop();
            audioSource.playOnAwake = false;
        }
        _lastPlayStatus = false;

    }

    /// <summary>
    /// ���һ�µ�ǰ����״̬
    /// </summary>
    private void UpdatePlaySstatus()
    {
        if (_lastPlayStatus == false && audioSource.isPlaying == true)
        {
            if (EventPlayStart != null)
            {
                EventPlayStart(this);//�����¼�����ʼ����
            }
        }
        if (_lastPlayStatus == true && audioSource.isPlaying == false)
        {
            if (EventPlayEnd != null)
            {
                EventPlayEnd(this);//�����¼�������ֹͣ
            }
        }
        _lastPlayStatus = audioSource.isPlaying;

    }


    public void Update()
    {
        UpdatePlaySstatus();
    }

    void OnDestoryed()
    {
        //������������Ƿ���Ҫ��������ֹͣ�¼���
        if (_lastPlayStatus == true)
        {
            if (EventPlayEnd != null)
            {
                EventPlayEnd(this);//�����¼�������ֹͣ
            }
        }
    }


}
