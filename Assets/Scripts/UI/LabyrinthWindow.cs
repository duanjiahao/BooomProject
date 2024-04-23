﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool Equals(SlotPosition other)
    {
        return this.X == other.X && this.Y == other.Y;
    }
}

public class LabyrinthWindow : MonoBehaviour
{
    private GameObject _slotPrefab;

    private Dictionary<SlotPosition, LabyrinthSlot> _slotDic;

    private const float INTERVAL = 165f; 

    // Start is called before the first frame update
    void Start()
    {
        _slotDic = new Dictionary<SlotPosition, LabyrinthSlot>();
        _slotPrefab = Resources.Load<GameObject>("UI/LabyrinthSlot");

        GenerateLabylinth();
    }

    private void GenerateLabylinth()
    {
        var X = 8; // TODO: 读表

        var BattleCount = 3;
        var EventCount = 1;

        // 要求多出base数量的格子，按照7:3（战斗：事件）的比例生成，这里先计算出额外的事件数量
        var extraEventCount = Mathf.FloorToInt((X - (BattleCount + EventCount)) * 3f / 7f);

        if (extraEventCount > 0) 
        {
            EventCount += extraEventCount;
        }

        BattleCount = X - EventCount;

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

        // 先假定生成的附属房间的概率是50%
        var chance = 50;
        
        // 生成的房间是战斗房，要再继续生成，概率衰减
        var reduce = 20;

        var generateRoomList = new List<SlotPosition>();
        foreach (var kv in _slotDic)
        {
            if (kv.Value.SlotType != SlotType.Born && kv.Value.SlotType != SlotType.Boss) 
            {
                generateRoomList.Add(kv.Key);
            }
        }

        foreach (var pos in generateRoomList)
        {
            var slotDirs = GetCanGenerateDirs(pos);

            foreach (var dir in slotDirs)
            {
                if (CommonUtils.Roll(chance)) 
                {
                    var newPos = pos.Next(dir);
                    var type = (SlotType)UnityEngine.Random.Range(1, 4);
                    GenerateSlot(newPos, dir, type);
                    
                    // 如果随机到了战斗就接着再生成
                    if (type == SlotType.Battle) 
                    {
                        GenrateAdditionalRoomRecursive(newPos, chance, reduce);
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

    private LabyrinthSlot GenerateSlot(SlotPosition pos, SlotDirection dir, SlotType type) 
    {
        var slot = GameObject.Instantiate(_slotPrefab, transform);
        var slotComp = slot.GetComponent<LabyrinthSlot>();
        slotComp.Init(dir, type);
        slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.X * INTERVAL, pos.Y * INTERVAL);
        _slotDic.Add(pos, slotComp);
        return slotComp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
