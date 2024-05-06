using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Marketable : MonoBehaviour, IPointerClickHandler
{
    public Equipment thisEquipment = null;
    public Weapon thisWeapon = null;
    public Item thisItem = null;



    public void OnPointerClick(PointerEventData eventData)
    {
        if (StoreUI.onbody.Contains(this))
        {
            OnSold();
            print("SELL");
        }
        else if (StoreUI.inMarket.Contains(this))
        {
            OnBought();
            print("BUY");
        }
    }

    //身上的装备和物品被点击只能被售卖
    public void OnSold()
    {
        DestroyImmediate(gameObject);
    }

    //商店里的物品被点击只能被购买
    public void OnBought()
    {
        DestroyImmediate(gameObject);
    }
}
