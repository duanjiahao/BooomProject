using System;
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
        var bornSlot = GameObject.Instantiate(_slotPrefab, transform);
        var slotComp = bornSlot.GetComponent<LabyrinthSlot>();
        slotComp.Init(SlotDirection.None, SlotType.Born);
        bornSlot.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        var slotPosition = new SlotPosition();
        _slotDic.Add(slotPosition, slotComp);

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
                dir = (SlotDirection)UnityEngine.Random.Range(1, 4);
                next = _lastSlotPosition.Next(dir);

            } while (_slotDic.ContainsKey(next));

            SlotType selectType;
            if (i == X)
            {
                selectType = SlotType.Boss;
            }
            else 
            {
                var randomIndex = UnityEngine.Random.Range(0, randomSet.Count - 1);
                selectType = randomSet[randomIndex] ? SlotType.Battle : SlotType.Event;
                randomSet.RemoveAt(randomIndex);
            }

            var newSlot = GameObject.Instantiate(_slotPrefab, transform);
            var newSlotComp = newSlot.GetComponent<LabyrinthSlot>();
            newSlotComp.Init(dir, selectType);
            newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(next.X * INTERVAL, next.Y * INTERVAL);
            _slotDic.Add(next, newSlotComp);

            _lastSlotPosition = next;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
