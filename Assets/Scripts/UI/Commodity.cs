using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commodity : MonoBehaviour
{
    public Text priceText;
    public int price;

    public void RefleshCommodityUI()
    {
        if (PlayerData.Instance.GoldNum < price)
        {
            priceText.color = Color.red;
        }
        else if (PlayerData.Instance.GoldNum >= price)
        {
            priceText.color = Color.black;
        }
        priceText.text = price.ToString();
    }
}
