using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : SingleMono<PlayerData>
{
    public UnitAttributeSO playerSO;

    public float maxHP;//最大血量
    public float currentHP;//当前血量

    public int goldNum;//金钱数量

    //public WeapenSO weapenSO;//武器数据  todo:预想把武器和装备的SO分开
    public EquipmentSO weapenSO;//武器数据
    public EquipmentSO headSO;//头部装备数据
    public EquipmentSO leftHandSO;//左手装备数据
    public EquipmentSO rightHandSO;//右手装备数据
    public EquipmentSO breastSO;//胸部装备数据
    public EquipmentSO legsSO;//下肢装备数据

    private void Awake()
    {
        
        maxHP = playerSO.MaxHp.Value;
        currentHP = playerSO.Hp.Value;

        //取数据
        weapenSO = playerSO.Weapon;
        headSO = playerSO.Head;
        leftHandSO = playerSO.LeftHand;
        rightHandSO = playerSO.RightHand;
        breastSO = playerSO.Breast;
        legsSO = playerSO.Legs;
    }
}
