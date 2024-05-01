using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit
{
    public float Hp { get; protected set; }

    public float MaxHp { get; protected set; }

    public EquipmentSystem equipmentSystem;

    // 是否在防御
    public bool isDefending;

    // 正在防御的位置
    public EquipmentLocation defendingLocation;

    // 是否破甲
    public bool isBreaking;

    protected GameObject Root;

    protected GameObject RootUI;

    // 生成对应的显示层
    public virtual void GenerateGameObject(int id)
    {
    }

    public void Defend(EquipmentLocation location)
    {
        isDefending = true;
        defendingLocation = location;
    }

    //  每次行动前解除防御
    public void StopDefending()
    {
        isDefending = false;
        defendingLocation = EquipmentLocation.NULL;
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
    public virtual void Attack(Unit target, EquipmentLocation location)
    {
        target.BeAttacked(this, location);
    }

    //被别人攻击
    public virtual void BeAttacked(Unit attacker, EquipmentLocation location)
    {
        // 播放受击动画

        // 计算血量减少，对应护甲耐久减少
        var damage = Random.Range(attacker.equipmentSystem.Weapon?.config.weapomAttack[0] ?? 1f, attacker.equipmentSystem.Weapon?.config.weapomAttack[1] ?? 1f) * (isBreaking ? 1.5f : 1f);

        // 假如正好防住了
        if (isDefending && defendingLocation == location)
        {
            if (attacker.equipmentSystem.Weapon != null)
            {
                attacker.equipmentSystem.Weapon.Hp -= damage;
                if (attacker.equipmentSystem.Weapon.Hp <= 0)
                {
                    attacker.SetUnitEquipment(EquipmentLocation.Weapon, null, null);
                }

                Debug.Log($"unit的攻击被防住了！武器耐久减少:{damage}");
            }
            return;
        }

        var equiptment = GetEquipmentByLocation(location);
        var def = equiptment?.config.armorValue ?? 0f;

        var armDamage = damage * Mathf.Clamp01(1 - def);
        var hpDamage = damage - armDamage;

        if (equiptment != null)
        {
            // 记录一下减去伤害前的HP，因为有可能会超过装备耐久
            var tempHp = equiptment.Hp;

            equiptment.Hp -= armDamage;
            if (equiptment.Hp <= 0)
            {
                SetUnitEquipment(location, null, null);

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

    public Equipment GetEquipmentByLocation(EquipmentLocation location)
    {
        return equipmentSystem.GetEquipmentByLocation(location);
    }

    public Weapon GetWeapon() 
    {
        return equipmentSystem.Weapon;
    }

    public void SetUnitEquipment(EquipmentLocation location, Equipment equipment, Weapon weapon)
    {
        equipmentSystem.SetEquipment(location, equipment, weapon);
    }

    public bool HasUnequipedLocation(out EquipmentLocation location)
    {
        if (this.equipmentSystem.LeftHand == null)
        {
            location = EquipmentLocation.LeftHand;
            return true;
        }

        if (this.equipmentSystem.RightHand == null)
        {
            location = EquipmentLocation.RightHand;
            return true;
        }

        if (this.equipmentSystem.Leg == null)
        {
            location = EquipmentLocation.Leg;
            return true;
        }

        if (this.equipmentSystem.Head == null)
        {
            location = EquipmentLocation.Head;
            return true;
        }

        if (this.equipmentSystem.Breast == null)
        {
            location = EquipmentLocation.Breast;
            return true;
        }

        location = EquipmentLocation.NULL;
        return false;
    }
}
