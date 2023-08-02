using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
public class CameraManager : ManagerBase<CameraManager>
{
    private CinemachineVirtualCamera m_mainVirtualCamera;
    private GameObject m_camera;
    public static GameObject cameraObject
    {
        get
        {
            if (Instance.m_camera == null)
            {
                Instance.m_camera = GameObject.FindGameObjectWithTag("MainCamera");
                if (Instance.m_camera == null)
                {
                    Instance.m_camera = ResourceManager.GetInstance(GameConst.CameraPrefab);
                }
                AutoSetCameraBoundary();
            }
            return Instance.m_camera;
        }
    }

    public static CinemachineVirtualCamera mainVirtualCamera
    {
        get
        {
            if (Instance.m_mainVirtualCamera == null)
            {
                Instance.m_mainVirtualCamera = cameraObject.GetComponentInChildren<CinemachineVirtualCamera>();
                if (Instance.m_mainVirtualCamera == null)
                    return null;
                DontDestroyOnLoad(Instance.m_camera);
                Instance.m_initialOrthographicSize = Instance.m_mainVirtualCamera.m_Lens.OrthographicSize;
            }
            return Instance.m_mainVirtualCamera;
        }
    }

    private float m_initialOrthographicSize;
    private float m_beginOrthographicSize;
    private float m_endOrthographicSize;
    private TimerNode OrthographicSizeChangeTimer;
    void Start()
    {

    }

    private void Update()
    {
        if (mainVirtualCamera == null || mainVirtualCamera.Follow == null)
            return;
        var v = mainVirtualCamera.Follow.GetComponent<Rigidbody2D>().velocity;
        v.y = 0;
        v = v.normalized * Mathf.Min(v.magnitude * GameManager.globalParam.CameraLookForwardSpeed, GameManager.globalParam.CameraLookForwardMaxDistance);
        mainVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = v;
    }
    public static void cameraOrthographicSizeChange(float changeTime, float targetSize)
    {
        Instance.m_beginOrthographicSize = Instance.m_mainVirtualCamera.m_Lens.OrthographicSize;
        Instance.m_endOrthographicSize = targetSize;
        if (Instance.OrthographicSizeChangeTimer == null)
        {
            Instance.OrthographicSizeChangeTimer = TimerManager.Schedule("CameraOrthographicSizeChangeTimer", changeTime, 0, false, null, (float duration) =>
            {
                float d = 1.0f - duration / Instance.OrthographicSizeChangeTimer.reloadValue;
                Instance.m_mainVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(Instance.m_beginOrthographicSize, Instance.m_endOrthographicSize, d);
            });
        }
        else
        {
            Instance.OrthographicSizeChangeTimer.reloadValue = changeTime;
        }
        Instance.OrthographicSizeChangeTimer.Reload();
    }

    public static void cameraOrthographicResetSize(float changeTime)
    {
        cameraOrthographicSizeChange(changeTime, Instance.m_initialOrthographicSize);
    }
    public static void BindCameraFollow(GameObject target)
    {
        mainVirtualCamera.Follow = target.transform;
    }
    public static void AutoSetCameraBoundary()
    {
        if (GameObject.FindGameObjectWithTag("CameraBound") == null)
            return;
        var boundary = GameObject.FindGameObjectWithTag("CameraBound").GetComponent<Collider2D>();
        mainVirtualCamera.gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = boundary;
    }
    private static CinemachineImpulseSource GetImpulseSource(GameObject target)
    {
        CinemachineImpulseSource impulseSource = target.GetComponent<CinemachineImpulseSource>();
        if (impulseSource == null)
            impulseSource = target.AddComponent<CinemachineImpulseSource>();
        return impulseSource;
    }
    public static void cameraShake(GameObject shakeSource)
    {
        var impulseSource = GetImpulseSource(shakeSource);
        impulseSource.GenerateImpulse();
    }
    public static void cameraShake(GameObject shakeSource, SignalSourceAsset signalSourceAsset)
    {
        var impulseSource = GetImpulseSource(shakeSource);
        impulseSource.m_ImpulseDefinition.m_RawSignal = signalSourceAsset;
        impulseSource.GenerateImpulse();
    }
    public static void cameraShake(GameObject shakeSource, SignalSourceAsset signalSourceAsset, Vector2 velocity)
    {
        var impulseSource = GetImpulseSource(shakeSource);
        impulseSource.m_ImpulseDefinition.m_RawSignal = signalSourceAsset;
        impulseSource.GenerateImpulse(velocity);
    }
    public static void cameraShake(GameObject shakeSource, SignalSourceAsset signalSourceAsset, float force)
    {
        var impulseSource = GetImpulseSource(shakeSource);
        impulseSource.m_ImpulseDefinition.m_RawSignal = signalSourceAsset;
        impulseSource.GenerateImpulse(force);
    }

}
