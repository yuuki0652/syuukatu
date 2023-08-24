using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySEManager : MonoBehaviour
{
    public Slider slider;
    AudioSource audioSource;
    private float EnemySEBolum;

    private void Awake()
    {
        EnemySEBolum = TItleEnemySE.GetEnemySEBolum();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slider.value = EnemySEBolum; //タイトルで決めた音量をスライダーに入れる
        audioSource.volume = EnemySEBolum;//タイトルで決めた音量に設定する
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }
}
