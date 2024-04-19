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
    public Intension CurrentIntension { get; set; }

    public override void GenerateGameObject()
    {
        Hp = 100;
        Root = GameObject.Instantiate(Resources.Load<GameObject>("Monster"));
        Root.transform.position = new Vector3(5f, 0f, 0f);
        Root.transform.localScale = new Vector3(-1, 1, 1);

        Weapon = new Equipment()
        {
            type = EquipmentType.Weapon,
            Hp = 50,
            Turns = 3, // Turns对于怪物无效
            MinDamage = 2,
            MaxDamage = 4,
        };

        leftFoot = new Equipment()
        {
            type = EquipmentType.LeftFoot,
            DefencePercent = 50f,
            Hp = 50,
            MaxHp = 50,
        };
        GameObject MonsterleftFootUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        MonsterleftFootUI.GetComponent<EquipmentUI>().equipment = leftFoot;

        RightFoot = new Equipment()
        {
            type = EquipmentType.RightFoot,
            DefencePercent = 50f,
            Hp = 50,
            MaxHp = 50,
        };
        GameObject MonsterRightFootUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        MonsterRightFootUI.GetComponent<EquipmentUI>().equipment = RightFoot;

        LeftHand = new Equipment()
        {
            type = EquipmentType.LeftHand,
            DefencePercent = 50f,
            Hp = 50,
            MaxHp = 50,
        };
        GameObject MonsterLeftHandUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        MonsterLeftHandUI.GetComponent<EquipmentUI>().equipment = LeftHand;

        rightHand = new Equipment()
        {
            type = EquipmentType.RightHand,
            DefencePercent = 50f,
            Hp = 50,
            MaxHp = 50,
        };
        GameObject MonsterrightHandUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        MonsterrightHandUI.GetComponent<EquipmentUI>().equipment = rightHand;


        Breast = new Equipment()
        {
            type = EquipmentType.Breast,
            DefencePercent = 80f,
            Hp = 100,
            MaxHp = 100,
        };
        GameObject MonsterBreastUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        MonsterBreastUI.GetComponent<EquipmentUI>().equipment = Breast;

        CurrentIntension = new Intension()
        {
            AttackOrDefence = 1,
            location = (EquipmentType)Random.Range(1, 5),
        };

        //UI实例化
        RootUI = GameObject.Instantiate(Resources.Load<GameObject>("MonsterUI_Canvas"));
        RootUI.GetComponent<MonsterUI>()._currentUnit = this;
    }
}
