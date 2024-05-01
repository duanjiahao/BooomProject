using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Items_Panel : MonoBehaviour
{
    public GameObject descriptionPanel; // 引用详细介绍面板的GameObject
    private DescriptionPanel desPanel;

    public ToggleGroup toggleGroup;
    public Transform Slots;
    private Toggle[] toggles; // 引用Toggle组件
    public Item[] items;
    private bool isIn;

    private void Start()
    {
        toggles = Slots.GetComponentsInChildren<Toggle>();

        // 关闭详细介绍面板
        descriptionPanel.SetActive(false);

        for (int i = 0; i < toggles.Length; i++)
        {
            // 监听Toggle的状态改变事件
            toggles[i].onValueChanged.AddListener(OnToggleValueChanged);
        }

        desPanel = descriptionPanel.GetComponent<DescriptionPanel>();
    }

    private void OnToggleValueChanged(bool isOn)
    {
        // 根据Toggle状态决定是否显示详细介绍面板
        descriptionPanel.SetActive(isOn);
    }

    private void Update()
    {
        descriptionPanel.SetActive(isIn);
        
        Vector2 mousePosition = Input.mousePosition;
        for (int i = 0; i < toggles.Length; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(toggles[i].GetComponent<RectTransform>(), mousePosition))
            {
                descriptionPanel.GetComponent<RectTransform>().position = new Vector2(toggles[i].GetComponent<RectTransform>().position.x,230);
                //desPanel.item = toggles[i];
                //todo:把选中的物品的SO赋值给desPanel
                isIn = true;
                return;
            }
        }
        isIn = false;
    }
}
