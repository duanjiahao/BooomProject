using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFullUI : MonoBehaviour
{
    public Image icon;

    public Button replaceBtn;

    public Button discardBtn;

    private Item _item;

    private int _selectedItemIndex;

    private void OnEnable()
    {
        replaceBtn.onClick.AddListener(OnReplaceBtnClicked);
        discardBtn.onClick.AddListener(OnDiscardBtnClicked);
    }

    private void OnDiscardBtnClicked()
    {
        this.gameObject.SetActive(false);
    }

    private void OnReplaceBtnClicked()
    {
        PlayerData.Instance.RemoveItem(_selectedItemIndex);
        PlayerData.Instance.AddItem(_item);
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        replaceBtn.onClick.RemoveListener(OnReplaceBtnClicked);
        discardBtn.onClick.RemoveListener(OnDiscardBtnClicked);
    }

    public void RefreshUI(Item extraItem, int selectedItemIndex)
    {
        _item = extraItem;
        _selectedItemIndex = selectedItemIndex;
        
        icon.sprite = Resources.Load<Sprite>(extraItem.config.itemIcon);

        replaceBtn.interactable = selectedItemIndex != -1;
    }
}
