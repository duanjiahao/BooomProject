using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    // 玩家的行动次数
    public int Turns 
    { 
        get { return Weapon?.Turns ?? 1; }
    }

    public override void GenerateGameObject()
    {
        Hp = 100;
        Root = GameObject.Instantiate(Resources.Load<GameObject>("Hero"));
        Root.transform.position = new Vector3(-5f, 0f, 0f);
        Root.transform.localScale = Vector3.one;
    }
}
