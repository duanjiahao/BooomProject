using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleMono<UIManager>
{
    public OutsideUI outsideUI;

    public BattleUI BattleUI;

    public EventUI EventUI;

    public void BeginBattleUI()
    {
        BattleUI.gameObject.SetActive(true);
        outsideUI.gameObject.SetActive(false);
    }

    public void ReturnOutside()
    {
        BattleUI.gameObject.SetActive(false);
        EventUI.gameObject.SetActive(false);
        outsideUI.gameObject.SetActive(true);
    }

    public void BeginEventUI()
    {
        EventUI.gameObject.SetActive(true);
        BattleUI.gameObject.SetActive(false);
    }
}
