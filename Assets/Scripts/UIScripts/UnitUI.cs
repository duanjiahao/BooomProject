using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    //血量UI
    protected Image Health_Image;//血条的图片，锚点为（0，0.5）
    protected Text MaxHealth_Text;//最大血量的文本
    protected Text Health_Text;//血量的文本

    [SerializeField]
    protected UnitAttributeSO heroSO;
    [SerializeField]
    protected FloatReference currentHealth;//当前血量

    private void OnEnable()
    {
        //血量UI赋值
        Health_Image = transform.Find("HealthSlot/Health_Image").GetComponent<Image>();
        MaxHealth_Text = transform.Find("HealthSlot/MaxHealth_Text").GetComponent<Text>();
        Health_Text = transform.Find("HealthSlot/Health_Text").GetComponent<Text>();
    }


    internal virtual void Update()
    {
        Health_Image.rectTransform.localScale = new Vector3(currentHealth / heroSO.MaxHp, 1, 1);
        Health_Text.text = currentHealth.Value.ToString("0");
        MaxHealth_Text.text = heroSO.MaxHp.Value.ToString("0");
    }
}
