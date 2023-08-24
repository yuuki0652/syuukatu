using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSE : MonoBehaviour
{
    public Slider slider;
    AudioSource audioSource;
    public static float PlayerSEBolum;

    private void Awake()
    {
        PlayerSEBolum = 0.5f;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slider.value = PlayerSEBolum; // スライダーの初期値を0.3に設定
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }
    private void Update()
    {
        if (PlayerSEBolum != slider.value) { PlayerSEBolum = slider.value; }//スライダーを動かすとBolumも変わるようにした
        Debug.Log(PlayerSEBolum);
    }

    public static float GetPlayerSEBolum()
    {
        return PlayerSEBolum;//初めに設定した音量をメインゲームに輸送する
    }
}
