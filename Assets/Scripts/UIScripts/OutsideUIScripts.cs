using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideUIScripts : MonoBehaviour
{
    private UnitAttributeSO playerAttributeSO;

    public string health_Num;
    public string gold_Num;


    //装备耐久
    public string equipmentDurability_weapen;
    public string equipmentDurability_head;
    public string equipmentDurability_lefthand;
    public string equipmentDurability_righthand;
    public string equipmentDurability_breast;
    public string equipmentDurability_legs;


    private void Start()
    {
        Init();
        print(health_Num);
    }

    public void Init()
    {
        //health_Num = playerAttributeSO.Hp.ToString("0") + "/" + player.MaxHp.ToString("0");
    }
}
