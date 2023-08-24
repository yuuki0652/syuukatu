using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EndingBGMGet : MonoBehaviour
{
    private float EndingBGM;
    private AudioSource aud;

    private void Awake()
    {
        EndingBGM = MainGameBgmManager.GetMainGameBGMBolum();
    }
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.volume = EndingBGM;
    }
}
