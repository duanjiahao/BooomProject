// 装备类型 (与策划的Armor表的armorType字段定义对应)
public enum EquipmentType 
{
    NULL = 0,       
    Head = 1,   // 头甲
    Breast = 2, // 胸甲
    Hand = 3,   // 手甲（不分左右手）
    Leg = 4,    // 腿甲
    Count = 5,  // 装备计数，用来做随机、遍历等操作
}

public class Equipment
{
    public ArmorConfig config;

    public EquipmentType equipmentType;
    public float Hp { get; set; } // 装备耐久

    public Equipment(int id) 
    {
        config = ConfigManager.Instance.GetConfig<ArmorConfig>(id);
        equipmentType = (EquipmentType)config.armorType;
        Hp = config.armorDurable;
    }
}
