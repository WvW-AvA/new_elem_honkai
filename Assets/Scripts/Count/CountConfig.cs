using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class CountData
{
    public string countName = "UnkownCount";
    public int count = 0;
}
[CreateAssetMenu(fileName = "CountConfig", menuName = "ScriptableObjects/CountConfig")]

public class CountConfig : ScriptableObject
{
    public int count = 0;
    public List<CountData> countList = new List<CountData>();
}
#if UNITY_EDITOR
[CustomEditor(typeof(CountConfig))]
public class CountConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
#endif
