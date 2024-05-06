using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour
{
    [Header("��������")]
        [Tooltip("���ⱳ����")]
        public AudioClip BGM1;
        [Tooltip("���ⱳ����")]
        public AudioClip BGM2;
        [Tooltip("ս��������")]
        public AudioClip BGM3;
        [Tooltip("�̵걳����")]
        public AudioClip BGM4;

    [Header("�����Ч")]
        [Tooltip("UI�����Ч")]
        public AudioClip click1;
        [Tooltip("����/������Ч")]
        public AudioClip click2;
        [Tooltip("������Ч")]
        public AudioClip click3;
        [Tooltip("װ����Ч")]
        public AudioClip click4;

    [Header("ս����Ч")]
        [Tooltip("�����Ч")]
        public AudioClip effect1;
        [Tooltip("������Ч")]
        public AudioClip effect2;
        [Tooltip("�Ƽ���Ч")]
        public AudioClip effect3;

    [Header("�¼���Ч")]
        [Tooltip("�¼���Ч")]
        public AudioClip eventEffect;
}
