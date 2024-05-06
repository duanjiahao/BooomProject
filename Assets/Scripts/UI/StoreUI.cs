using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public Transform equipmentListTrans;
    public Transform itemListTrans;

    public GameObject commodityUI;//商品ui的预制

    private void OnEnable()
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
            obj.GetComponent<Commodity>().RefleshCommodityUI();
        }
        for (int j = 0; j < 2; j++)
        {
            GameObject obj = Instantiate(commodityUI, itemListTrans);
            int id = UnityEngine.Random.Range(10001, 10001 + ConfigManager.Instance.GetConfigNum<ItemConfig>());
            obj.GetComponent<Commodity>().price = (int)(ConfigManager.Instance.GetConfig<ItemConfig>(id).itemPrice * 1.5f);
            obj.GetComponent<Commodity>().RefleshCommodityUI();
        }
    }
}
