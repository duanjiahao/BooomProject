using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class OutsideUIScripts : MonoBehaviour
{
    public Text healthUI;
    public Text gold_UI;

    //装备耐久UI
    public Text weapenUI;
    public Text headUI;
    public Text lefthandUI;
    public Text righthandUI;
    public Text breastUI;
    public Text legsUI;

    PlayerData ins;

    private void Start()
    {
        ins = PlayerData.Instance;
        Init();
    }

    private void Update()
    {
        HealthUIBar();
        EquipmentHpUIBar();
    }

    public void Init()
    {
        healthUI.text = ins.CurrentHP.ToString() + "/" + ins.MaxHP.ToString();
        gold_UI.text = ins.GoldNum.ToString();

        EquipmentSystem system = ins.equipmentSystem;

        weapenUI.text = system.GetWeapon().Hp.ToString() + "/" + system.GetWeapon().maxHp.ToString();
        headUI.text = system.Head.Hp.ToString() + "/" + system.Head.maxHp.ToString();
        lefthandUI.text = system.LeftHand.Hp.ToString() + "/" + system.LeftHand.maxHp.ToString();
        righthandUI.text = system.RightHand.Hp.ToString() + "/" + system.RightHand.maxHp.ToString();
        breastUI.text = system.Breast.Hp.ToString() + "/" + system.Breast.maxHp.ToString();
        legsUI.text = system.Leg.Hp.ToString() + "/" + system.Leg.maxHp.ToString();
    }

    public RectMask2D healthBar;
    /// <summary>
    /// 根据血量数值调整血条长度
    /// </summary>
    public void HealthUIBar()
    {
        float width = healthBar.GetComponent<RectTransform>().rect.width;

        float right = ((ins.MaxHP - ins.CurrentHP) / ins.MaxHP) * width;

        //同步
        Vector4 padding = healthBar.padding;
        padding.z = right;
        healthBar.padding = padding;
    }

    public RectMask2D weapenHpBar;
    public RectMask2D headHpBar;
    public RectMask2D leftHandHpBar;
    public RectMask2D rightHandHpBar;
    public RectMask2D breastHpBar;
    public RectMask2D legsHpBar;
    /// <summary>
    /// 根据装备耐久调整装备耐久条长度
    /// </summary>
    public void EquipmentHpUIBar()
    {
        EquipmentSystem system = ins.equipmentSystem;

        //武器
        float width = weapenHpBar.GetComponent<RectTransform>().rect.width;
        float right = ((system.GetWeapon().maxHp - system.GetWeapon().Hp) / system.GetWeapon().maxHp) * width;
        Vector4 padding = weapenHpBar.padding;
        padding.z = right;
        weapenHpBar.padding = padding;

        //头
        width = headHpBar.GetComponent<RectTransform>().rect.width;
        right = ((system.Head.maxHp - system.Head.Hp) / system.Head.maxHp) * width;
        padding = headHpBar.padding;
        padding.z = right;
        headHpBar.padding = padding;

        //左手
        width = leftHandHpBar.GetComponent<RectTransform>().rect.width;
        right = ((system.LeftHand.maxHp - system.LeftHand.Hp) / system.LeftHand.maxHp) * width;
        padding = leftHandHpBar.padding;
        padding.z = right;
        leftHandHpBar.padding = padding;

        //右手
        width = rightHandHpBar.GetComponent<RectTransform>().rect.width;
        right = ((system.RightHand.maxHp - system.RightHand.Hp) / system.RightHand.maxHp) * width;
        padding = rightHandHpBar.padding;
        padding.z = right;
        rightHandHpBar.padding = padding;

        //胸
        width = breastHpBar.GetComponent<RectTransform>().rect.width;
        right = ((system.Breast.maxHp - system.Breast.Hp) / system.Breast.maxHp) * width;
        padding = breastHpBar.padding;
        padding.z = right;
        breastHpBar.padding = padding;

        //腿
        width = legsHpBar.GetComponent<RectTransform>().rect.width;
        right = ((system.Leg.maxHp - system.Leg.Hp) / system.Leg.maxHp) * width;
        padding = legsHpBar.padding;
        padding.z = right;
        legsHpBar.padding = padding;
    }
}
