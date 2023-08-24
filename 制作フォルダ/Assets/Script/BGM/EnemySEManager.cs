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
        slider.value = EnemySEBolum; //�^�C�g���Ō��߂����ʂ��X���C�_�[�ɓ����
        audioSource.volume = EnemySEBolum;//�^�C�g���Ō��߂����ʂɐݒ肷��
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }
}
