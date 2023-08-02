using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
public class UI_Icon : UI_Base
{
    public bool showEffectEnable = false;
    [ShowIf("showEffectEnable")]
    [BoxGroup("effectParam")]
    public float effectTime;
    [BoxGroup("effectParam")]
    [ShowIf("showEffectEnable")]
    public int moveDistance;
    [BoxGroup("effectParam")]
    [ShowIf("showEffectEnable")]
    public Vector2 moveDirection;
    [BoxGroup("effectParam")]
    [ShowIf("showEffectEnable")]
    public bool isEnableAlaphChange = false;
    [BoxGroup("effectParam")]
    [ShowIf("isEnableAlaphChange")]
    public bool isChildImageChange = false;
    //状态标注，1为出现 -1为消失
    protected int stateFlag = 0;
    protected float timer = 0;
    protected RectTransform rect;
    protected List<Image> images;
    protected List<Text> texts;
    protected Dictionary<int, float> originAlpha;
    protected Vector3 beginPos;
    protected Vector3 endPos;
    [NonSerialized]
    public UnityEvent onEffectDoneEvent = new UnityEvent();
    protected override void OnUIInit()
    {
        base.OnUIInit();
        if (showEffectEnable == false)
            return;
        rect = transform as RectTransform;
        images = new List<Image>();
        texts = new List<Text>();
        originAlpha = new Dictionary<int, float>();
        if (isEnableAlaphChange)
        {
            if (isChildImageChange)
            {
                images = new List<Image>(GetComponentsInChildren<Image>());
                texts = new List<Text>(GetComponentsInChildren<Text>());
            }
            if (GetComponent<Image>())
                images.Add(GetComponent<Image>());
            if (GetComponent<Text>())
                texts.Add(GetComponent<Text>());
            foreach (var im in images)
                if (originAlpha.ContainsKey(im.GetHashCode()) == false)
                    originAlpha.Add(im.GetHashCode(), im.color.a);
            foreach (var tx in texts)
                if (originAlpha.ContainsKey(tx.GetHashCode()) == false)
                    originAlpha.Add(tx.GetHashCode(), tx.color.a);
        }
        moveDirection.Normalize();
        beginPos = rect.position - (Vector3)(moveDirection.normalized * moveDistance);
        endPos = rect.position;
    }

    public override void Update()
    {
        if (InputManager.Keys[EKey.TAB].isKeyUp)
        {
            IsEnable = !IsEnable;
        }
        base.Update();
    }
    protected override void OnUIEnable()
    {
        base.OnUIEnable();
        if (showEffectEnable == false)
            return;
        stateFlag = 1;
    }
    protected override void OnUIDisable()
    {
        if (showEffectEnable == false)
        {
            if (onEffectDoneEvent != null)
                onEffectDoneEvent.Invoke();
            base.OnUIDisable();
            return;
        }
        stateFlag = -1;
    }
    protected override void OnUIUpdate()
    {
        base.OnUIUpdate();
        if (showEffectEnable == false || stateFlag == 0)
            return;
        if (stateFlag == 1 && timer < effectTime)
            timer += Time.deltaTime;
        else if (stateFlag == -1 && timer > 0)
            timer -= Time.deltaTime;
        else
        {
            stateFlag = 0;
            if (timer < 0)
            {
                if (onEffectDoneEvent != null)
                    onEffectDoneEvent.Invoke();
                base.OnUIDisable();
            }
        }
        var p = timer / effectTime;
        rect.position = Vector3.Lerp(beginPos, endPos, p);
        if (isEnableAlaphChange)
        {
            foreach (var image in images)
            {
                var col = image.color;
                col.a = Mathf.Lerp(0, originAlpha[image.GetHashCode()], p);
                image.color = col;
            }
            foreach (var text in texts)
            {
                var col = text.color;
                col.a = Mathf.Lerp(0, originAlpha[text.GetHashCode()], p);
                text.color = col;
            }
        }

    }
}
