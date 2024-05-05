using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    JumpNextEvent = 1,
    GetItem = 2,
    HeadRecDamage = 3,
    BreastRecDamage = 4,
    LeftHandRecDamage = 5,
    RightHandRecDamage = 6,
    LegRecDamage = 7,
    AllRecDamage = 8,
    WeaponHpDown = 9,
    HpDown = 10,
    HeadHpDown = 11,
    BreastHpDown = 12,
    LeftHandHpDown = 13,
    RightHandHpDown = 14,
    LegHpDown = 15,
    AllHpDown = 16,
    MaxHpDown = 17,
    StrengthDown = 18,
    GoldChange = 22,
}

public static class CommonUtils
{
    public static void DestroyAllChildren(this GameObject gameObject) 
    {
        var childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var child = gameObject.transform.GetChild(i);
            GameObject.Destroy(child.gameObject);
        }
    }

    // 随机一个结果
    public static bool Roll(float chance) 
    {
        var random = Random.Range(0f, 100f);

        return chance > random;
    }

    public static SlotDirection GetInverseDirection(SlotDirection dir) 
    {
        switch (dir)
        {
            case SlotDirection.None:
                return SlotDirection.None;
            case SlotDirection.Left:
                return SlotDirection.Right;
            case SlotDirection.Right:
                return SlotDirection.Left;
            case SlotDirection.Up:
                return SlotDirection.Down;
            case SlotDirection.Down:
                return SlotDirection.Up;
            default:
                return dir;
        }
    }

    public static List<int> RollRange(List<int> weights, int rollTime = 1, bool remove = true)
    {
        List<int> result = new List<int>();

        float amount = 0;
        foreach (var weight in weights)
        {
            amount += weight;
        }

        for (int i = 0; i < rollTime; i++)
        {
            var roll = Random.Range(0, amount);

            for (int j = 0; j < weights.Count; j++)
            {
                if (remove && result.Contains(j)) continue;

                var weight = weights[j];
                if (weight >= roll)
                {
                    result.Add(j);

                    if (remove) 
                    {
                        amount -= weight;
                    }
                    break;
                }

                roll -= weight;
            }
        }

        return result;
    }

    public static bool CheckEquipmentTypeCorrespond(EquipmentLocation location, EquipmentType equipmentType)
    {
        return (equipmentType == EquipmentType.Head && location == EquipmentLocation.Head) ||
               (equipmentType == EquipmentType.Hand && (location == EquipmentLocation.LeftHand || location == EquipmentLocation.RightHand)) ||
               (equipmentType == EquipmentType.Breast && location == EquipmentLocation.Breast) ||
               (equipmentType == EquipmentType.Leg && location == EquipmentLocation.Leg);
    }
    
    // 递归函数，用于查找所有带有特定标签的子对象
    public static List<GameObject> FindChildrenWithTag(GameObject parent, string tag)
    {
        List<GameObject> taggedChildren = new List<GameObject>();

        if (string.IsNullOrEmpty(tag))
        {
            return taggedChildren;
        }

        // 遍历所有子对象
        foreach (Transform child in parent.transform)
        {
            // 如果子对象的标签匹配，添加到结果列表
            if (!string.IsNullOrEmpty(child.tag) && child.CompareTag(tag))
            {
                taggedChildren.Add(child.gameObject);
            }
            
            // 递归查找更深层的子对象
            taggedChildren.AddRange(FindChildrenWithTag(child.gameObject, tag));
        }
        
        return taggedChildren;
    }

    public static EquipmentLocation GetLocationByTag(string tag)
    {
        switch (tag)
        {
            case "leftHand":
                return EquipmentLocation.LeftHand;
            case "rightHand":
                return EquipmentLocation.RightHand;
            case "legs":
                return EquipmentLocation.Leg;
            case "head":
                return EquipmentLocation.Head;
            case "breast":
                return EquipmentLocation.Breast;
        }

        return EquipmentLocation.NULL;
    }
    
    public static string GetTagByLocation(EquipmentLocation tag)
    {
        switch (tag)
        {
            case EquipmentLocation.LeftHand:
                return "leftHand";
            case EquipmentLocation.RightHand:
                return "rightHand";
            case EquipmentLocation.Leg:
                return "legs";
            case EquipmentLocation.Head:
                return "head";
            case EquipmentLocation.Breast:
                return "breast";
        }

        return string.Empty;
    }
}
