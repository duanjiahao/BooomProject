using System;
using System.Collections.Generic;

public enum WeaponType 
{
    NULL = 0,
    Sword = 1,    // 剑
    Hammer = 2,   // 锤子
    Dagon = 3,    // 匕首
}

public class Weapon
{
    public WeaponConfig config;

    public WeaponType weaponType;

    public float Hp { get; set; }
    public float maxHp { get; set; }

    public Weapon(int id) 
    {
        config = ConfigManager.Instance.GetConfig<WeaponConfig>(id);
        weaponType = (WeaponType)config.weapomType;
        Hp = config.weapomDurable;
    }
}
