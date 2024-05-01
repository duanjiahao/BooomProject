using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SlotDirection 
{
    None = 0,
    Left = 1,
    Right = 2,
    Up = 3,
    Down = 4,
}

public enum SlotType 
{
    Born = 0,
    Battle = 1,
    Event = 2,
    Shop = 3,
    Boss = 4,
}

public class LabyrinthSlot : MonoBehaviour
{
    public Image Left;

    public Image Right;

    public Image Up;

    public Image Down;

    public GameObject IconRoot;

    public Image Boss;

    public Image Battle;

    public Image Event;

    public Image ExploredSlot;

    public Image NewSlot;

    public Button Btn;

    public bool Explored { get; private set; }

    public SlotDirection Direction { get; private set; }

    public SlotType SlotType { get; private set; }

    public List<SlotDirection> ConnectionInfo { get; private set; }

    public void Init(SlotDirection dir, SlotType type) 
    {
        Direction = dir;
        ConnectionInfo = new List<SlotDirection>();
        ConnectionInfo.Add(CommonUtils.GetInverseDirection(dir));
        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);
        Up.gameObject.SetActive(false);
        Down.gameObject.SetActive(false);
        switch (dir)
        {
            case SlotDirection.Left:
                Right.gameObject.SetActive(true);
                break;
            case SlotDirection.Right:
                Left.gameObject.SetActive(true);
                break;
            case SlotDirection.Up:
                Down.gameObject.SetActive(true);
                break;
            case SlotDirection.Down:
                Up.gameObject.SetActive(true);
                break;
        }

        SlotType = type;
        Boss.gameObject.SetActive(false);
        Battle.gameObject.SetActive(false);
        Event.gameObject.SetActive(false);
        switch (type)
        {
            case SlotType.Battle:
                Battle.gameObject.SetActive(true);
                break;
            case SlotType.Event:
                Event.gameObject.SetActive(true);
                break;
            case SlotType.Boss:
                Boss.gameObject.SetActive(true);
                break;
        }

        NewSlot.gameObject.SetActive(true);
        ExploredSlot.gameObject.SetActive(false);
    }

    public void SetExplored() 
    {
        Explored = true;

        NewSlot.gameObject.SetActive(false);
        ExploredSlot.gameObject.SetActive(true);
    }

    public void AddConnection(SlotDirection dir) 
    {
        if (HasConnection(dir)) 
        {
            return;
        }

        switch (dir)
        {
            case SlotDirection.Right:
                Right.gameObject.SetActive(true);
                break;
            case SlotDirection.Left:
                Left.gameObject.SetActive(true);
                break;
            case SlotDirection.Down:
                Down.gameObject.SetActive(true);
                break;
            case SlotDirection.Up:
                Up.gameObject.SetActive(true);
                break;
        }

        ConnectionInfo.Add(dir);
    }

    public bool HasConnection(SlotDirection dir) 
    {
        return ConnectionInfo.Contains(dir);
    }
}
