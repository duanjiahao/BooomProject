using UnityEngine;

[CreateAssetMenu(fileName = "Equipment SO", menuName = "SO/Create Equipment")]
public class EquipmentSO : ScriptableObject
{
    // 装备类型
    public EquipmentType type;

    // 防御装备的减伤百分比
    public FloatReference DefencePercent;

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
}
