using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(AudioLibrary))]
public class AudioManager : SingleMono<AudioManager>
{
    public AudioLibrary library;

    [Tooltip("�������ֲ�����")]
    public AudioSource BGMSource;
    [Tooltip("UI�����Ч������")]
    public AudioSource UIClickSource;
    [Tooltip("ս����Ч������")]
    public AudioSource BattleEffectSource;

    private void Start()
    {
        library = GetComponent<AudioLibrary>();
    }

    ///������AudioManager.Instance.PlayAudio(BGMSource,AudioManager.Instance.library.BGM1,0,true);
    /// <summary>
    /// ������Ƶ
    /// </summary>
    /// <param name="source">˭����</param>
    /// <param name="clip">����ʲô</param>
    /// <param name="delay">�ӳٶ��٣����ӳ���0f</param>
    /// <param name="isloop">�Ƿ�ѭ������ѭ����false</param>
    public void PlayAudio(AudioSource source,AudioClip clip,float delay,bool isloop)
    {
        source.clip = clip;
        source.loop = isloop;
        source.Play((ulong)delay);
    }
}
