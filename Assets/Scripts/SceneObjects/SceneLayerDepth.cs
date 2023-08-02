using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SceneLayerDepth : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    private List<Transform> allMapSprites;
    private List<Vector3> originPos;
    private void Awake()
    {
        allMapSprites = new List<Transform>();
        originPos = new List<Vector3>();
        foreach (var g in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            allMapSprites.Add(g.gameObject.transform);
            var temp = g.gameObject.transform.position;
            originPos.Add(temp);
        }
    }

    private void Update()
    {
        var camera_pos = CameraManager.cameraObject.transform.position;
        for (int i = 0; i < originPos.Count; i++)
        {
            var temp = originPos[i];
            var v = (temp.x - camera_pos.x) * ((0 - camera_pos.z) / (temp.z * 0.2f - camera_pos.z));
            temp.x = v + camera_pos.x;
            allMapSprites[i].position = temp;
        }
    }
}
