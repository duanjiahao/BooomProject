using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit
{
    public float Hp;

    public Equipment LeftHand;
    public Equipment rightHand;
    public Equipment leftFoot;
    public Equipment RightFoot;
    public Equipment Breast;
    public Equipment Weapon;

    protected GameObject Root;

    // 生成对应的显示层
    public virtual void GenerateGameObject()
    {

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
}
