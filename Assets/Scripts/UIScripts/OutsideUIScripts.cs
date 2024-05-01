using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        healthUI.text = ins.currentHP.ToString() + "/" + ins.maxHP.ToString();
        gold_UI.text = ins.goldNum.ToString();

        weapenUI.text = ins.weapenSO.Hp.Value.ToString() + "/" + ins.weapenSO.MaxHp.Value.ToString();
        headUI.text = ins.headSO.Hp.Value.ToString() + "/" + ins.headSO.MaxHp.Value.ToString();
        lefthandUI.text = ins.leftHandSO.Hp.Value.ToString() + "/" + ins.leftHandSO.MaxHp.Value.ToString();
        righthandUI.text = ins.rightHandSO.Hp.Value.ToString() + "/" + ins.rightHandSO.MaxHp.Value.ToString();
        breastUI.text = ins.breastSO.Hp.Value.ToString() + "/" + ins.breastSO.MaxHp.Value.ToString();
        legsUI.text = ins.legsSO.Hp.Value.ToString() + "/" + ins.legsSO.MaxHp.Value.ToString();
    }

    public RectMask2D healthBar;
    /// <summary>
    /// 根据血量数值调整血条长度
    /// </summary>
    public void HealthUIBar()
    {
        float width = healthBar.GetComponent<RectTransform>().rect.width;

        float right = ((ins.maxHP - ins.currentHP) / ins.maxHP) * width;

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
        //武器
        float width = weapenHpBar.GetComponent<RectTransform>().rect.width;
        float right = ((ins.weapenSO.MaxHp.Value - ins.weapenSO.Hp.Value) / ins.weapenSO.MaxHp.Value) * width;
        Vector4 padding = weapenHpBar.padding;
        padding.z = right;
        weapenHpBar.padding = padding;

        //头
        width = headHpBar.GetComponent<RectTransform>().rect.width;
        right = ((ins.headSO.MaxHp.Value - ins.headSO.Hp.Value) / ins.headSO.MaxHp.Value) * width;
        padding = headHpBar.padding;
        padding.z = right;
        headHpBar.padding = padding;

        //左手
        width = leftHandHpBar.GetComponent<RectTransform>().rect.width;
        right = ((ins.leftHandSO.MaxHp.Value - ins.leftHandSO.Hp.Value) / ins.leftHandSO.MaxHp.Value) * width;
        padding = leftHandHpBar.padding;
        padding.z = right;
        leftHandHpBar.padding = padding;

        //右手
        width = rightHandHpBar.GetComponent<RectTransform>().rect.width;
        right = ((ins.rightHandSO.MaxHp.Value - ins.rightHandSO.Hp.Value) / ins.rightHandSO.MaxHp.Value) * width;
        padding = rightHandHpBar.padding;
        padding.z = right;
        rightHandHpBar.padding = padding;

        //胸
        width = breastHpBar.GetComponent<RectTransform>().rect.width;
        right = ((ins.breastSO.MaxHp.Value - ins.breastSO.Hp.Value) / ins.breastSO.MaxHp.Value) * width;
        padding = breastHpBar.padding;
        padding.z = right;
        breastHpBar.padding = padding;

        //腿
        width = legsHpBar.GetComponent<RectTransform>().rect.width;
        right = ((ins.legsSO.MaxHp.Value - ins.legsSO.Hp.Value) / ins.legsSO.MaxHp.Value) * width;
        padding = legsHpBar.padding;
        padding.z = right;
        legsHpBar.padding = padding;
    }
}
