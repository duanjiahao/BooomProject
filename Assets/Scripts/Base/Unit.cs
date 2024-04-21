using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit
{
    protected UnitAttributeSO UnitAttribute;

    public float Hp 
    {
        get 
        {
            return UnitAttribute.Hp.Value;
        }
        set 
        {
            UnitAttribute.Hp.Value = Mathf.Max(value, 0f);
        }
    }

    public float MaxHp 
    {
        get
        {
            return UnitAttribute.MaxHp;
        }
    }

    public Equipment LeftHand;
    public Equipment RightHand;
    public Equipment LeftFoot;
    public Equipment RightFoot;
    public Equipment Breast;
    public Equipment Weapon;

    // 是否在防御
    public bool isDefending;

    // 正在防御的位置
    public EquipmentType defendingLocation;

    // 是否破甲
    public bool isBreaking;

    protected GameObject Root;

    protected GameObject RootUI;

    // 生成对应的显示层
    public virtual void GenerateGameObject()
    {

        //实例化的是SO的实例
        UnitAttribute = GameObject.Instantiate(Root.GetComponent<UnitDataHolder>().UnitData);

        // 直接设置动态数值的初始值
        UnitAttribute.Hp.Value = UnitAttribute.MaxHp;

        LeftHand = new Equipment(UnitAttribute.LeftHand);
        RightHand = new Equipment(UnitAttribute.RightHand);
        LeftFoot = new Equipment(UnitAttribute.LeftFoot);
        RightFoot = new Equipment(UnitAttribute.RightFoot);
        Breast = new Equipment(UnitAttribute.Breast);
        Weapon = new Equipment(UnitAttribute.Weapon);
    }

    public void Defend(EquipmentType location)
    {
        isDefending = true;
        defendingLocation = location;
    }

    //  每次行动前解除防御
    public void StopDefending()
    {
        isDefending = false;
        defendingLocation = EquipmentType.NULL;
    }
    public void SetBreaking()
    {
        isBreaking = true;
    }

    //  每次行动前解除防御
    public void StopBreaking()
    {
        isBreaking = false;
    }

    // 攻击别人
    public virtual void Attack(Unit target, EquipmentType location)
    {
        target.BeAttacked(this, location);
    }

    //被别人攻击
    public virtual void BeAttacked(Unit attacker, EquipmentType location)
    {
        // 播放受击动画

        // 计算血量减少，对应护甲耐久减少
        var damage = Random.Range(attacker.Weapon?.MinDamage ?? 1f, attacker.Weapon?.MaxDamage ?? 1f) * (isBreaking ? 1.5f : 1f);

        // 假如正好防住了
        if (isDefending && defendingLocation == location)
        {
            if (attacker.Weapon != null)
            {
                attacker.Weapon.Hp -= damage;
                if (attacker.Weapon.Hp <= 0)
                {
                    attacker.SetUnitEquipment(null, EquipmentType.Weapon);
                }

                Debug.Log($"unit的攻击被防住了！武器耐久减少:{damage}");
            }
            return;
        }

        var equiptment = GetEquipmentByLocation(location);
        var def = equiptment?.DefencePercent ?? 0f;

        var armDamage = damage * Mathf.Clamp01(1 - def / 100f);
        var hpDamage = damage - armDamage;

        if (equiptment != null)
        {
            // 记录一下减去伤害前的HP，因为有可能会超过装备耐久
            var tempHp = equiptment.Hp;

            equiptment.Hp -= armDamage;
            if (equiptment.Hp <= 0)
            {
                SetUnitEquipment(null, location);

                // 设置破甲状态
                SetBreaking();

                Hp -= armDamage - tempHp;
            }
        }
        else
        {
            Hp -= armDamage;
        }

        Hp -= hpDamage;

        Debug.Log($"当前Unit：{this.GetType().Name} 当前血量：{this.Hp} 受损装备:{location} 装备剩余耐久:{equiptment?.Hp ?? 0f}");
    }

    // 判断一个gameObject是否从属与这个unit
    public bool IsFromThisGO(GameObject gameObject)
    {
        if (Root != null)
        {
            var cur = gameObject.transform;
            while (cur.parent != null)
            {
                cur = cur.parent;
                if (cur.gameObject == Root)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public Equipment GetEquipmentByLocation(EquipmentType location)
    {
        switch (location)
        {
            case EquipmentType.LeftHand:
                return LeftHand;
            case EquipmentType.RightHand:
                return RightHand;
            case EquipmentType.LeftFoot:
                return LeftFoot;
            case EquipmentType.RightFoot:
                return RightFoot;
            case EquipmentType.Breast:
                return Breast;
            case EquipmentType.Weapon:
                return Weapon;
        }

        return null;
    }

    public void SetUnitEquipment(Equipment equipment, EquipmentType location)
    {
        if (equipment != null && equipment.type != location)
        {
            Debug.LogError($"错误，设置的装备不是对应的槽位 {equipment.type} -> {location}");
            return;
        }

        switch (location)
        {
            case EquipmentType.LeftHand:
                this.LeftHand = equipment;
                break;
            case EquipmentType.RightHand:
                this.RightHand = equipment;
                break;
            case EquipmentType.LeftFoot:
                this.LeftFoot = equipment;
                break;
            case EquipmentType.RightFoot:
                this.RightFoot = equipment;
                break;
            case EquipmentType.Breast:
                this.Breast = equipment;
                break;
            case EquipmentType.Weapon:
                this.Weapon = equipment;
                break;
        }
    }

    public bool HasUnequipedLocation(out EquipmentType location)
    {
        if (this.LeftHand == null)
        {
            location = EquipmentType.LeftHand;
            return true;
        }

        if (this.RightHand == null)
        {
            location = EquipmentType.RightHand;
            return true;
        }

        if (this.LeftFoot == null)
        {
            location = EquipmentType.LeftFoot;
            return true;
        }

        if (this.RightFoot == null)
        {
            location = EquipmentType.RightFoot;
            return true;
        }

        if (this.Breast == null)
        {
            location = EquipmentType.Breast;
            return true;
        }

        location = EquipmentType.NULL;
        return false;
    }
}
