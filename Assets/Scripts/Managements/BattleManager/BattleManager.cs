using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleManager : SingleMono<BattleManager>
{
    private enum BattleStage
    {
        // 没有战斗
        NoBattle = 0,
        // 等待玩家选择指令
        PlayerTurning = 1,

        // 正在执行玩家指令动画
        PlayerPerforming = 2,

        // 怪兽正在行动
        MonsterTuring = 3,

        // 正在结算战斗结果
        BattleOver = 4,
    }

    private Hero _currentHero;

    private Monster _currentMonster;

    private BattleStage _currentBattleStage;

    private int _leftHeroTurns;

    private Intension _playerIntension;

    //private void Update()
    //{
    //    
    //}

    public override void Init()
    {
        _currentBattleStage = BattleStage.NoBattle;
        StartABattle();
    }

    public void StartABattle()
    {
        _currentHero = new Hero(); // TODO: 添加数据
        _currentHero.GenerateGameObject();

        _currentMonster = new Monster(); // TODO: 添加数据
        _currentMonster.GenerateGameObject();

        _currentBattleStage = BattleStage.PlayerTurning;
        _leftHeroTurns = _currentHero.Turns;
    }

    public void EndBattle()
    {
        _currentBattleStage = BattleStage.BattleOver;
    }

    public override void Tick(int delta)
    {
        _currentHero.leftTurn = _leftHeroTurns;//实时更新玩家剩余行动点数
        StageTick(delta);
    }

    private void StageTick(int delta)
    {
        switch (_currentBattleStage)
        {
            case BattleStage.NoBattle:
                return;
            case BattleStage.PlayerTurning:
                DoPlayerTurning(delta);
                return;
            case BattleStage.PlayerPerforming:
                DoPlayerPerforming(delta);
                return;
            case BattleStage.MonsterTuring:
                DoMonsterTuring(delta);
                return;
        }
    }

    private void DoPlayerTurning(int delta)
    {
        _currentHero.StopDefending();
        // 检测玩家的点击输入，确定执行的操作
        if (!CheckPlayerSelection())
        {
            return;
        }

        _currentBattleStage = BattleStage.PlayerPerforming;
    }

    private bool CheckPlayerSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标位置并转换为世界坐标
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 进行射线投射
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // 检查射线是否碰到了碰撞体
            if (hit.collider != null)
            {
                _playerIntension = new Intension()
                {
                    AttackOrDefence = _currentMonster.IsFromThisGO(hit.collider.gameObject) ? 1 : 0
                };
                switch (hit.collider.tag)
                {
                    case "leftHand":
                        _playerIntension.location = EquipmentType.LeftHand;
                        return true;
                    case "rightHand":
                        _playerIntension.location = EquipmentType.RightHand;
                        return true;
                    case "leftFoot":
                        _playerIntension.location = EquipmentType.LeftFoot;
                        return true;
                    case "rightFoot":
                        _playerIntension.location = EquipmentType.RightFoot;
                        return true;
                    case "breast":
                        _playerIntension.location = EquipmentType.Breast;
                        return true;
                }
            }
        }
        return false;
    }

    private void DoPlayerPerforming(int delta)
    {
        Debug.Log($"玩家当前意图 {_playerIntension.AttackOrDefence} {_playerIntension.location}");

        // 执行对应操作
        if (_playerIntension.AttackOrDefence == 1)
        {
            _currentHero.Attack(_currentMonster, _playerIntension.location);
        }
        else
        {
            _currentHero.Defend(_playerIntension.location);
        }


        if (_currentMonster.Hp <= 0)
        {
            // 怪物死亡

            _currentBattleStage = BattleStage.BattleOver;
            return;
        }

        _leftHeroTurns--;

        if (_leftHeroTurns <= 0)
        {
            _currentBattleStage = BattleStage.MonsterTuring;
        }
        else
        {
            _currentBattleStage = BattleStage.PlayerTurning;
        }
    }

    private void DoMonsterTuring(int delta)
    {
        _currentMonster.StopDefending();

        // 怪物根据AI执行对应动作并更新意图
        DecideMonsterAction();

        if (_currentHero.Hp <= 0)
        {
            // 玩家死亡
            _currentBattleStage = BattleStage.BattleOver;
            return;
        }

        _currentBattleStage = BattleStage.PlayerTurning;
        _leftHeroTurns = _currentHero.Turns;
    }

    private void DecideMonsterAction()
    {
        var intension = _currentMonster.CurrentIntension;
        if (intension.AttackOrDefence == 1)
        {
            _currentMonster.Attack(_currentHero, intension.location);
        }
        else
        {
            _currentMonster.Defend(intension.location);
        }
        //RefreshHeroHp?.Invoke(Mathf.Max(_currentHero.Hp));

        // 更新意图
        if (_currentMonster.HasUnequipedLocation(out var location))
        {
            _currentMonster.CurrentIntension = new Intension()
            {
                AttackOrDefence = 0,
                location = location,
            };
        }
        else
        {
            // 随机攻击一个部位
            _currentMonster.CurrentIntension = new Intension()
            {
                AttackOrDefence = 1,
                location = (EquipmentType)UnityEngine.Random.Range(1, 5),
            };
        }
    }

    public Unit GetCurrentHero()
    {
        return _currentHero;
    }

    public Unit GetCurrentMonster()
    {
        return _currentMonster;
    }
}
