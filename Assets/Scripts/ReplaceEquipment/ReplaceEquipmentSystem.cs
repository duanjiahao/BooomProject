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
    /// ˢ��ȫ��װ��
    /// </summary>
    public void RefleshEquipment()
    {
        PlayerData playerData = PlayerData.Instance;
        foreach (var item in spriteResolvers)
        {
            switch (item.GetCategory())
            {
                case "Head"://ͷ
                    item.SetCategoryAndLabel("Head",playerData.equipmentSystem.Head.config.armorIcon);
                    break;

                case "Body"://����
                    item.SetCategoryAndLabel("Body", playerData.equipmentSystem.Breast.config.armorIcon);
                    break;
                case "R_Shoulder"://�Ҽ�
                    item.SetCategoryAndLabel("R_Shoulder", playerData.equipmentSystem.Breast.config.armorIcon);
                    break;
                case "L_Shoulder"://���
                    item.SetCategoryAndLabel("L_Shoulder", playerData.equipmentSystem.Breast.config.armorIcon);
                    break;
                case "L_Arm_1"://����
                    item.SetCategoryAndLabel("L_Arm_1", playerData.equipmentSystem.Breast.config.armorIcon);
                    break;
                case "R_Arm_1"://�Ҵ��
                    item.SetCategoryAndLabel("R_Arm_1", playerData.equipmentSystem.Breast.config.armorIcon);
                    break;

                case "L_Arm_2"://��С��
                    item.SetCategoryAndLabel("L_Arm_2", playerData.equipmentSystem.LeftHand.config.armorIcon);
                    break;

                case "R_Arm_2"://��С��
                    item.SetCategoryAndLabel("R_Arm_2", playerData.equipmentSystem.RightHand.config.armorIcon);
                    break;

                case "Weapen"://����
                    item.SetCategoryAndLabel("Weapen", playerData.equipmentSystem.Weapon.config.weaponIcon);
                    break;

                case "L_Leg_2"://��С��
                    item.SetCategoryAndLabel("L_Leg_2", playerData.equipmentSystem.Leg.config.armorIcon);
                    break;
                case "L_Foot"://���
                    item.SetCategoryAndLabel("L_Foot", playerData.equipmentSystem.Leg.config.armorIcon);
                    break;
                case "R_Leg_2"://��С��
                    item.SetCategoryAndLabel("R_Leg_2", playerData.equipmentSystem.Leg.config.armorIcon);
                    break;
                case "R_Foot"://�ҽ�
                    item.SetCategoryAndLabel("R_Foot", playerData.equipmentSystem.Leg.config.armorIcon);
                    break;

                default:
                    break;
            }
        }
    }

}
