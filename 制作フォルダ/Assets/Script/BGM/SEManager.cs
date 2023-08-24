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
        slider.value = SEBolum; //�^�C�g���Ō��߂����ʂ��X���C�_�[�ɓ����
        audioSource.volume = SEBolum;//�^�C�g���Ō��߂����ʂɐݒ肷��
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }
}
