using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class PlayerDetailsInStore : MonoBehaviour
{
    public Transform head;
    public Transform righthand;
    public Transform lefthand;
    public Transform breast;
    public Transform leg;
    public Transform weapon;

    public Transform[] itemTrans;

    public GameObject equipmentIcon;
    public GameObject itemIcon;

    private void OnEnable()
    {
        EquipmentSystem system = PlayerData.Instance.equipmentSystem;

        if (system.Head != null)
        {
            GameObject obj = Instantiate(equipmentIcon, head);
            obj.GetComponent<Marketable>().thisEquipment = system.Head;
            StoreUI.onbody.Add(obj.GetComponent<Marketable>());
        }
        if (system.LeftHand != null)
        {
            GameObject obj = Instantiate(equipmentIcon, lefthand);
            obj.GetComponent<Marketable>().thisEquipment = system.LeftHand;
            StoreUI.onbody.Add(obj.GetComponent<Marketable>());
        }
        if (system.RightHand != null)
        {
            GameObject obj = Instantiate(equipmentIcon, righthand);
            obj.GetComponent<Marketable>().thisEquipment = system.RightHand;
            StoreUI.onbody.Add(obj.GetComponent<Marketable>());
        }
        if (system.Breast != null)
        {
            GameObject obj = Instantiate(equipmentIcon, breast);
            obj.GetComponent<Marketable>().thisEquipment = system.Breast;
            StoreUI.onbody.Add(obj.GetComponent<Marketable>());
        }
        if (system.Leg != null)
        {
            GameObject obj = Instantiate(equipmentIcon, leg);
            obj.GetComponent<Marketable>().thisEquipment = system.Leg;
            StoreUI.onbody.Add(obj.GetComponent<Marketable>());
        }
        if (system.Weapon != null)
        {
            GameObject obj = Instantiate(equipmentIcon, weapon);
            obj.GetComponent<Marketable>().thisWeapon = system.Weapon;
            StoreUI.onbody.Add(obj.GetComponent<Marketable>());
        }

        List<Item> items = PlayerData.Instance.ItemList;
        for (int i = 0; i < items.Count; i++)
        {
            GameObject obj = Instantiate(itemIcon, itemTrans[i]);
            obj.GetComponent<Marketable>().thisItem = items[i];
            StoreUI.onbody.Add(obj.GetComponent<Marketable>());
        }
    }
}
