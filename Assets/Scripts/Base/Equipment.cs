// 装备类型
public enum EquipmentType 
{
    NULL = 0,       // -1
    LeftHand = 1,   // 左臂
    RightHand = 2,  // 右臂
    LeftFoot = 3,   // 左脚
    RightFoot = 4,  // 右脚
    Breast = 5,     // 胸甲
    Weapon = 6,     // 武器
}
public class Equipment
{
    // 装备类型
    public EquipmentType type;

    // 防御装备的减伤百分比
    public float DefencePercent;

    // 装备的耐久
    public float Hp;

    //装备耐久上限
    public float MaxHp;

    // 武器的攻击次数
    public int Turns;

    // 武器的最小伤害
    public float MinDamage;

    // 武器的最大伤害
    public float MaxDamage;


}
