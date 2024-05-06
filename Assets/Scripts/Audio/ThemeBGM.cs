using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayAudio(AudioManager.Instance.BGMSource, AudioManager.Instance.library.BGM2, 0, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
