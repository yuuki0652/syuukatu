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
        if (PlayerSEBolum != slider.value) { PlayerSEBolum = slider.value; }//�X���C�_�[�𓮂�����Bolum���ς��悤�ɂ���
        Debug.Log(PlayerSEBolum);
    }

    public static float GetEnemySEBolum()
    {
        return PlayerSEBolum;//���߂ɐݒ肵�����ʂ����C���Q�[���ɗA������
    }
}
