using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EventUI : MonoBehaviour
{
    public Text eventName;

    public Image eventIcon;

    public Text eventDesc;

    public Transform container;

    private void OnEnable()
    {
        var eventConfigList = ConfigManager.Instance.GetConfigListWithFilter<EventConfig>((config) =>
        {
            var list = new List<int>(config.eventLevel);
            return list.Contains(1);
        });

        var randomConfig = eventConfigList[Random.Range(0, eventConfigList.Count)];
        
        RefreshUI(randomConfig);
    }

    private void RefreshUI(EventConfig config)
    {
        eventName.text = config.eventName;
        eventDesc.text = config.eventDesc;

        eventIcon.sprite = Resources.Load<Sprite>(config.eventImgPath);

        if (!string.IsNullOrEmpty(config.option1))
        {
            var itemGO = container.GetChild(0);
            itemGO.gameObject.SetActive(true);
            var mono = itemGO.GetComponent<EventSelection>();
            mono.RefreshUI(config.option1);
            mono.btn.onClick.RemoveAllListeners();
            mono.btn.onClick.AddListener(() =>
            {
                OnSelectionClicked(config.option1Eff, config.option1EffValue);
            });
        }
        else
        {
            var itemGO = container.GetChild(0);
            itemGO.gameObject.SetActive(false);
        }

        if (!string.IsNullOrEmpty(config.option2))
        {
            var itemGO = container.GetChild(1);
            itemGO.gameObject.SetActive(true);
            var mono = itemGO.GetComponent<EventSelection>();
            mono.RefreshUI(config.option2);
            mono.btn.onClick.RemoveAllListeners();
            mono.btn.onClick.AddListener(() =>
            {
                OnSelectionClicked(config.option1Eff, config.option1EffValue);
            });
        }
        else
        {
            var itemGO = container.GetChild(1);
            itemGO.gameObject.SetActive(false);
        }
        
        if (!string.IsNullOrEmpty(config.option3))
        {
            var itemGO = container.GetChild(2);
            itemGO.gameObject.SetActive(true);
            var mono = itemGO.GetComponent<EventSelection>();
            mono.RefreshUI(config.option3);
            mono.btn.onClick.RemoveAllListeners();
            mono.btn.onClick.AddListener(() =>
            {
                OnSelectionClicked(config.option1Eff, config.option1EffValue);
            });
        }
        else
        {
            var itemGO = container.GetChild(2);
            itemGO.gameObject.SetActive(false);
        }
    }

    private void OnSelectionClicked(int type, int effectValue)
    {
        var effectType = (EffectType)type;
        switch (effectType)
        {
            case EffectType.JumpNextEvent:
                RefreshUI(ConfigManager.Instance.GetConfig<EventConfig>(effectValue));
                return;
            case EffectType.GetItem:
                PlayerData.Instance.AddItem(new Item(effectValue));
                break;
            case EffectType.HeadRecDamage:
                break;
            case EffectType.BreastRecDamage:
                break;
            case EffectType.LeftHandRecDamage:
                break;
            case EffectType.RightHandRecDamage:
                break;
            case EffectType.LegRecDamage:
                break;
            case EffectType.AllRecDamage:
                break;
            case EffectType.WeaponHpDown:
                break;
            case EffectType.HeadHpDown:
                break;
            case EffectType.BreastHpDown:
                break;
            case EffectType.LeftHandHpDown:
                break;
            case EffectType.RightHandHpDown:
                break;
            case EffectType.LegHpDown:
                break;
            case EffectType.AllHpDown:
                break;
            case EffectType.MaxHpDown:
                break;
            case EffectType.StrengthDown:
                break;
            case EffectType.GoldChange:
                break;
        }
        
        UIManager.Instance.ReturnOutside();
    }
}
