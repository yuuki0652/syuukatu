using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameBgmManager : MonoBehaviour
{
    public Slider slider;
    AudioSource audioSource;
    private  float Bolum;

    private void Awake()
    {
        Bolum = BgmManager.GetBolum();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        slider.value = Bolum;
        audioSource.volume = slider.value;
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
        Debug.Log(Bolum);
    }
}
