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

    override internal void Update()
    {
        //调用父类的update，且自己的
        base.Update();
        //更新ui
        ActionPointBG_Image.fillAmount = (float)BattleManager.Instance.LeftHeroTurns / (heroSO.Weapon?.Turns ?? 1);
        ActionPoint_Text.text = BattleManager.Instance.LeftHeroTurns.ToString();
        MaxActionPoint_Text.text = (heroSO.Weapon?.Turns ?? 1).ToString();
    }
}
