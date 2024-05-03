using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UIClickHelper : MonoBehaviour, IPointerClickHandler
{
    public static UIClickHelper CurrentClick { get; private set; }

    public GameObject ClickGO;

    public Vector2 Offset;

    public void OnPointerClick(PointerEventData eventData)
    {
        var showing = ClickGO.activeSelf;

        CurrentClick = showing ? null : this;
        
        ClickGO.SetActive(!showing);
        ClickGO.GetComponent<RectTransform>().anchoredPosition =
            this.GetComponent<RectTransform>().anchoredPosition + Offset;
    }
}
