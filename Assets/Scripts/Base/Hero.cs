using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    // 玩家的行动次数
    public int Turns 
    { 
        get { return Weapon?.Turns ?? 1; }
    }

    public override void GenerateGameObject()
    {
        Hp = 100;
        MaxHp = 100;
        Root = GameObject.Instantiate(Resources.Load<GameObject>("Hero"));
        Root.transform.position = new Vector3(-5f, 0f, 0f);
        Root.transform.localScale = Vector3.one;

        Weapon = new Equipment()
        {
            type = EquipmentType.Weapon,
            Hp = 100,
            Turns = 3,
            MinDamage = 5,
            MaxDamage = 8,
        };

        leftFoot = new Equipment()
        {
            type = EquipmentType.LeftFoot,
            DefencePercent = 50f,
            Hp = 100,
        };

        RightFoot = new Equipment()
        {
            type = EquipmentType.RightFoot,
            DefencePercent = 50f,
            Hp = 100,
        };

        LeftHand = new Equipment()
        {
            type = EquipmentType.LeftHand,
            DefencePercent = 50f,
            Hp = 100,
        };

        rightHand = new Equipment()
        {
            type = EquipmentType.RightHand,
            DefencePercent = 50f,
            Hp = 100,
        };

        Breast = new Equipment()
        {
            type = EquipmentType.Breast,
            DefencePercent = 80f,
            Hp = 200,
        };
    }
}
