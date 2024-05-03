using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIntroUI : MonoBehaviour
{
    public Text desc;

    public Image consume;

    public Button btn;

    public bool battling = false;

    private void OnEnable()
    {
        var click = UIClickHelper.CurrentClick;
        var itemUI = click.gameObject.GetComponent<ItemUI>();
        if (itemUI != null)
        {
            RefreshUI(PlayerData.Instance.ItemList[itemUI.index]);
        }
    }

    public void RefreshUI(Item item)
    {
        desc.text = item.config.itemDesc;
        
        consume.gameObject.SetActive(item.config.itemPos == 4); // 仅战斗就认为是消耗一个行动点的道具

        bool canUse = (battling && (item.config.itemPos == 2 || item.config.itemPos == 4) ||
                       (!battling && (item.config.itemPos == 2 || item.config.itemPos == 3)));
        btn.gameObject.SetActive(canUse);
    }
}
