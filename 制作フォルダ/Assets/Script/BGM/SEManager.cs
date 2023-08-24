using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEManager : MonoBehaviour
{
    public Slider slider;
    AudioSource audioSource;
    private float SEBolum;

    private void Awake()
    {
        SEBolum = TitleSE.GetPlayerSEBolum();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slider.value = SEBolum; //タイトルで決めた音量をスライダーに入れる
        audioSource.volume = SEBolum;//タイトルで決めた音量に設定する
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }
}
