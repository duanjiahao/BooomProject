using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverShowHide : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<GameObject> showObjects;

    public List<GameObject> hideObjects;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        for (int i = 0; i < showObjects?.Count; i++)
        {
            showObjects[i].SetActive(true);
        }
        
        for (int i = 0; i < hideObjects?.Count; i++)
        {
            hideObjects[i].SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        for (int i = 0; i < showObjects?.Count; i++)
        {
            showObjects[i].SetActive(false);
        }
        
        for (int i = 0; i < hideObjects?.Count; i++)
        {
            hideObjects[i].SetActive(true);
        }
    }
}
