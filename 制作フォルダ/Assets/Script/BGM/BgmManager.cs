using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgmManager : MonoBehaviour
{
    public Slider slider;
    AudioSource audioSource;
    public static float Bolum;

    private void Awake()
    {
        Bolum = 0.3f;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slider.value = Bolum; //タイトルで決めた音量に設定する
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }
    private void Update()
    {
        if(Bolum != slider.value) { Bolum = slider.value; }//スライダーを動かすとBolumも変わるようにした
        Debug.Log("タイトル現在のBGMの音量は"+Bolum);
    }

    public static float GetBolum()
    {
        return Bolum;//初めに設定した音量をメインゲームに輸送する
    }
}
