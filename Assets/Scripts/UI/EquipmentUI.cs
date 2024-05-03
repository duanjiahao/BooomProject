using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public Image hp;

    public Text hpTxt;

    public EquipmentLocation Location;

    public bool IsPlayer = true;

    private void OnEnable()
    {
        RefreshUI(null);
        
        Notification.Instance.Register(Notification.BattleAfterHeroPerform, RefreshUI);
        Notification.Instance.Register(Notification.BattleAfterMonsterPerform, RefreshUI);
    }

    private void OnDisable()
    {
        Notification.Instance.Unregister(Notification.BattleAfterHeroPerform, RefreshUI);
        Notification.Instance.Register(Notification.BattleAfterMonsterPerform, RefreshUI);
    }

    public void RefreshUI(object data)
    {
        var equipSystem = IsPlayer ? PlayerData.Instance.equipmentSystem : BattleManager.Instance.GetCurrentMonster().equipmentSystem;
        if (Location == EquipmentLocation.Weapon)
        {
            var weapon = equipSystem.Weapon;

            if (weapon != null)
            {
                hp.fillAmount = Mathf.Clamp01(weapon.Hp / weapon.config.weapomDurable);
                hpTxt.text = $"{(int)weapon.Hp}/{weapon.config.weapomDurable}";
            }
            else
            {
                hp.fillAmount = 0;
                hpTxt.text = $"-/-";
            }
        }
        else
        {
            var equipment = equipSystem.GetEquipmentByLocation(Location);
            if (equipment != null)
            {
                hp.fillAmount = Mathf.Clamp01(equipment.Hp / equipment.config.armorDurable);
                hpTxt.text = $"{(int)equipment.Hp}/{equipment.config.armorDurable}";
            }
            else
            {
                hp.fillAmount = 0;
                hpTxt.text = $"-/-";
            }
        }
    }
}
