using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(AudioLibrary))]
public class AudioManager : SingleMono<AudioManager>
{
    public AudioLibrary library;

    [Tooltip("背景音乐播放器")]
    public AudioSource BGMSource;
    [Tooltip("UI点击音效播放器")]
    public AudioSource UIClickSource;
    [Tooltip("战斗音效播放器")]
    public AudioSource BattleEffectSource;

    private void Start()
    {
        library = GetComponent<AudioLibrary>();
    }

    ///样例：AudioManager.Instance.PlayAudio(BGMSource,AudioManager.Instance.library.BGM1,0,true);
    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="source">谁播放</param>
    /// <param name="clip">播放什么</param>
    /// <param name="delay">延迟多少，不延迟填0f</param>
    /// <param name="isloop">是否循环，不循环填false</param>
    public void PlayAudio(AudioSource source,AudioClip clip,float delay,bool isloop)
    {
        source.clip = clip;
        source.loop = isloop;
        source.Play((ulong)delay);
    }
}
