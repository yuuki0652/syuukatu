using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource aud;
    public AudioClip ConboVoice1, ConboVoice2,ConboVoic12, KnockBackvoice1,KnockBackvoice2, escape, Die4;
    private int randomIndex;
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }
    private void SetRandomIndex()// âπê∫ÇÉâÉìÉ_ÉÄÇ…ÇµÇΩÇ¢Ç∆Ç´égÇ§ä÷êî
    {
        randomIndex = Random.Range(0, 2);
    }
    public void Conbo1()
    {
        aud.PlayOneShot(ConboVoice1);
    }
    public void Conbo2()
    {
        SetRandomIndex();
        if (randomIndex == 0)
        {
            aud.PlayOneShot(ConboVoice2);
        }
        else
        {
            aud.PlayOneShot(ConboVoic12);
        }
    }
    public void KnockBackVoice()
    {
        SetRandomIndex();
        if (randomIndex == 0)
        {
            aud.PlayOneShot(KnockBackvoice1);
        }
        else
        {
            aud.PlayOneShot(KnockBackvoice2);
        }
    }

    public void escapeVoice()
    {
        aud.PlayOneShot(escape);
    }
    public void DieVoice()
    {
        aud.PlayOneShot(Die4);
    }
}
