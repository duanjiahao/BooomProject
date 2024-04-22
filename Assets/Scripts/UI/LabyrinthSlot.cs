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
    Boss = 3,
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

    public bool Explored { get; private set; }

    public SlotDirection Direction { get; private set; }

    public void Init(SlotDirection dir, SlotType type) 
    {
        Direction = dir;
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
}
