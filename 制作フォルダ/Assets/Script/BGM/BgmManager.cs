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
        slider.value = Bolum; //�^�C�g���Ō��߂����ʂɐݒ肷��
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }
    private void Update()
    {
        if(Bolum != slider.value) { Bolum = slider.value; }//�X���C�_�[�𓮂�����Bolum���ς��悤�ɂ���
        Debug.Log("�^�C�g�����݂�BGM�̉��ʂ�"+Bolum);
    }

    public static float GetBolum()
    {
        return Bolum;//���߂ɐݒ肵�����ʂ����C���Q�[���ɗA������
    }
}
