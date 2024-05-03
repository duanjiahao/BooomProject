using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemConfig config;

    public int num;

    public Item(int itemId, int num = 1)
    {
        config = ConfigManager.Instance.GetConfig<ItemConfig>(itemId);
        this.num = num;
    }
}
