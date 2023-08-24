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
    public static float GetMainGameBGMBolum()
    {
        return BGMBolum;//‰‚ß‚Éİ’è‚µ‚½‰¹—Ê‚ğƒƒCƒ“ƒQ[ƒ€‚É—A‘—‚·‚é
    }
}
