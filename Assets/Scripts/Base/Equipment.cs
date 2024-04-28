// 装备类型
public enum EquipmentType 
{
    NULL = 0,       // -1
    LeftHand = 1,   // 左臂
    RightHand = 2,  // 右臂
    Legs = 3,       // 下肢
    Head = 4,       // 头
    Breast = 5,     // 胸甲
    Weapon = 6,     // 武器
}
public class Equipment
{
    internal EquipmentSO SO;

    // 装备类型
    public EquipmentType type => SO.type;

    // 防御装备的减伤百分比
    public float DefencePercent => SO.DefencePercent;

    //装备耐久上限
    public float MaxHp => SO.MaxHp;

    // 武器的攻击次数
    public int Turns => SO.Turns;

    // 武器的最小伤害
    public float MinDamage => SO.MinDamage;

    // 武器的最大伤害
    public float MaxDamage => SO.MaxDamage;

    public float Hp
    {
        get { return SO.Hp.Value; }
        set 
        { 
            SO.Hp.Value = UnityEngine.Mathf.Max(value, 0f); 
        }
    }

    public Equipment(EquipmentSO SO) 
    {
        this.SO = SO;
        SO.Hp.Value = SO.MaxHp.Value;
    }
}
