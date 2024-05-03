// 装备类型 (与策划的Armor表的armorType字段定义对应)

using UnityEngine;
using UnityEngine.UIElements;

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

    private float _hp;
    public float Hp {
        get
        {
            return _hp;
        }
        set
        {
            _hp = Mathf.Max(value, 0);
        }
    } // 装备耐久
    public float maxHp { get; set; }//装备最大耐久

    public Equipment(int id) 
    {
        config = ConfigManager.Instance.GetConfig<ArmorConfig>(id);
        equipmentType = (EquipmentType)config.armorType;
        Hp = config.armorDurable;
    }
}
