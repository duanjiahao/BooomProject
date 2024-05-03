using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image selected;

    public Image icon;

    public Text num;

    [Range(0, 2)]
    public int index;

    private void OnEnable()
    {
        RefreshUI();
    }

    private void OnDisable()
    {
    }

    public void RefreshUI()
    {
        var item = PlayerData.Instance.ItemList[index];

        if (item != null)
        {
            icon.gameObject.SetActive(true);
            num.gameObject.SetActive(true);
            icon.sprite = Resources.Load<Sprite>(item.config.itemIcon);
            num.text = item.num.ToString();
        }
        else
        {
            icon.gameObject.SetActive(false);
            num.gameObject.SetActive(false);
        }
    }
}
