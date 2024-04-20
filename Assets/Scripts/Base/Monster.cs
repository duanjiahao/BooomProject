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
        Root = GameObject.Instantiate(Resources.Load<GameObject>("Monster"));
        base.GenerateGameObject();

        Root.transform.position = new Vector3(5f, 0f, 0f);
        Root.transform.localScale = new Vector3(-1, 1, 1);

        GameObject MonsterleftFootUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        MonsterleftFootUI.GetComponent<EquipmentUI>().equipment = LeftFoot;
        GameObject MonsterRightFootUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        MonsterRightFootUI.GetComponent<EquipmentUI>().equipment = RightFoot;
        GameObject MonsterLeftHandUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        MonsterLeftHandUI.GetComponent<EquipmentUI>().equipment = LeftHand;
        GameObject MonsterrightHandUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        MonsterrightHandUI.GetComponent<EquipmentUI>().equipment = RightHand;
        GameObject MonsterBreastUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        MonsterBreastUI.GetComponent<EquipmentUI>().equipment = Breast;
        //UI实例化
        RootUI = GameObject.Instantiate(Resources.Load<GameObject>("MonsterUI_Canvas"));

        CurrentIntension = new Intension()
        {
            AttackOrDefence = 1,
            location = (EquipmentType)Random.Range(1, 5),
        };
    }
}
