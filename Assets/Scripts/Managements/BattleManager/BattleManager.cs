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

    public int LeftHeroTurns
    {
        get
        {
            return _leftHeroTurns;
        }

        private set
        {
            if (_leftHeroTurns != value)
            {
                _leftHeroTurns = value;
                Notification.Instance.Notify(Notification.BattleActionPointsChanged);
            }

        }
    }

    private Intension _playerIntension;

    public override void Init()
    {
        _currentBattleStage = BattleStage.NoBattle;
    }

    public void StartABattle(bool isBoos)
    {
        _currentHero = new Hero();
        _currentHero.GenerateGameObject(0);

        if (isBoos)
        {
            var bossConfigList = ConfigManager.Instance.GetConfigListWithFilter<BossConfig>((config)=> 
            {
                return config.enemyFloor == 1;
            });
            
            _currentMonster = new Monster();
            _currentMonster.GenerateGameObject(bossConfigList[UnityEngine.Random.Range(0, bossConfigList.Count)].id);
        }
        else
        {
            var monsterConfigList = ConfigManager.Instance.GetConfigListWithFilter<EnemyConfig>((config)=> 
            {
                return config.enemyFloor == 1;
            });

            _currentMonster = new Monster();
            _currentMonster.GenerateGameObject(monsterConfigList[UnityEngine.Random.Range(0, monsterConfigList.Count)].id);
        }
   

        _currentBattleStage = BattleStage.PlayerTurning;
        _leftHeroTurns = _currentHero.Turns;
    }

    public override void Tick(int delta)
    {
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
            case BattleStage.BattleOver:
                DoEndBattle(delta);
                return;
        }
    }

    private void DoEndBattle(int delta)
    {
        // 结算
        PlayerData.Instance.ChangePlayerAttribute(PlayerAttributeType.CurrentHp, _currentHero.Hp);
        PlayerData.Instance.equipmentSystem = new EquipmentSystem(_currentHero.equipmentSystem);
        
        _currentHero.Dispose();
        _currentHero = null;

        _currentMonster.Dispose();
        _currentMonster = null;

        _currentBattleStage = BattleStage.NoBattle;
        UIManager.Instance.ReturnOutside();
    }

    private void DoPlayerTurning(int delta)
    {
        _currentHero.StopDefending();
        _currentHero.StopBreaking();
        // 检测玩家的点击输入，确定执行的操作
        if (!CheckPlayerSelection())
        {
            return;
        }

        _currentBattleStage = BattleStage.PlayerPerforming;
    }

    private bool CheckPlayerSelection()
    {
        // 获取鼠标位置并转换为世界坐标
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 进行射线投射
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        
        _currentHero.SetAllLocationColor(Color.white);
        _currentMonster.SetAllLocationColor(Color.white);
        
        // 检查射线是否碰到了碰撞体
        if (hit.collider != null)
        {
            var location = CommonUtils.GetLocationByTag(hit.collider.tag);
            var isMonsterLocation = _currentMonster.IsFromThisGO(hit.collider.gameObject);
            
            if (isMonsterLocation)
            {
                _currentMonster.SetLocationColor(location, Color.red);
            }
            else
            {
                _currentHero.SetLocationColor(location, Color.red);
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                _playerIntension = new Intension()
                {
                    AttackOrDefence = isMonsterLocation ? 1 : 0
                };
                _playerIntension.location = location;
                return true;
            }
        }

        return false;
    }

    private void DoPlayerPerforming(int delta)
    {
        // 执行对应操作
        if (_playerIntension.AttackOrDefence == 1)
        {
            if (!_currentHero.Attack(_currentMonster, _playerIntension.location))
            {
                return;
            }
        }
        else
        {
            if (!_currentHero.Defend(_playerIntension.location))
            {
                return;
            }
        }

        if (_currentMonster.Hp <= 0)
        {
            // 怪物死亡

            _currentBattleStage = BattleStage.BattleOver;
            return;
        }

        LeftHeroTurns--;

        if (_leftHeroTurns <= 0)
        {
            _currentBattleStage = BattleStage.MonsterTuring;
        }
        else
        {
            _currentBattleStage = BattleStage.PlayerTurning;
        }
        
        Notification.Instance.Notify(Notification.BattleAfterHeroPerform);
    }

    private void DoMonsterTuring(int delta)
    {
        _currentMonster.StopDefending();
        _currentMonster.StopBreaking();

        // 怪物根据AI执行对应动作并更新意图
        if (!DecideMonsterAction())
        {
            return;
        }

        if (_currentHero.Hp <= 0)
        {
            // 玩家死亡
            _currentBattleStage = BattleStage.BattleOver;
            return;
        }

        _currentBattleStage = BattleStage.PlayerTurning;
        _leftHeroTurns = _currentHero.Turns;
        
        Notification.Instance.Notify(Notification.BattleAfterMonsterPerform);
    }

    private bool DecideMonsterAction()
    {
        var intension = _currentMonster.CurrentIntension;
        if (intension.AttackOrDefence == 1)
        {
            if (!_currentMonster.Attack(_currentHero, intension.location))
            {
                return false;
            }
        }
        else
        {
            if (!_currentMonster.Defend(intension.location))
            {
                return false;
            }
        }

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
                location = (EquipmentLocation)UnityEngine.Random.Range(0, 5),
            };
        }

        return true;
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
