// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class EquipmentUI : MonoBehaviour
// {
//     public EquipmentSO equipmentSO;
//
//     public Text DamageReductionPercentage_Text;//减伤百分比UI
//
//     public Text equipmentName;//装备名称
//     public Image EquipmentDurability_Image;//装备耐久条
//
//     public Text EquipmentDurability_Text;//装备当前耐久文本
//     public float equipmentHp;//装备当前耐久
//
//     public Text MaxEquipmentDurability_Text;//装备最大耐久文本
//     public float equipmentMaxHp;//装备最大耐久
//
//     public void Start()
//     {
//
//         //if (!(equipmentSO.type == EquipmentType.Weapon))
//         //{
//         //    EquipmentDurability_Image = transform.Find("EquipmentSlot/EquipmentDurability_Image").GetComponent<Image>();
//         //    DamageReductionPercentage_Text = transform.Find("EquipmentSlot/DamageReductionPercentage").GetComponent<Text>();
//         //    EquipmentDurability_Text = transform.Find("EquipmentSlot/EquipmentDurability_Text").GetComponent<Text>();
//         //    MaxEquipmentDurability_Text = transform.Find("EquipmentSlot/MaxEquipmentDurability_Text").GetComponent<Text>();
//
//         //    DamageReductionPercentage_Text.text = "减伤：" + equipmentSO.DefencePercent.Value.ToString() + "%";
//         //}
//
//         //equipmentName = transform.Find("EquipmentSlot/EquipmentName").GetComponent<Text>();
//
//         //equipmentName.text = equipmentSO.type.ToString();
//     }
//
//     private void Update()
//     {
//         equipmentHp = equipmentSO.Hp.Value;
//         EquipmentDurability_Text.text = equipmentHp.ToString("0");
//
//         equipmentMaxHp = equipmentSO.MaxHp.Value;
//         MaxEquipmentDurability_Text.text = equipmentMaxHp.ToString("0");
//
//         EquipmentDurability_Image.rectTransform.localScale = new Vector3(equipmentHp / equipmentMaxHp, 1, 1);
//     }
// }
