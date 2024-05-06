using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour
{
    [Header("背景音乐")]
        [Tooltip("局外背景音")]
        public AudioClip BGM1;
        [Tooltip("战斗背景音")]
        public AudioClip BGM2;

    [Header("点击音效")]
        [Tooltip("点击音效1")]
        public AudioClip click1;
        [Tooltip("点击音效2")]
        public AudioClip click2;

    [Header("战斗音效")]
        [Tooltip("打击音效1")]
        public AudioClip effect1;
        [Tooltip("打击音效2")]
        public AudioClip effect2;
}
