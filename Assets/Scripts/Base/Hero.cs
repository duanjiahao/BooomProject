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

    public int leftTurn;
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
        //GameObject HeroWeaponUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("EquipmentDurability_Panel").transform);
        //HeroWeaponUI.GetComponent<EquipmentUI>().equipment = Weapon;

        leftFoot = new Equipment()
        {
            type = EquipmentType.LeftFoot,
            DefencePercent = 50f,
            Hp = 100,
            MaxHp = 100,
        };
        GameObject HeroleftFootUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        HeroleftFootUI.GetComponent<EquipmentUI>().equipment = leftFoot;

        RightFoot = new Equipment()
        {
            type = EquipmentType.RightFoot,
            DefencePercent = 50f,
            Hp = 100,
            MaxHp = 100,
        };
        GameObject HeroRightFootUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        HeroRightFootUI.GetComponent<EquipmentUI>().equipment = RightFoot;

        LeftHand = new Equipment()
        {
            type = EquipmentType.LeftHand,
            DefencePercent = 50f,
            Hp = 100,
            MaxHp = 100,
        };
        GameObject HeroLeftHandUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        HeroLeftHandUI.GetComponent<EquipmentUI>().equipment = LeftHand;

        rightHand = new Equipment()
        {
            type = EquipmentType.RightHand,
            DefencePercent = 50f,
            Hp = 100,
            MaxHp = 100,
        };
        GameObject HerorightHandUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        HerorightHandUI.GetComponent<EquipmentUI>().equipment = rightHand;

        Breast = new Equipment()
        {
            type = EquipmentType.Breast,
            DefencePercent = 80f,
            Hp = 200,
            MaxHp = 200,
        };
        GameObject HeroBreastUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        HeroBreastUI.GetComponent<EquipmentUI>().equipment = Breast;

        //UI实例化
        RootUI = GameObject.Instantiate(Resources.Load<GameObject>("HeroUI_Canvas"));
        RootUI.GetComponent<HeroUI>()._currentUnit = this;
        RootUI.GetComponent<HeroUI>()._currentHero = this;
    }
}
