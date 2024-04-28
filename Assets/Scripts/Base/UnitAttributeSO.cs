using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Attribute", menuName = "SO/Create Unit Attribute")]
public class UnitAttributeSO : ScriptableObject
{
    public FloatVariable Hp;

    public FloatReference MaxHp;

    public EquipmentSO LeftHand;

    public EquipmentSO RightHand;

    public EquipmentSO Head;

    public EquipmentSO Legs;

    public EquipmentSO Breast;

    public EquipmentSO Weapon;
}
