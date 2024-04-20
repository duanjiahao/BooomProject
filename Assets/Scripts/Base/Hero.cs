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
        Root = GameObject.Instantiate(Resources.Load<GameObject>("Hero"));
        base.GenerateGameObject();

        Root.transform.position = new Vector3(-5f, 0f, 0f);
        Root.transform.localScale = Vector3.one;

        //GameObject HeroWeaponUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("EquipmentDurability_Panel").transform);
        //HeroWeaponUI.GetComponent<EquipmentUI>().equipment = Weapon;
        GameObject HeroleftFootUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        HeroleftFootUI.GetComponent<EquipmentUI>().equipment = LeftFoot;
        GameObject HeroRightFootUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        HeroRightFootUI.GetComponent<EquipmentUI>().equipment = RightFoot;
        GameObject HeroLeftHandUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        HeroLeftHandUI.GetComponent<EquipmentUI>().equipment = LeftHand;
        GameObject HerorightHandUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        HerorightHandUI.GetComponent<EquipmentUI>().equipment = RightHand;
        GameObject HeroBreastUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        HeroBreastUI.GetComponent<EquipmentUI>().equipment = Breast;
        //UI实例化
        RootUI = GameObject.Instantiate(Resources.Load<GameObject>("HeroUI_Canvas"));
    }
}
