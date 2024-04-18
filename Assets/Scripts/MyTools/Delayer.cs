using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

/// <summary>
/// 延时拉姆达表达式的协程方法
///使用时需要使用StartCoroutine开启，如StartCoroutine(Delayer.DelayedLambdaOnEndOfSeconds(() => { scanAnim.SetActive(false); }, 0.5f));
/// </summary>
public class Delayer
{
    //在等待几秒以后执行拉姆达表达式的内容
    public static IEnumerator DelayedLambdaOnEndOfSeconds(System.Action action, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        action?.Invoke();
    }

    //在当前帧执行完以后执行拉姆达表达式的内容
    public static IEnumerator DelayedLambdaOnEndOfFrame(System.Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }
}
