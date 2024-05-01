using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerEquipmentLocation 
{
    Head = 0,
    LeftHand = 1,
    RightHand = 2,
    Breast = 3,
    Leg = 4,
    Weapon = 5,
}

public enum PlayerAttributeType 
{
    MaxHp,
    CurrentHp,
    GlodNum,
    Strength,
    Ductility,
    Luck,
}

public class PlayerData : SingleMono<PlayerData>
{
    public float MaxHP { get; private set; }         //最大血量
    public float CurrentHP { get; private set; }     //当前血量

    public int GoldNum { get; private set; }        //金钱数量

    public int Strength { get; private set; }       //力量 

    public float Ductility { get; private set; }       //韧性

    public int Luck { get; private set; }           //运气

    public Weapon Weapon { get; private set; }       //武器数据
    public Equipment Head                            //头部装备数据
    { 
        get 
        {
            return equipmentList[(int)PlayerEquipmentLocation.Head];
        } 
        private set 
        {
            equipmentList[(int)PlayerEquipmentLocation.Head] = value;
        } 
    }     
    public Equipment LeftHand                        //左手装备数据
    {
        get
        {
            return equipmentList[(int)PlayerEquipmentLocation.LeftHand];
        }
        private set
        {
            equipmentList[(int)PlayerEquipmentLocation.LeftHand] = value;
        }
    } 
    public Equipment RightHand                       //右手装备数据
    {
        get
        {
            return equipmentList[(int)PlayerEquipmentLocation.RightHand];
        }
        private set
        {
            equipmentList[(int)PlayerEquipmentLocation.RightHand] = value;
        }
    }
    public Equipment Breast                          //胸部装备数据
    {
        get
        {
            return equipmentList[(int)PlayerEquipmentLocation.Breast];
        }
        private set
        {
            equipmentList[(int)PlayerEquipmentLocation.Breast] = value;
        }
    }   
    public Equipment Leg                            //下肢装备数据
    {
        get
        {
            return equipmentList[(int)PlayerEquipmentLocation.Leg];
        }
        private set
        {
            equipmentList[(int)PlayerEquipmentLocation.Leg] = value;
        }
    }       

    public List<Equipment> equipmentList;            //根据Location存放装备，方便操作 

    public override void Init()
    {
        var initProperty = ConfigManager.Instance.GetConfig<PropertyConfig>(1);

        MaxHP = CurrentHP = initProperty.hp;

        GoldNum = initProperty.gold;

        Strength = initProperty.strength;

        Ductility = initProperty.ductility;

        Luck = initProperty.luck;
    }

    public void ChangePlayerEquipment(PlayerEquipmentLocation location, Equipment equipment, Weapon weapon) 
    {
        switch (location)
        {
            case PlayerEquipmentLocation.Weapon:
                Weapon = weapon;
                break;
            case PlayerEquipmentLocation.Head:
            case PlayerEquipmentLocation.LeftHand:
            case PlayerEquipmentLocation.RightHand:
            case PlayerEquipmentLocation.Breast:
            case PlayerEquipmentLocation.Leg:
                if (!CheckEquipmentTypeCorrespond(location, equipment)) 
                {
                    Debug.LogError($"错误设置装备 {equipment.equipmentType} -> {location}");
                    return;
                }
                equipmentList[(int)location] = equipment;
                break;
        }

        Notification.Instance.Notify(Notification.PlayerDataEquipmentChanged, location);
    }

    public void ChangePlayerAttribute(PlayerAttributeType playerAttributeType, float val) 
    {
        switch (playerAttributeType)
        {
            case PlayerAttributeType.MaxHp:
                MaxHP = val;
                break;
            case PlayerAttributeType.CurrentHp:
                CurrentHP = val;
                break;
            case PlayerAttributeType.GlodNum:
                GoldNum = (int)val;
                break;
            case PlayerAttributeType.Strength:
                Strength = (int)val;
                break;
            case PlayerAttributeType.Ductility:
                Ductility = val;
                break;
            case PlayerAttributeType.Luck:
                Luck = (int)val;
                break;
        }

        Notification.Instance.Notify(Notification.PlayerDataAttributeChanged, playerAttributeType);
    }

    private bool CheckEquipmentTypeCorrespond(PlayerEquipmentLocation location, Equipment equipment) 
    {
        return (equipment.equipmentType == EquipmentType.Head && location == PlayerEquipmentLocation.Head) ||
               (equipment.equipmentType == EquipmentType.Hand && (location == PlayerEquipmentLocation.LeftHand || location == PlayerEquipmentLocation.RightHand)) ||
               (equipment.equipmentType == EquipmentType.Breast && location == PlayerEquipmentLocation.Breast) ||
               (equipment.equipmentType == EquipmentType.Leg && location == PlayerEquipmentLocation.Leg);
    }
}
