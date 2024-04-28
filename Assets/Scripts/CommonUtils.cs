using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonUtils
{
    public static void DestroyAllChildren(this GameObject gameObject) 
    {
        var childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var child = gameObject.transform.GetChild(i);
            GameObject.Destroy(child.gameObject);
        }
    }

    // 随机一个结果
    public static bool Roll(float chance) 
    {
        var random = Random.Range(0f, 100f);

        return chance > random;
    }

    public static SlotDirection GetInverseDirection(SlotDirection dir) 
    {
        switch (dir)
        {
            case SlotDirection.None:
                return SlotDirection.None;
            case SlotDirection.Left:
                return SlotDirection.Right;
            case SlotDirection.Right:
                return SlotDirection.Left;
            case SlotDirection.Up:
                return SlotDirection.Down;
            case SlotDirection.Down:
                return SlotDirection.Up;
            default:
                return dir;
        }
    }
}
