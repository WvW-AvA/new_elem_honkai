using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using DG.Tweening;
public class FKeyTriggerBase : Interactible
{
    public GameObject FKeyPrefab;
    public Sprite iconSprite;
    public Sprite arrawSprite;
    [ReadOnly]
    public bool isFKeyHasDown;
    public UnityEvent OnFKeyDownEvent;
    public UnityEvent OnPlayerExitAreaEvent;

    public float movementTime = 0.3f;
    public float moveDistance = 2;
    public Vector2 iconPosOffset;
    public Vector2 arrawPosOffset;
    private List<string> invokeUIList;
    private GameObject go;
    private GameObject arrawGo;

    private void Awake()
    {
        invokeUIList = new List<string>();
    }
    protected override void OnPlayerTouch()
    {
        base.OnPlayerTouch();
        invokeUIList.Clear();
        var pos = transform.position + (Vector3)iconPosOffset;
        IconShow(ref go, pos, pos + Vector3.up * moveDistance, iconSprite);
        pos = transform.position + (Vector3)arrawPosOffset;
        IconShow(ref arrawGo, pos + Vector3.up * moveDistance, pos, arrawSprite);
    }
    private void IconShow(ref GameObject go, Vector3 beginPos, Vector3 endPos, Sprite sprite)
    {
        if (go == null)
            go = ResourceManager.GetInstance(FKeyPrefab);
        go.transform.position = beginPos;
        go.transform.DOMove(endPos, 0.5f);
        go.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
        go.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    private void IconFadeOut(ref GameObject go, Vector3 endPos)
    {
        go.transform.DOMove(endPos, 0.5f);
        go.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
    }
    protected override void OnPlayerStay()
    {
        base.OnPlayerStay();
        if (isFKeyHasDown == false && InputManager.Keys[EKey.F].isKeyPressing)
        {
            IconFadeOut(ref go, transform.position + (Vector3)iconPosOffset);
            IconFadeOut(ref arrawGo, transform.position + (Vector3)arrawPosOffset + Vector3.up * moveDistance);
            OnFKeyDown();
            if (OnFKeyDownEvent != null)
                OnFKeyDownEvent.Invoke();
            isFKeyHasDown = true;
        }
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();
        if (OnPlayerExitAreaEvent != null)
            OnPlayerExitAreaEvent.Invoke();
        isFKeyHasDown = false;
        IconFadeOut(ref go, transform.position);
        IconFadeOut(ref arrawGo, transform.position + Vector3.up * (5 + moveDistance));
    }

    public virtual void OnFKeyDown()
    {

    }
    public void CreatUI(string uiPath)
    {
        invokeUIList.Add(uiPath);
        UIManager.CreateUI(uiPath);
    }

    public void ReleaseAllUI()
    {
        foreach (var uiPath in invokeUIList)
            UIManager.ReleaseUI(uiPath);
    }

}
