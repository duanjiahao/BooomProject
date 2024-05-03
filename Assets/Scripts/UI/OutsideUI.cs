using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutsideUI : MonoBehaviour
{
    public Image headIcon;

    public Image hp;

    public Text hpTxt;

    public ItemFullUI itemFullUI;

    public Text gold;

    public Button settingBtn;

    private void OnEnable()
    {
        RefreshUI(null);
        Notification.Instance.Register(Notification.PlayerDataAttributeChanged, RefreshUI);
        Notification.Instance.Register(Notification.PlayerItemOverflow, OnItemOverflow);
    }

    private void OnItemOverflow(object data)
    {
        itemFullUI.gameObject.SetActive(true);
        itemFullUI.RefreshUI((Item)data, -1);
    }

    private void OnDisable()
    {
        Notification.Instance.Unregister(Notification.PlayerDataAttributeChanged, RefreshUI);
        Notification.Instance.Register(Notification.PlayerItemOverflow, OnItemOverflow);
    }

    private void RefreshUI(object data)
    {
        hp.fillAmount = PlayerData.Instance.CurrentHP / PlayerData.Instance.MaxHP;

        hpTxt.text = $"{(int)PlayerData.Instance.CurrentHP}/{PlayerData.Instance.MaxHP}";

        gold.text = PlayerData.Instance.GoldNum.ToString();
    }
}
