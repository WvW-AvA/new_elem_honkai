using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using UnityEngine.UI;
public class UI_ElementAttach_Icon : UI_Icon
{
    public Image AttachedElementImage;
    public Text ElementLayerCount;
    public Pawn target;

    private EElement lastElement;
    private int lastLayerCount;
    protected override void OnUIUpdate()
    {
        base.OnUIUpdate();
        if (lastElement == target.attachedElement && lastLayerCount == target.attachedElementCount)
            return;
        lastElement = target.attachedElement;
        lastLayerCount = target.attachedElementCount;
        if (lastElement == EElement.NONE || lastLayerCount == 0)
        {
            AttachedElementImage.sprite = null;
            ElementLayerCount.text = "";
            return;
        }
        AttachedElementImage.sprite = Element.ElementIcons[lastElement];
        ElementLayerCount.text = lastLayerCount.ToString();
        ElementLayerCount.color = Element.ElementFloatStringColor[lastElement];
    }
}
