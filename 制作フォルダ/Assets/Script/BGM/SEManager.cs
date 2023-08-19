using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEManager : MonoBehaviour
{
    public Slider slider;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slider.value = 0.7f; // �X���C�_�[�̏����l��0.7�ɐݒ�
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }
}
