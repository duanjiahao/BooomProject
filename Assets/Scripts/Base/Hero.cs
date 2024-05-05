using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    // 玩家的行动次数
    public int Turns
    {
        get { return equipmentSystem.Weapon?.config.actionPoint ?? 1; }
    }

    public override void GenerateGameObject(int id)
    {
        Root = GameObject.Instantiate(Resources.Load<GameObject>("Hero"));
        base.GenerateGameObject(id);

        Root.transform.position = new Vector3(-4f, -1f, 0f);
        Root.transform.localScale = Vector3.one;

        MaxHp = PlayerData.Instance.MaxHP;
        Hp = PlayerData.Instance.CurrentHP;

        Strength = PlayerData.Instance.Strength;
        Agility = PlayerData.Instance.Agility;
        Dexterity = PlayerData.Instance.Dexterity;
        Ductility = PlayerData.Instance.Ductility;

        equipmentSystem = new EquipmentSystem(PlayerData.Instance.equipmentSystem);

        _animator = Root.GetComponentInChildren<Animator>();

        //GameObject HeroWeaponUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("EquipmentDurability_Panel").transform);
        //HeroWeaponUI.GetComponent<EquipmentUI>().equipment = Weapon;
        //GameObject HeroHeadUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        //HeroHeadUI.GetComponent<EquipmentUI>().equipmentSO = Head.SO;
        //GameObject HeroLegsUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        //HeroLegsUI.GetComponent<EquipmentUI>().equipmentSO = Legs.SO;
        //GameObject HeroLeftHandUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        //HeroLeftHandUI.GetComponent<EquipmentUI>().equipmentSO = LeftHand.SO;
        //GameObject HerorightHandUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        //HerorightHandUI.GetComponent<EquipmentUI>().equipmentSO = RightHand.SO;
        //GameObject HeroBreastUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("HeroEquipmentDurability_Panel").transform);
        //HeroBreastUI.GetComponent<EquipmentUI>().equipmentSO = Breast.SO;
        ////UI实例化
        //RootUI = GameObject.Instantiate(Resources.Load<GameObject>("HeroUI_Canvas"),GameObject.Find("CombatUI").transform);
    }
}
