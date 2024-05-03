using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleMono<UIManager>
{
    public OutsideUI outsideUI;

    public BattleUI BattleUI;

    public void BeginBattleUI()
    {
        BattleUI.gameObject.SetActive(true);
        outsideUI.gameObject.SetActive(false);
    }

    public void ReturnOutside()
    {
        BattleUI.gameObject.SetActive(false);
        outsideUI.gameObject.SetActive(true);
    }
}
