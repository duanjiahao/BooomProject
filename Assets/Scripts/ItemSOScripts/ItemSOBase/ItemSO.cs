using System;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    [Tooltip("是否可以全局使用")]
    public bool isGlobalUse;

    public Sprite icon;//图标
    public int num = 1;//数量
    public string itemName;//名字

    [TextArea]
    public string description;//描述

    public virtual void BeUsed()
    {
        num--;
    }
}
