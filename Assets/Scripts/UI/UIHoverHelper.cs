using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UIHoverHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static UIHoverHelper CurrentHover { get; private set; }

    public GameObject HoverShowGO;

    public Vector2 Offset;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        CurrentHover = this;
        
        HoverShowGO.SetActive(true);
        HoverShowGO.GetComponent<RectTransform>().anchoredPosition =
            this.GetComponent<RectTransform>().anchoredPosition + Offset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CurrentHover = null;
        
        HoverShowGO.SetActive(false);
    }
}
