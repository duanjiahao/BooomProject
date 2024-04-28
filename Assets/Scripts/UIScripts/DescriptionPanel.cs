using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DescriptionPanel : MonoBehaviour
{
    public Text description;
    public Button use_Btn;
    public ItemSO item;

    private void OnEnable()
    {
        SynchronizeData(item);

        if (!item.isGlobalUse)
        {
            use_Btn.gameObject.SetActive(false);

            //区域变化
            description.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, 0f);
            description.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0f, 0f);
            description.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, 0f);
            description.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 0f);
        }
        else
        {
            use_Btn.gameObject.SetActive(true);

            //区域变化
            description.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, 0f);
            description.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0f, 0f);
            description.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, 0f);
            description.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 0f);
        }
    }

    /// <summary>
    /// 同步数据
    /// </summary>
    /// <param name="item"></param>
    private void SynchronizeData(ItemSO item)
    {
        description.text = item.description;
    }
}
