using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentRarity
{
    Rare1 = 1,
    Rare2 = 2,
    Rare3 = 3,
    Rare4 = 4,
}

public abstract class Unit
{
    public float Hp { get; protected set; }

    public float MaxHp { get; protected set; }
    
    public int Strength { get; protected set; }
    
    public int Ductility { get; protected set; }       //体质

    public int Dexterity { get; protected set; }       //灵巧
    
    public int Agility { get; protected set; }        // 敏捷

    public EquipmentSystem equipmentSystem;

    // 是否在防御
    public bool isDefending;

    // 正在防御的位置
    public EquipmentLocation defendingLocation;

    // 是否破甲
    public bool isBreaking;

    protected GameObject Root;

    protected Animator _animator;
    
    private static readonly int Defence = Animator.StringToHash("Defence");
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    private static readonly int GetHit = Animator.StringToHash("GetHit");

    // 生成对应的显示层
    public virtual void GenerateGameObject(int id)
    {
    }

    public virtual void Dispose() 
    {
        GameObject.Destroy(Root);
    }

    public bool Defend(EquipmentLocation location)
    {
        _animator.SetBool(Defence, true);
        
        isDefending = true;
        defendingLocation = location;

        if (!_animator.IsInTransition(0))
        {
            return true;
        }

        return false;
    }

    //  每次行动前解除防御
    public void StopDefending()
    {
        _animator.SetBool(Defence, false);
        
        isDefending = false;
        defendingLocation = EquipmentLocation.NULL;
    }
    public virtual void SetBreaking()
    {
        isBreaking = true;
        
    }

    //  每次行动前解除防御
    public virtual void StopBreaking()
    {
        isBreaking = false;
    }

    // 攻击别人
    public virtual bool Attack(Unit target, EquipmentLocation location)
    {
        if (!_hasPreAttacked)
        {
            PreAttack();
            return false;
        }

        var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        // 攻击动画
        if (_animator.IsInTransition(0) || stateInfo.IsName("Attack"))
        {
            return false;
        }

        if (!target.BeAttacked(this, location))
        {
            return false;
        }

        _hasPreAttacked = false;
        return true;
    }

    private bool _hasPreAttacked;
    public virtual void PreAttack()
    {
        _animator.SetTrigger(Attack1);
        _hasPreAttacked = true;
    }

    private bool _hasPreBeHitted;
    public virtual void PreBeHit()
    {
        _animator.SetTrigger(GetHit);
        _hasPreBeHitted = true;
    }

    //被别人攻击
    public virtual bool BeAttacked(Unit attacker, EquipmentLocation location)
    {
        if (CommonUtils.Roll(Agility))
        {
            // 闪避
            return true;
        }
        
        var weaponAttack = Random.Range(attacker.equipmentSystem.Weapon?.config.weapomAttack[0] ?? 1f,
            attacker.equipmentSystem.Weapon?.config.weapomAttack[1] ?? 1f);
        
        // 假如正好防住了
        if (isDefending && defendingLocation == location)
        {
            if (attacker.equipmentSystem.Weapon != null)
            {
                attacker.equipmentSystem.Weapon.Hp -= weaponAttack;
                if (attacker.equipmentSystem.Weapon.Hp <= 0)
                {
                    attacker.SetUnitEquipment(EquipmentLocation.Weapon, null, null);
                }

                Debug.Log($"unit的攻击被防住了！武器耐久减少:{weaponAttack}");
            }
            
            if (equipmentSystem.Weapon != null)
            {
                equipmentSystem.Weapon.Hp -= weaponAttack;
                if (equipmentSystem.Weapon.Hp <= 0)
                {
                    SetUnitEquipment(EquipmentLocation.Weapon, null, null);
                }
            }
            return true;
        }

        if (!isDefending)
        {
            // 播放受击动画
            if (!_hasPreBeHitted)
            {
                PreBeHit();
                return false;
            }
        
            var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (_animator.IsInTransition(0) || stateInfo.IsName("GetHit"))
            {
                return false;
            }

            _hasPreBeHitted = false;
        }
        
        var criticalRate = 1f;
        if (CommonUtils.Roll(Dexterity))
        {
            criticalRate = 1.5f;
        }

        var strengthRate = 1 + Strength / 100f;

        // 计算血量减少，对应护甲耐久减少
        var damage = weaponAttack * (isBreaking ? 1.5f : 1f) * criticalRate * strengthRate;

        var equiptment = GetEquipmentByLocation(location);
        var def = equiptment?.config.armorValue ?? 0f;

        var armDamage = damage * Mathf.Clamp01(1 - def);
        var hpDamage = (damage - armDamage) * (1 - Ductility / 100f);

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
        return true;
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

    public void SetLocationColor(EquipmentLocation location, Color color)
    {
        var GOs = CommonUtils.FindChildrenWithTag(Root, CommonUtils.GetTagByLocation(location));

        for (int i = 0; i < GOs.Count; i++)
        {
            var go = GOs[i];
            go.GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void SetAllLocationColor(Color color)
    {
        for (int i = 0; i < (int)EquipmentLocation.Count; i++)
        {
            SetLocationColor((EquipmentLocation)i, color);
        }
    }
}
