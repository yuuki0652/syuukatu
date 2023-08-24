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
        slider.value = PlayerSEBolum; // �X���C�_�[�̏����l��0.3�ɐݒ�
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }
    private void Update()
    {
        if (PlayerSEBolum != slider.value) { PlayerSEBolum = slider.value; }//�X���C�_�[�𓮂�����Bolum���ς��悤�ɂ���
        Debug.Log("�^�C�g�����݂̃v���C��SE�̉��ʂ�"+PlayerSEBolum);
    }

    public static float GetPlayerSEBolum()
    {
        return PlayerSEBolum;//���߂ɐݒ肵�����ʂ����C���Q�[���ɗA������
    }
}
