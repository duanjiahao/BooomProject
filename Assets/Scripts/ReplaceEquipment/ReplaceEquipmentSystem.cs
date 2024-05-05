using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class ReplaceEquipmentSystem : MonoBehaviour
{
    /// <summary>
    /// 根据给定的物体返回他身上的reslovers
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public static List<SpriteResolver> ReturnSpriteResolvers(GameObject unit)
    {
        List<SpriteResolver> spriteResolvers = new List<SpriteResolver>();
        foreach (var spriteResolver in unit.transform.GetChild(0).GetComponentsInChildren<SpriteResolver>())
        {
            spriteResolvers.Add(spriteResolver);
        }
        return spriteResolvers;
    }

    //用例：刷新某个角色身上的装备显示：如怪物：RefleshSomeoneEquipment(怪物身上的equipmentSystem,ReturnSpriteResolvers(怪物gameobject));

    /// <summary>
    /// 刷新全身装备
    /// </summary>
    public static void RefleshSomeoneEquipment(EquipmentSystem system, List<SpriteResolver> spriteResolvers)
    {
        foreach (var item in spriteResolvers)
        {
            switch (item.GetCategory())
            {
                case "Head"://头
                    item.SetCategoryAndLabel("Head", system.Head?.config.armorIcon);
                    break;

                case "Body"://身体
                    item.SetCategoryAndLabel("Body", system.Breast?.config.armorIcon);
                    break;
                case "R_Shoulder"://右肩
                    item.SetCategoryAndLabel("R_Shoulder", system.Breast?.config.armorIcon);
                    break;
                case "L_Shoulder"://左肩
                    item.SetCategoryAndLabel("L_Shoulder", system.Breast?.config.armorIcon);
                    break;
                case "L_Arm_1"://左大臂
                    item.SetCategoryAndLabel("L_Arm_1", system.Breast?.config.armorIcon);
                    break;
                case "R_Arm_1"://右大臂
                    item.SetCategoryAndLabel("R_Arm_1", system.Breast?.config.armorIcon);
                    break;

                case "L_Arm_2"://左小臂
                    item.SetCategoryAndLabel("L_Arm_2", system.LeftHand?.config.armorIcon);
                    break;

                case "R_Arm_2"://右小臂
                    item.SetCategoryAndLabel("R_Arm_2", system.RightHand?.config.armorIcon);
                    break;

                case "Weapen"://武器
                    item.SetCategoryAndLabel("Weapen", system.Weapon?.config.weapomIcon);
                    break;

                case "L_Leg_2"://左小腿
                    item.SetCategoryAndLabel("L_Leg_2", system.Leg?.config.armorIcon);
                    break;
                case "L_Foot"://左脚
                    item.SetCategoryAndLabel("L_Foot", system.Leg?.config.armorIcon);
                    break;
                case "R_Leg_2"://右小腿
                    item.SetCategoryAndLabel("R_Leg_2", system.Leg?.config.armorIcon);
                    break;
                case "R_Foot"://右脚
                    item.SetCategoryAndLabel("R_Foot", system.Leg?.config.armorIcon);
                    break;

                default:
                    break;
            }
        }
    }

}
