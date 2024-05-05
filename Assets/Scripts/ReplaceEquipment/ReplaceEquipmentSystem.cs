using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ReplaceEquipmentSystem : MonoBehaviour
{
    public List<SpriteResolver> spriteResolvers = new List<SpriteResolver>();

    void Start()
    {
        foreach (var spriteResolver in FindObjectsOfType<SpriteResolver>())
        {
            spriteResolvers.Add(spriteResolver);
        }
    }

    /// <summary>
    /// 刷新全身装备
    /// </summary>
    public void RefleshEquipment()
    {
        PlayerData playerData = PlayerData.Instance;
        foreach (var item in spriteResolvers)
        {
            switch (item.GetCategory())
            {
                case "Head"://头
                    item.SetCategoryAndLabel("Head",playerData.equipmentSystem.Head.config.armorIcon);
                    break;

                case "Body"://身体
                    item.SetCategoryAndLabel("Body", playerData.equipmentSystem.Breast.config.armorIcon);
                    break;
                case "R_Shoulder"://右肩
                    item.SetCategoryAndLabel("R_Shoulder", playerData.equipmentSystem.Breast.config.armorIcon);
                    break;
                case "L_Shoulder"://左肩
                    item.SetCategoryAndLabel("L_Shoulder", playerData.equipmentSystem.Breast.config.armorIcon);
                    break;
                case "L_Arm_1"://左大臂
                    item.SetCategoryAndLabel("L_Arm_1", playerData.equipmentSystem.Breast.config.armorIcon);
                    break;
                case "R_Arm_1"://右大臂
                    item.SetCategoryAndLabel("R_Arm_1", playerData.equipmentSystem.Breast.config.armorIcon);
                    break;

                case "L_Arm_2"://左小臂
                    item.SetCategoryAndLabel("L_Arm_2", playerData.equipmentSystem.LeftHand.config.armorIcon);
                    break;

                case "R_Arm_2"://右小臂
                    item.SetCategoryAndLabel("R_Arm_2", playerData.equipmentSystem.RightHand.config.armorIcon);
                    break;

                case "Weapen"://武器
                    item.SetCategoryAndLabel("Weapen", playerData.equipmentSystem.Weapon.config.weapomIcon);
                    break;

                case "L_Leg_2"://左小腿
                    item.SetCategoryAndLabel("L_Leg_2", playerData.equipmentSystem.Leg.config.armorIcon);
                    break;
                case "L_Foot"://左脚
                    item.SetCategoryAndLabel("L_Foot", playerData.equipmentSystem.Leg.config.armorIcon);
                    break;
                case "R_Leg_2"://右小腿
                    item.SetCategoryAndLabel("R_Leg_2", playerData.equipmentSystem.Leg.config.armorIcon);
                    break;
                case "R_Foot"://右脚
                    item.SetCategoryAndLabel("R_Foot", playerData.equipmentSystem.Leg.config.armorIcon);
                    break;

                default:
                    break;
            }
        }
    }

}
