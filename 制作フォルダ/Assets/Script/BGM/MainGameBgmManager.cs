using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameBgmManager : MonoBehaviour
{
    public Slider slider;
    AudioSource audioSource;
    public static float BGMBolum;

    private void Awake()
    {
        BGMBolum = BgmManager.GetBolum();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slider.value = BGMBolum;
        audioSource.volume = slider.value;
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
        Debug.Log(BGMBolum);
    }

    private void Update()
    {
        if (BGMBolum != slider.value) { BGMBolum = slider.value; }//�X���C�_�[�𓮂�����Bolum���ς��悤�ɂ���
        Debug.Log("���݂�BGM�̉��ʂ�"+BGMBolum);
    }
    public static float GetMainGameBGMBolum()
    {
        return BGMBolum;//���߂ɐݒ肵�����ʂ����C���Q�[���ɗA������
    }
}
