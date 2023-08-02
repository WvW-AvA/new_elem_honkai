using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
public class FloatString : MonoBehaviour
{
    public float effectTime;
    public int moveDistance;
    public Vector2 moveDirection;
    public Vector2 posOffset;

    //状态标注，1为出现 -1为消失
    public int stateFlag = 0;
    protected float timer = 0;
    protected RectTransform rect;
    private TextMeshPro m_textMesh;
    protected TextMeshPro textMesh
    {
        get
        {
            if (m_textMesh == null)
                m_textMesh = GetComponent<TextMeshPro>();
            return m_textMesh;
        }
    }
    protected Vector3 beginPos;
    protected Vector3 endPos;


    private string m_stringValue;
    public string stringValue
    {
        get { return m_stringValue; }
        set
        {
            textMesh.text = value;
            m_stringValue = value;
        }
    }
    public Color color { set { textMesh.color = value; } }
    public float bias;
    [Range(1f, 1.5f)]
    public float sizeChangeRate;

    private float beginSize;
    private float endSize;
    private void Start()
    {
        rect = transform as RectTransform;
        moveDirection.Normalize();
        beginPos = rect.position + (Vector3)posOffset - (Vector3)(moveDirection.normalized * moveDistance);
        endPos = rect.position + (Vector3)posOffset;
        beginPos += Random.onUnitSphere * bias;
        endPos += Random.onUnitSphere * bias;
        beginSize = textMesh.fontSize * Random.Range(0.5f, 1f);
        endSize = beginSize * sizeChangeRate * Random.Range(1f, 2f);
    }
    protected void Update()
    {
        if (stateFlag == 0)
            return;
        if (stateFlag == 1 && timer < effectTime)
            timer += Time.deltaTime;
        else if (stateFlag == -1 && timer > 0)
            timer -= Time.deltaTime;
        else
        {
            stateFlag = 0;
        }
        var p = timer / effectTime;
        if (stateFlag == 1)
        {
            rect.position = Vector3.Lerp(beginPos, endPos, p);
        }
        textMesh.fontSize = (int)Mathf.Lerp(beginSize, endSize, p);
        var col = textMesh.color;
        col.a = Mathf.Lerp(0, 1, p);
        textMesh.color = col;
    }

}
