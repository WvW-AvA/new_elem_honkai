using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_UIEnlargeEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    RectTransform rec;
    private void Awake()
    {
        rec = gameObject.GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entry");
        rec.localScale = new Vector3(1.2f,1.2f, 0);
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Entry");
        rec.localScale = new Vector3(1f,1f, 0);
        //throw new System.NotImplementedException();
    }
}
