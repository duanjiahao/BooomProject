using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour
{
    [Header("背景音乐")]
        [Tooltip("局外背景音")]
        public AudioClip BGM1;
        [Tooltip("主题背景音")]
        public AudioClip BGM2;
        [Tooltip("战斗背景音")]
        public AudioClip BGM3;
        [Tooltip("商店背景音")]
        public AudioClip BGM4;

    [Header("点击音效")]
        [Tooltip("UI点击音效")]
        public AudioClip click1;
        [Tooltip("出售/购买音效")]
        public AudioClip click2;
        [Tooltip("修理音效")]
        public AudioClip click3;
        [Tooltip("装备音效")]
        public AudioClip click4;

    [Header("战斗音效")]
        [Tooltip("打击音效")]
        public AudioClip effect1;
        [Tooltip("防御音效")]
        public AudioClip effect2;
        [Tooltip("破甲音效")]
        public AudioClip effect3;

    [Header("事件音效")]
        [Tooltip("事件音效")]
        public AudioClip eventEffect;
}
