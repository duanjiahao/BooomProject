using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSelection : MonoBehaviour
{
    public Button btn;

    public Text name;

    public void RefreshUI(string txt)
    {
        name.text = txt;
    }
}
