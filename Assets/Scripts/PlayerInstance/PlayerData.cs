using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentLocation 
{
    NULL = -1,
    Head = 0,
    LeftHand = 1,
    RightHand = 2,
    Breast = 3,
    Leg = 4,
    Weapon = 5,
    Count = 6,
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

public class EquipmentSystem
{
    private Weapon _weapon;

    public Weapon Weapon { get { return _weapon; } private set { if (value?.config == null) return; _weapon = value; } }       //武器数据
    public Equipment Head                            //头部装备数据
    {
        get
        {
            return equipmentList[(int)EquipmentLocation.Head];
        }
        private set
        {
            if (value?.config == null) 
            {
                return;
            }
            equipmentList[(int)EquipmentLocation.Head] = value;
        }
    }
    public Equipment LeftHand                        //左手装备数据
    {
        get
        {
            return equipmentList[(int)EquipmentLocation.LeftHand];
        }
        private set
        {
            if (value?.config == null)
            {
                return;
            }
            equipmentList[(int)EquipmentLocation.LeftHand] = value;
        }
    }
    public Equipment RightHand                       //右手装备数据
    {
        get
        {
            return equipmentList[(int)EquipmentLocation.RightHand];
        }
        private set
        {
            if (value?.config == null)
            {
                return;
            }
            equipmentList[(int)EquipmentLocation.RightHand] = value;
        }
    }
    public Equipment Breast                          //胸部装备数据
    {
        get
        {
            return equipmentList[(int)EquipmentLocation.Breast];
        }
        private set
        {
            if (value?.config == null)
            {
                return;
            }
            equipmentList[(int)EquipmentLocation.Breast] = value;
        }
    }
    public Equipment Leg                            //下肢装备数据
    {
        get
        {
            return equipmentList[(int)EquipmentLocation.Leg];
        }
        private set
        {
            if (value?.config == null)
            {
                return;
            }
            equipmentList[(int)EquipmentLocation.Leg] = value;
        }
    }

    public List<Equipment> equipmentList;            //根据Location存放装备，方便操作 

    public EquipmentSystem() 
    {
        equipmentList = new List<Equipment>(5) { null, null, null, null, null };
    }

    public EquipmentSystem(int head, int leftHand, int rightHand, int breast, int leg, int weapon) 
    {
        equipmentList = new List<Equipment>(5) { null, null, null, null, null };

        Head = new Equipment(head);

        LeftHand = new Equipment(leftHand);

        RightHand = new Equipment(rightHand);

        Breast = new Equipment(breast);

        Leg = new Equipment(leg);

        Weapon = new Weapon(weapon);
    }

    public EquipmentSystem(EquipmentSystem system) 
    {
        equipmentList = new List<Equipment>(5) { null, null, null, null, null };

        Head = system.Head;

        LeftHand = system.LeftHand;

        RightHand = system.RightHand;

        Breast = system.Breast;

        Leg = system.Leg;

        Weapon = system.Weapon;
    }

    public Equipment GetEquipmentByLocation(EquipmentLocation location) 
    {
        return equipmentList[(int)location];
    }

    public Weapon GetWeapon() 
    {
        return Weapon;
    }

    public void SetEquipment(EquipmentLocation location, Equipment equipment, Weapon weapon) 
    {
        switch (location)
        {
            case EquipmentLocation.Weapon:
                Weapon = weapon;
                break;
            case EquipmentLocation.Head:
            case EquipmentLocation.LeftHand:
            case EquipmentLocation.RightHand:
            case EquipmentLocation.Breast:
            case EquipmentLocation.Leg:
                if (equipment != null && !CheckEquipmentTypeCorrespond(location, equipment))
                {
                    Debug.LogError($"错误设置装备 {equipment.equipmentType} -> {location}");
                    return;
                }
                equipmentList[(int)location] = equipment;
                break;
        }
    }

    private bool CheckEquipmentTypeCorrespond(EquipmentLocation location, Equipment equipment)
    {
        return CommonUtils.CheckEquipmentTypeCorrespond(location, equipment.equipmentType);
    }
}

public class PlayerData : SingleMono<PlayerData>
{
    public float MaxHP { get; private set; }         //最大血量
    public float CurrentHP { get; private set; }     //当前血量

    public int GoldNum { get; private set; }        //金钱数量

    public int Strength { get; private set; }       //力量 

    public float Ductility { get; private set; }       //韧性

    public int Luck { get; private set; }           //运气

    public EquipmentSystem equipmentSystem;

    public const int ItemCapacity = 3;
    
    public List<Item> ItemList = new List<Item>(ItemCapacity) {null, null, null};

    public void AddItem(Item item)
    {
        bool hasAdd = false;
        for (int i = 0; i < ItemCapacity; i++)
        {
            var bagItem = ItemList[i];
            if (bagItem == null)
            {
                ItemList[i] = item;
                hasAdd = true;
                break;
            }

            if (bagItem.config.id == item.config.id)
            {
                bagItem.num += item.num;
                hasAdd = true;
                break;
            }
        }

        if (!hasAdd)
        {
            Notification.Instance.Notify(Notification.PlayerItemOverflow, item);
        }
    }

    public void RemoveItem(int index)
    {
        ItemList[index] = null;
    }

    public override void Init()
    {
        var initProperty = ConfigManager.Instance.GetConfig<PropertyConfig>(1);

        MaxHP = CurrentHP = initProperty.hp;

        GoldNum = initProperty.gold;

        Strength = initProperty.strength;

        Ductility = initProperty.ductility;

        Luck = initProperty.luck;

        equipmentSystem = new EquipmentSystem(initProperty.playerHead, initProperty.playerLeft, initProperty.playerRight, initProperty.playerBody, initProperty.playerLeg, initProperty.playerWeapon);
    }

    public void ChangePlayerEquipment(EquipmentLocation location, Equipment equipment, Weapon weapon) 
    {
        equipmentSystem.SetEquipment(location, equipment, weapon);

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
}
