using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TItleEnemySE : MonoBehaviour
{
    public Slider slider;
    public static float PlayerSEBolum;

    private void Awake()
    {
        PlayerSEBolum = 0.7f;
    }

    void Start()
    {
        slider.value = PlayerSEBolum; 
    }
    private void Update()
    {
        if (PlayerSEBolum != slider.value) { PlayerSEBolum = slider.value; }//スライダーを動かすとBolumも変わるようにした
        Debug.Log(PlayerSEBolum);
    }

    public static float GetEnemySEBolum()
    {
        return PlayerSEBolum;//初めに設定した音量をメインゲームに輸送する
    }
}
