using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public Text DamageReductionPercentage_Text;//减伤百分比UI
    public Text equipmentName;
    public Image EquipmentDurability_Image;
    public Text EquipmentDurability_Text;
    public Text MaxEquipmentDurability_Text;
    public Equipment equipment;
    public float equipmentMaxHp;
    public float equipmentHp;
    //public float EquipmentHp
    //{
    //    get
    //    {
    //        EquipmentDurability_Image.rectTransform.localScale = new Vector3(equipmentHp / equipmentMaxHp, 1, 1);
    //        EquipmentDurability_Text.text = equipmentHp.ToString("0");
    //        equipmentHp = equipmentHp <= 0 ? 0 : equipmentHp;
    //        return equipmentHp;
    //    }
    //    set
    //    {
    //        equipmentHp = value;
    //    }
    //}

    public void Start()
    {

        if (!(equipment.type == EquipmentType.Weapon))
        {
            DamageReductionPercentage_Text = transform.Find("EquipmentSlot/DamageReductionPercentage").GetComponent<Text>();
            EquipmentDurability_Image = transform.Find("EquipmentSlot/EquipmentDurability_Image").GetComponent<Image>();
            EquipmentDurability_Text = transform.Find("EquipmentSlot/EquipmentDurability_Text").GetComponent<Text>();
            MaxEquipmentDurability_Text = transform.Find("EquipmentSlot/MaxEquipmentDurability_Text").GetComponent<Text>();

            DamageReductionPercentage_Text.text = "减伤：" + equipment.DefencePercent.ToString() + "%";
        }

        equipmentName = transform.Find("EquipmentSlot/EquipmentName").GetComponent<Text>();

        equipmentName.text = equipment.type.ToString();
    }

    private void Update()
    {
        equipmentHp = equipment.Hp;
        equipmentMaxHp = equipment.MaxHp;
        EquipmentDurability_Image.rectTransform.localScale = new Vector3(equipmentHp / equipmentMaxHp, 1, 1);
        EquipmentDurability_Text.text = equipmentHp.ToString("0");
    }
}
