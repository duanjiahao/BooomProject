using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentInfoUI : MonoBehaviour
{
    public Text nameTxt;

    public Image icon;

    public Text attack;

    public Text hp;

    public Text price;

    public Text consume;

    public Text effect;

    public GameObject root;

    private void OnEnable()
    {
        var helper = UIHoverHelper.CurrentHover;
        if (helper != null)
        {
            var equipmentUI = helper.gameObject.GetComponent<EquipmentUI>();
            if (equipmentUI != null)
            {
                if (equipmentUI.Location == EquipmentLocation.Weapon)
                {
                    RefreshUI(null, equipmentUI.IsPlayer ? (equipmentUI.IsBattle ? BattleManager.Instance.GetCurrentHero().equipmentSystem.Weapon : PlayerData.Instance.equipmentSystem.Weapon) : BattleManager.Instance.GetCurrentMonster().equipmentSystem.Weapon);
                }
                else
                {
                    RefreshUI(equipmentUI.IsPlayer ? (equipmentUI.IsBattle ? BattleManager.Instance.GetCurrentHero().equipmentSystem.GetEquipmentByLocation(equipmentUI.Location) : PlayerData.Instance.equipmentSystem.GetEquipmentByLocation(equipmentUI.Location)) : BattleManager.Instance.GetCurrentMonster().equipmentSystem.GetEquipmentByLocation(equipmentUI.Location), null);
                }
            }
        }
    }

    public void RefreshUI(Equipment equipment, Weapon weapon)
    {
        root.SetActive(equipment != null || weapon != null);
        if (equipment == null && weapon == null)
        {
            return;
        }

        if (equipment != null)
        {
            nameTxt.text = equipment.config.armorName;
            icon.sprite = Resources.Load<Sprite>(equipment.config.armorIcon);
            attack.text = $"减伤：{equipment.config.armorValue}";
            hp.text = $"耐久：{equipment.Hp}";
            price.text = $"价格：{equipment.config.armorPrice}";
            consume.text = $"消耗：1";
            effect.text = $"{equipment.config.armorDesc}";
        }
        else
        {
            nameTxt.text = weapon.config.weaponName;
            icon.sprite = Resources.Load<Sprite>(weapon.config.weaponIcon);
            attack.text = $"攻击：{weapon.config.weaponAttack[0]}-{weapon.config.weaponAttack[1]}";
            hp.text = $"耐久：{weapon.Hp}";
            price.text = $"价格：{weapon.config.weaponPrice}";
            consume.text = $"消耗：{weapon.config.actionPoint}";
            effect.text = $"{weapon.config.weaponDesc}";
        }
    }
}
