using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

/// <summary>
/// ��ʱ��ķ����ʽ��Э�̷���
///ʹ��ʱ��Ҫʹ��StartCoroutine��������StartCoroutine(Delayer.DelayedLambdaOnEndOfSeconds(() => { scanAnim.SetActive(false); }, 0.5f));
/// </summary>
public class Delayer
{
    //�ڵȴ������Ժ�ִ����ķ����ʽ������
    public static IEnumerator DelayedLambdaOnEndOfSeconds(System.Action action, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        action?.Invoke();
    }

    //�ڵ�ǰִ֡�����Ժ�ִ����ķ����ʽ������
    public static IEnumerator DelayedLambdaOnEndOfFrame(System.Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }
}
