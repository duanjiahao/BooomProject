using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public Image playerIcon;

    public Image heroHp;

    public Text heroHpTxt;

    public Image monsterHp;

    public Text monsterHpTxt;

    public Image battleBg;

    public List<Image> actionPoints;

    private void OnEnable()
    {
        battleBg.gameObject.SetActive(true);
        RefreshUI(null);
        Notification.Instance.Register(Notification.BattleAfterHeroPerform, RefreshUI);
        Notification.Instance.Register(Notification.BattleAfterMonsterPerform, RefreshUI);
    }

    private void OnDisable()
    {
        battleBg.gameObject.SetActive(false);
        Notification.Instance.Unregister(Notification.BattleAfterHeroPerform, RefreshUI);
        Notification.Instance.Unregister(Notification.BattleAfterMonsterPerform, RefreshUI);
    }

    public void RefreshUI(object data)
    {
        var hero = BattleManager.Instance.GetCurrentHero();

        heroHp.fillAmount = hero.Hp / hero.MaxHp;

        heroHpTxt.text = $"{(int)hero.Hp}/{hero.MaxHp}";

        var monster = BattleManager.Instance.GetCurrentMonster();

        monsterHp.fillAmount = monster.Hp / monster.MaxHp;
        
        monsterHpTxt.text = $"{(int)monster.Hp}/{monster.MaxHp}";

        RefreshActionPoints(null);
    }

    private void RefreshActionPoints(object data)
    {
        var points = BattleManager.Instance.LeftHeroTurns;

        for (int i = 0; i < actionPoints.Count; i++)
        {
            var actionImg = actionPoints[i];
            actionImg.color = points > i ? new Color(198f / 255f, 145f / 255f, 92f / 255f) : new Color(93f/255f, 88f/255f, 80f/255f);
        }
    }
}
