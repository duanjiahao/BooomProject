using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 怪物的意图
public class Intension 
{
    // 目标位置（目前不可以是武器）
    public EquipmentType location;

    // 是防御还是攻击：0防御，1攻击
    public int AttackOrDefence;
}

public class Monster : Unit
{
    // 怪物当前的意图
    public Intension CurrentIntension { get; private set; }

    // 怪物是否在防御
    public bool isDefending;

    // 怪物正在防御的位置
    public EquipmentType defendingLocation;
}
