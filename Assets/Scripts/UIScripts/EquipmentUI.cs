using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    private Image EquipmentDurability_Image;
    private Text EquipmentDurability_Text;
    private Text MaxEquipmentDurability_Text;
    private EquipmentType equipmentType;
    private float equipmentMaxHp = 100;
    private float equipmentHp = 90;
    public float EquipmentHp
    {
        get 
        {
            EquipmentDurability_Image.rectTransform.localScale = new Vector3(equipmentHp / equipmentMaxHp, 1, 1);
            EquipmentDurability_Text.text = equipmentHp.ToString("0");
            equipmentHp = equipmentHp <= 0 ? 0 : equipmentHp;
            return equipmentHp;
        } 
        set
        {
            equipmentHp = value;
        }
    }

    public void OnEnable()
    {
        EquipmentDurability_Image = transform.Find("EquipmentSlot/EquipmentDurability_Image").GetComponent<Image>();
        EquipmentDurability_Text = transform.Find("EquipmentSlot/EquipmentDurability_Text").GetComponent<Text>();
        MaxEquipmentDurability_Text = transform.Find("EquipmentSlot/MaxEquipmentDurability_Text").GetComponent<Text>();
    }

    //从装备上拉取数据
    public void EquipmentUIInit()
    {
        Equipment equipment = GetComponent<Equipment>();
        equipmentType = equipment.type;
        equipmentHp = equipment.Hp;
        equipmentMaxHp = equipment.MaxHp;
    }

    private void Update()
    {
        print(EquipmentHp);         
    }
}
