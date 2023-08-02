using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "AudioClipConfig", menuName = "ScriptableObjects/AudioClipConfig", order = 2)]
public class AudioConfig : ScriptableObject
{
    public AudioClip clip;

    public float endPoint;

    private void Awake()
    {
        //TODO: Audios systems should be completed.
    }
}

[CustomEditor(typeof(AudioConfig))]
public class AudioConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}