using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public struct SlotPosition : IEquatable<SlotPosition>
{
    public int X;
    public int Y;

    public SlotPosition(int x, int y) 
    {
        this.X = x;
        this.Y = y;
    }

    public SlotPosition Next(SlotDirection dir) 
    {
        switch (dir)
        {
            case SlotDirection.Left:
                return new SlotPosition(X - 1, Y);
            case SlotDirection.Right:
                return new SlotPosition(X + 1, Y);
            case SlotDirection.Up:
                return new SlotPosition(X, Y + 1);
            case SlotDirection.Down:
                return new SlotPosition(X, Y - 1);
        }
        return this;
    }

    public SlotDirection Dir(SlotPosition pos)
    {
        if (this.X == pos.X && this.Y == pos.Y + 1) 
        {
            return SlotDirection.Down;
        }

        if (this.X == pos.X && this.Y == pos.Y - 1)
        {
            return SlotDirection.Up;
        }

        if (this.X == pos.X + 1 && this.Y == pos.Y)
        {
            return SlotDirection.Left;
        }

        if (this.X == pos.X - 1 && this.Y == pos.Y)
        {
            return SlotDirection.Right;
        }

        return SlotDirection.None;
    }

    public bool IsNeighbour(SlotPosition other)
    {
        var dis = this.X + this.Y;
        var otherDis = other.X + other.Y;
        return Mathf.Abs(dis - otherDis) == 1;
    }

    public bool Equals(SlotPosition other)
    {
        return this.X == other.X && this.Y == other.Y;
    }
}

public class LabyrinthWindow : MonoBehaviour
{
    public Transform _playerRoot;

    public Transform _slotRoot;

    private GameObject _slotPrefab;

    private Dictionary<SlotPosition, LabyrinthSlot> _slotDic;

    private const float INTERVAL = 124f;

    private SlotPosition _currentPosition;

    private bool _moving;

    private GameObject _playerGO;

    // Start is called before the first frame update
    void Start()
    {
        _slotDic = new Dictionary<SlotPosition, LabyrinthSlot>();
        _slotPrefab = Resources.Load<GameObject>("UI/LabyrinthSlot");
        _playerGO = Instantiate(Resources.Load<GameObject>("UI/smallPlayer"), _playerRoot);
        _playerGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(_currentPosition.X * INTERVAL, _currentPosition.Y * INTERVAL);

        GenerateLabylinth();
    }

    private void GenerateLabylinth()
    {
        var floorConfig = ConfigManager.Instance.GetConfig<FloorConfigConfig>(1);

        var X = floorConfig.mainRoom;

        var BattleCount = 3;
        var EventCount = 1;

        // 要求多出base数量的格子，按照7:3（战斗：事件）的比例生成，这里先计算出额外的事件数量
        var extraEventCount = Mathf.FloorToInt((X - (BattleCount + EventCount)) * 3f / 10f);

        if (extraEventCount > 0) 
        {
            EventCount += extraEventCount;
        }

        // 生成一个集合，其中ture表示是战斗房，flase表示事件房
        List<bool> randomSet = new List<bool>(X);
        for (int i = 0; i < X; i++)
        {
            randomSet.Add(i >= EventCount);
        }

        // 先生成出生点
        var slotPosition = new SlotPosition();
        GenerateSlot(slotPosition, SlotDirection.None, SlotType.Born);

        var _lastSlotPosition = slotPosition;
        for (int i = 0; i < X + 1; i++) // 多生成一个BOSS房
        {
            if (IsAllSurrounded(_lastSlotPosition)) 
            {
                var slot = _slotDic[_lastSlotPosition];
                _lastSlotPosition = _lastSlotPosition.Next(slot.Direction);
                i--;
                continue;
            }

            SlotDirection dir;
            SlotPosition next;
            do
            {
                dir = (SlotDirection)UnityEngine.Random.Range(1, 5);
                next = _lastSlotPosition.Next(dir);

            } while (_slotDic.ContainsKey(next));

            SlotType selectType;
            if (i == X)
            {
                selectType = SlotType.Boss;
            }
            else 
            {
                var randomIndex = UnityEngine.Random.Range(0, randomSet.Count);
                selectType = randomSet[randomIndex] ? SlotType.Battle : SlotType.Event;
                randomSet.RemoveAt(randomIndex);
            }

            GenerateSlot(next, dir, selectType);
            _lastSlotPosition = next;
        }

        var chance = floorConfig.sideRoomRate * 100f;
        
        // 生成的房间是战斗房，要再继续生成，概率衰减
        var reduce = floorConfig.sideRoomDecrease * 100f;

        var generateRoomList = new List<SlotPosition>();
        foreach (var kv in _slotDic)
        {
            if (kv.Value.SlotType != SlotType.Born && kv.Value.SlotType != SlotType.Boss) 
            {
                generateRoomList.Add(kv.Key);
            }
        }

        // 只生成一个商店
        bool hasGeneratedShop = false;
        foreach (var pos in generateRoomList)
        {
            var slotDirs = GetCanGenerateDirs(pos);

            foreach (var dir in slotDirs)
            {
                if (CommonUtils.Roll(chance)) 
                {
                    var newPos = pos.Next(dir);
                    var type = (SlotType)UnityEngine.Random.Range(1, hasGeneratedShop ? 3 : 4);
                    GenerateSlot(newPos, dir, type);

                    // 如果随机到了战斗就接着再生成
                    if (type == SlotType.Battle)
                    {
                        GenrateAdditionalRoomRecursive(newPos, chance, reduce);
                    }
                    else if (type == SlotType.Shop) 
                    {
                        hasGeneratedShop = true;
                    }
                }
            }
        }

        while (!hasGeneratedShop)
        {
            var randomRoom = generateRoomList[UnityEngine.Random.Range(0, generateRoomList.Count)];
            var slotDirs = GetCanGenerateDirs(randomRoom);
            if (slotDirs.Count > 0) 
            {
                var randomDir = slotDirs[UnityEngine.Random.Range(0, slotDirs.Count)];
                var newPos = randomRoom.Next(randomDir);
                GenerateSlot(newPos, randomDir, SlotType.Shop);
                hasGeneratedShop = true;
            }
        }

        // 暂定房间连通概率为50
        var connectRate = 50f;

        foreach (var kv in _slotDic)
        {
            if (kv.Value.SlotType != SlotType.Born && kv.Value.SlotType != SlotType.Boss)
            {
                var notConnectedList = GetNotConnectedDirs(kv.Key);
                foreach (var dir in notConnectedList)
                {
                    if (CommonUtils.Roll(connectRate)) 
                    {
                        kv.Value.AddConnection(dir);
                        var nextSlot = kv.Key.Next(dir);
                        _slotDic[nextSlot].AddConnection(CommonUtils.GetInverseDirection(dir));
                    }
                }
            }
        }
    }

    [ContextMenu("测试生成一遍")]
    // 测试用
    public void RegenerateLabylinth() 
    {
        _slotDic = new Dictionary<SlotPosition, LabyrinthSlot>();
        gameObject.DestroyAllChildren();
        GenerateLabylinth();
    }

    private bool IsAllSurrounded(SlotPosition pos) 
    {
        return _slotDic.ContainsKey(pos.Next(SlotDirection.Left)) &&
               _slotDic.ContainsKey(pos.Next(SlotDirection.Right)) &&
               _slotDic.ContainsKey(pos.Next(SlotDirection.Up)) &&
               _slotDic.ContainsKey(pos.Next(SlotDirection.Down));
    }

    private void GenrateAdditionalRoomRecursive(SlotPosition pos, float chance, float reduce) 
    {
        var slotDirs = GetCanGenerateDirs(pos);
        foreach (var dir in slotDirs)
        {
            if (CommonUtils.Roll(chance))
            {
                var newPos = pos.Next(dir);
                var type = (SlotType)UnityEngine.Random.Range(1, 4);
                GenerateSlot(newPos, dir, type);

                if (type == SlotType.Battle) 
                {
                    GenrateAdditionalRoomRecursive(newPos, chance - reduce, reduce);
                }
            }
        }
    }

    private List<SlotDirection> GetCanGenerateDirs(SlotPosition pos) 
    {
        var slotDirs = new List<SlotDirection>();
        for (int i = 1; i <= 4; i++)
        {
            var dir = (SlotDirection)i;
            if (!_slotDic.ContainsKey(pos.Next(dir)))
            {
                slotDirs.Add(dir);
            }
        }
        return slotDirs;
    }

    private List<SlotDirection> GetNotConnectedDirs(SlotPosition pos) 
    {
        var slotDirs = new List<SlotDirection>();
        for (int i = 1; i <= 4; i++)
        {
            var dir = (SlotDirection)i;
            var nextPos = pos.Next(dir);
            if (_slotDic.ContainsKey(nextPos))
            {
                var nextSlot = _slotDic[nextPos];
                if (nextSlot.SlotType == SlotType.Born || nextSlot.SlotType == SlotType.Boss) 
                {
                    continue;
                }

                if (!nextSlot.HasConnection(CommonUtils.GetInverseDirection(dir))) 
                {
                    slotDirs.Add(dir);
                }
            }
        }
        return slotDirs;
    }

    private LabyrinthSlot GenerateSlot(SlotPosition pos, SlotDirection dir, SlotType type) 
    {
        var slot = GameObject.Instantiate(_slotPrefab, _slotRoot);
        var slotComp = slot.GetComponent<LabyrinthSlot>();
        slotComp.Btn.onClick.AddListener(()=> 
        {
            OnSlotClicked(pos);
        });
        slotComp.Init(dir, type);
        if (type == SlotType.Born)
        {
            slotComp.SetExplored();
        }

        if (dir != SlotDirection.None) 
        {
            var lastPos = pos.Next(CommonUtils.GetInverseDirection(dir));
            _slotDic[lastPos].AddConnection(dir);
        }

        slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.X * INTERVAL, pos.Y * INTERVAL);
        _slotDic.Add(pos, slotComp);
        return slotComp;
    }

    private void OnSlotClicked(SlotPosition pos)
    {
        if (_moving) 
        {
            return;
        }

        if (_currentPosition.IsNeighbour(pos)) 
        {
            var currentSlot = _slotDic[_currentPosition];

            if (currentSlot.HasConnection(_currentPosition.Dir(pos))) 
            {
                MoveTo(pos);
            }
        }
    }

    private void MoveTo(SlotPosition pos)
    {
        _moving = true;

        var targetPos = new Vector2(-pos.X * INTERVAL, -pos.Y * INTERVAL);
        _slotRoot.GetComponent<RectTransform>().DOAnchorPos(targetPos, 0.5f).OnComplete(()=> 
        {
            _currentPosition = pos;
            _moving = false;

            var currentSlot = _slotDic[_currentPosition];

            switch (currentSlot.SlotType)
            {
                case SlotType.Battle:
                    DoBattle();
                    break;
                case SlotType.Event:
                    DoEvent();
                    break;
                case SlotType.Shop:
                    DoShop();
                    break;
                case SlotType.Boss:
                    DoBoss();
                    break;
            }
            
            currentSlot.SetExplored();
        });
    }

    private void DoBoss()
    {
    }

    private void DoShop()
    {
    }

    private void DoEvent()
    {
        UIManager.Instance.BeginEventUI();
    }

    private void DoBattle()
    {
        BattleManager.Instance.StartABattle(false);
        UIManager.Instance.BeginBattleUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
