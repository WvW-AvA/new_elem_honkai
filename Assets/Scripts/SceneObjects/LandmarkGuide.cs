using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class LandmarkGuide : Interactible
{
    [Range(10, 15)]
    public float cameraFarAwayDistance = 11.5f;
    public Sprite sprite;
    public float cameraMoveTime;
    public float keepTime;
    protected override void OnPlayerTouch()
    {
        base.OnPlayerTouch();
        UIManager.landmarkUI.gameObject.GetComponent<Image>().sprite = sprite;
        UIManager.landmarkUI.gameObject.GetComponent<Image>().SetNativeSize();
        UIManager.landmarkUI.IsEnable = true;
        CameraManager.cameraOrthographicSizeChange(cameraMoveTime, cameraFarAwayDistance);
        TimerManager.Schedule(sprite.name + "Show Timer", keepTime, keepTime, false, () =>
        {
            UIManager.landmarkUI.IsEnable = false;
            CameraManager.cameraOrthographicResetSize(cameraMoveTime);
            this.gameObject.SetActive(false);
        });
    }
}
