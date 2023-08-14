using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEManager : MonoBehaviour
{
    public Slider slider;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slider.value = 0.7f; // スライダーの初期値を0.7に設定
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }
}
