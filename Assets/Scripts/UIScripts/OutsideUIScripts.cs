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

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        PlayerData ins = PlayerData.Instance;

        //healthUI.text = ins.currentHP.ToString() + "/" + ins.maxHP.ToString();
        //gold_UI.text = ins.goldNum.ToString();

        //weapenUI.text = ins.weapenSO.Hp.Value.ToString() + "/" + ins.weapenSO.MaxHp.Value.ToString();
        //headUI.text = ins.headSO.Hp.Value.ToString() + "/" + ins.headSO.MaxHp.Value.ToString();
        //lefthandUI.text = ins.leftHandSO.Hp.Value.ToString() + "/" + ins.leftHandSO.MaxHp.Value.ToString();
        //righthandUI.text = ins.rightHandSO.Hp.Value.ToString() + "/" + ins.rightHandSO.MaxHp.Value.ToString();
        //breastUI.text = ins.breastSO.Hp.Value.ToString() + "/" + ins.breastSO.MaxHp.Value.ToString();
        //legsUI.text = ins.legsSO.Hp.Value.ToString() + "/" + ins.legsSO.MaxHp.Value.ToString();
    }
}
