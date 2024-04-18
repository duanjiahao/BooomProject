using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    //血量UI
    private Image Health_Image;//血条的图片，锚点为（0，0.5）
    private Text MaxHealth_Text;//最大血量的文本
    private Text Health_Text;//血量的文本
    private float maxHealth = 100;//最大血量
    private float currentHealth = 90;//当前血量
    public float CurrentHealth//当前血量的属性
    {
        get 
        {
            Health_Image.rectTransform.localScale = new Vector3(currentHealth / maxHealth,1,1);
            Health_Text.text = currentHealth.ToString("0");
            currentHealth = currentHealth <= 0 ? 0 : currentHealth;
            return currentHealth; 
        }
        set { currentHealth = value; }
    }

    //行动点数UI
    private Image ActionPointBG_Image;
    private Text MaxActionPoint_Text;//最大行动点数的文本
    private Text ActionPoint_Text;//行动点数的文本
    private int maxActionPoint = 3;//最大行动点数
    private int currentActionPoint = 2;//行动点数
    public int CurrentActionPoint//当前行动点数的属性
    {
        get
        {
            ActionPointBG_Image.fillAmount = (float)currentActionPoint / maxActionPoint;
            ActionPoint_Text.text = currentActionPoint.ToString();
            currentActionPoint = currentActionPoint <= 0 ? 0 : currentActionPoint;
            return currentActionPoint;
        }
        set { currentActionPoint = value; }
    }

    private void OnEnable()
    {
        //血量UI赋值
        Health_Image = transform.Find("HealthSlot/Health_Image").GetComponent<Image>();
        MaxHealth_Text = transform.Find("HealthSlot/MaxHealth_Text").GetComponent<Text>();
        Health_Text = transform.Find("HealthSlot/Health_Text").GetComponent<Text>();

        //行动点数UI赋值
        ActionPointBG_Image = transform.Find("ActionPointSlot/ActionPointBG_Image").GetComponent<Image>();
        MaxActionPoint_Text = transform.Find("ActionPointSlot/MaxActionPoint_Text").GetComponent<Text>();
        ActionPoint_Text = transform.Find("ActionPointSlot/ActionPoint_Text").GetComponent<Text>();
    }

    void Update()
    {
        print(CurrentHealth);
        print(CurrentActionPoint);
    }

    /// <summary>
    /// 暴露给Unit的HP的属性，当HP的值更改的时候，HP的属性应该调用一次刷新hp的ui的方法
    /// </summary>
    /// <param name="hp"></param>
    public void RefleshCurrentHealth(float hp)
    {
        CurrentHealth = hp;
    }
    /// <summary>
    /// 暴露给Unit的行动点数的属性，当行动点数的值更改的时候，行动点数的属性应该调用一次刷新行动点数的ui的方法
    /// </summary>
    /// <param name="hp"></param>
    public void RefleshCurrentActionPoint(int point)
    {
        currentActionPoint = point;
    }
}
