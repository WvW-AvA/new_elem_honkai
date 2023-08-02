using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class UI_FloatString_Icon : UI_Icon
{
    private string m_stringValue;
    public string stringValue
    {
        get { return m_stringValue; }
        set
        {
            text.text = value;
            m_stringValue = value;
        }
    }
    public Color color { set { text.color = value; } }
    public Text text;
    public float bias;
    [Range(1f, 1.5f)]
    public float sizeChangeRate;

    private float beginSize;
    private float endSize;
    protected override void OnUIInit()
    {
        if (showEffectEnable == false)
            return;
        rect = transform as RectTransform;
        images = new List<Image>();
        texts = new List<Text>();
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
        }
        moveDirection.Normalize();
        beginPos = rect.position - (Vector3)(moveDirection.normalized * moveDistance);
        endPos = rect.position;
        beginPos += Random.onUnitSphere * bias;
        endPos += Random.onUnitSphere * bias;
        beginSize = text.fontSize * Random.Range(0.5f, 1f);
        endSize = beginSize * sizeChangeRate * Random.Range(1f, 2f);
        IsEnable = true;
    }
    protected override void OnUIUpdate()
    {
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
                base.OnUIDisable();
        }
        var p = timer / effectTime;
        if (stateFlag == 1)
        {
            rect.position = Vector3.Lerp(beginPos, endPos, p);
        }
        text.fontSize = (int)Mathf.Lerp(beginSize, endSize, p);
        if (isEnableAlaphChange)
        {
            foreach (var image in images)
            {
                var col = image.color;
                col.a = Mathf.Lerp(0, 1, p);
                image.color = col;
            }
            foreach (var text in texts)
            {
                var col = text.color;
                col.a = Mathf.Lerp(0, 1, p);
                text.color = col;
            }
        }


    }
}
