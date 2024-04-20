using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : UnitUI
{
    //行动点数UI，玩家专属的
    protected Image ActionPointBG_Image;
    protected Text MaxActionPoint_Text;//最大行动点数的文本
    protected Text ActionPoint_Text;//行动点数的文本

    //public int CurrentActionPoint//当前行动点数的属性
    //{
    //    get
    //    {
    //        ActionPointBG_Image.fillAmount = (float)currentActionPoint / maxActionPoint;
    //        ActionPoint_Text.text = currentActionPoint.ToString();
    //        currentActionPoint = currentActionPoint <= 0 ? 0 : currentActionPoint;
    //        return currentActionPoint;
    //    }
    //    set { currentActionPoint = value; }
    //}
    private void OnEnable()
    {
        //行动点数UI赋值
        ActionPointBG_Image = transform.Find("ActionPointSlot/ActionPointBG_Image").GetComponent<Image>();
        MaxActionPoint_Text = transform.Find("ActionPointSlot/MaxActionPoint_Text").GetComponent<Text>();
        ActionPoint_Text = transform.Find("ActionPointSlot/ActionPoint_Text").GetComponent<Text>();

        //血量UI赋值
        Health_Image = transform.Find("HealthSlot/Health_Image").GetComponent<Image>();
        MaxHealth_Text = transform.Find("HealthSlot/MaxHealth_Text").GetComponent<Text>();
        Health_Text = transform.Find("HealthSlot/Health_Text").GetComponent<Text>();
    }

    private void Update()
    {
        //更新ui
        ActionPointBG_Image.fillAmount = (float)BattleManager.Instance.GetCurrentTurns() / (heroSO.Weapon?.Turns ?? 1);
        ActionPoint_Text.text = BattleManager.Instance.GetCurrentTurns().ToString();
        MaxActionPoint_Text.text = (heroSO.Weapon?.Turns ?? 1).ToString();

        Health_Image.rectTransform.localScale = new Vector3(currentHealth / heroSO.MaxHp, 1, 1);
        Health_Text.text = currentHealth.Value.ToString("0");
    }
}
