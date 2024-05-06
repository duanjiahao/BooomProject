using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    //用来标志物品在哪里的列表
    public static List<Marketable> onbody = new List<Marketable>();
    public static List<Marketable> inMarket = new List<Marketable>();

    public Text goldNumText;
    public Transform equipmentListTrans;
    public Transform itemListTrans;

    public GameObject commodityUI;//商品ui的预制
    public List<Commodity> commodities = new List<Commodity>();

    //todo:一个商店，一次进入时不会改变它的物品

    #region 刷新和讲价逻辑
    private void OnEnable()
    {
        RefleshStore();
    }

    public void Update()
    {
        goldNumText.text = PlayerData.Instance.GoldNum.ToString();
    }

    private void OnDisable()
    {
        ClearStore();
    }

    public void RefleshStore()
    {
        ClearStore();
        GenerateCommodity();
    }
    private void GenerateCommodity()
    {
        for (int i = 0; i < 3; ++i)
        {
            GameObject obj = Instantiate(commodityUI, equipmentListTrans);
            int a = UnityEngine.Random.Range(30001, 30001 + ConfigManager.Instance.GetConfigNum<WeaponConfig>());
            int b = UnityEngine.Random.Range(20001, 20001 + ConfigManager.Instance.GetConfigNum<ArmorConfig>());
            int id = UnityEngine.Random.Range(0f, 1f) > 0.5f ? a : b;
            string type = id > 30000 ? "WeaponConfig" : "ArmorConfig";
            switch (type)
            {
                case "WeaponConfig":
                    WeaponConfig weaponConfig = ConfigManager.Instance.GetConfig<WeaponConfig>(id);
                    obj.GetComponent<Commodity>().price = (int)(weaponConfig.weapomPrice * 1.5f);
                    break;
                case "ArmorConfig":
                    ArmorConfig armorConfig = ConfigManager.Instance.GetConfig<ArmorConfig>(id);
                    obj.GetComponent<Commodity>().price = (int)(armorConfig.armorPrice * 1.5f);
                    break;
                default:
                    break;
            }
            commodities.Add(obj.GetComponent<Commodity>());
            inMarket.Add(obj.transform.GetChild(0).GetComponent<Marketable>());
            obj.GetComponent<Commodity>().RefleshCommodityUI();
        }
        for (int j = 0; j < 2; j++)
        {
            GameObject obj = Instantiate(commodityUI, itemListTrans);
            int id = UnityEngine.Random.Range(10001, 10001 + ConfigManager.Instance.GetConfigNum<ItemConfig>());
            obj.GetComponent<Commodity>().price = (int)(ConfigManager.Instance.GetConfig<ItemConfig>(id).itemPrice * 1.5f);
            commodities.Add(obj.GetComponent<Commodity>());
            inMarket.Add(obj.transform.GetChild(0).GetComponent<Marketable>());
            obj.GetComponent<Commodity>().RefleshCommodityUI();
        }
    }

    public void ClearStore()
    {
        for (int i = equipmentListTrans.childCount - 1; i > -1; --i)
        {
            DestroyImmediate(equipmentListTrans.GetChild(i).gameObject);
        }
        for (int i = itemListTrans.childCount - 1; i > -1; --i)
        {
            DestroyImmediate(itemListTrans.GetChild(i).gameObject);
        }
        commodities.Clear();
        inMarket.Clear();
    }

    public void ForBargain()
    {
        foreach (var item in commodities)
        {
            int result = UnityEngine.Random.Range(1, 11);
            if (result <= 1)
            {
                item.price = (int)(item.price * 0.5f);
            }
            else if (result >= 2 && result <= 4)
            {
                item.price = (int)(item.price * 0.7f);
            }
            else if (result >= 8 && result <= 10)
            {
                item.price = (int)(item.price * 1.2f);
            }
            item.RefleshCommodityUI();
        }
    }
    #endregion

    #region 购买和出售逻辑

    public bool Buy()
    {
        return false;
    }
    public bool Sell()
    {
        return false;
    }
    #endregion
}
