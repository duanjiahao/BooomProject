using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : UnitUI
{
    public Hero _currentHero;

    //行动点数UI，玩家专属的
    protected Image ActionPointBG_Image;
    protected Text MaxActionPoint_Text;//最大行动点数的文本
    protected Text ActionPoint_Text;//行动点数的文本
    protected int maxActionPoint = 3;//最大行动点数
    protected int currentActionPoint = 3;//行动点数
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
    }

    private void Update()
    {
        currentActionPoint = _currentHero.leftTurn;
        //更新ui
        ActionPointBG_Image.fillAmount = (float)currentActionPoint / maxActionPoint;
        ActionPoint_Text.text = currentActionPoint.ToString();
        currentActionPoint = currentActionPoint <= 0 ? 0 : currentActionPoint;
    }
}
