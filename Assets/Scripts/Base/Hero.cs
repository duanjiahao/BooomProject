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
}
