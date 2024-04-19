using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    public Unit _currentUnit;

    //血量UI
    protected Image Health_Image;//血条的图片，锚点为（0，0.5）
    protected Text MaxHealth_Text;//最大血量的文本
    protected Text Health_Text;//血量的文本
    protected float maxHealth = 100;//最大血量
    protected float currentHealth = 100;//当前血量
    //public float CurrentHealth//当前血量的属性
    //{
    //    get 
    //    {
    //        Health_Image.rectTransform.localScale = new Vector3(currentHealth / maxHealth,1,1);
    //        Health_Text.text = currentHealth.ToString("0");
    //        currentHealth = currentHealth <= 0 ? 0 : currentHealth;
    //        return currentHealth; 
    //    }
    //    set { currentHealth = value; }
    //}

    private void OnEnable()
    {
        //血量UI赋值
        Health_Image = transform.Find("HealthSlot/Health_Image").GetComponent<Image>();
        MaxHealth_Text = transform.Find("HealthSlot/MaxHealth_Text").GetComponent<Text>();
        Health_Text = transform.Find("HealthSlot/Health_Text").GetComponent<Text>();
    }


    private void Update()
    {
        currentHealth = _currentUnit.Hp;
        Health_Image.rectTransform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
        Health_Text.text = currentHealth.ToString("0");
        currentHealth = currentHealth <= 0 ? 0 : currentHealth;
    }

    ///// <summary>
    ///// 暴露给Unit的HP的属性，当HP的值更改的时候，HP的属性应该调用一次刷新hp的ui的方法
    ///// </summary>
    ///// <param name="hp"></param>
    //public void RefleshCurrentHealth(float hp)
    //{
    //    CurrentHealth = hp;
    //}
    ///// <summary>
    ///// 暴露给Unit的行动点数的属性，当行动点数的值更改的时候，行动点数的属性应该调用一次刷新行动点数的ui的方法
    ///// </summary>
    ///// <param name="hp"></param>
    //public void RefleshCurrentActionPoint(int point)
    //{
    //    currentActionPoint = point;
    //}
}
