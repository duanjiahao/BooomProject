using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapen SO", menuName = "SO/Create Wenpen")]
public class WeapenSO : ScriptableObject
{
    // 装备类型
    public EquipmentType type;

    //伤害
    public FloatReference damage;

    // 装备的耐久
    public FloatVariable Hp;

    //装备耐久上限
    public FloatReference MaxHp;

    // 武器的攻击次数
    public IntReference Turns;

    // 武器的最小伤害
    public FloatReference MinDamage;

    // 武器的最大伤害
    public FloatReference MaxDamage;

    //价值
    public IntReference value;
}
