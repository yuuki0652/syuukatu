using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioSource aud;
    [SerializeField]
    private AudioClip Boss1;
    [SerializeField]
    private AudioClip Boss2;
    [SerializeField]
    private bool Boss1Die = false;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.clip = Boss1;
        aud.Play();
    }

    void Update()
    {
        switch (Boss1Die)
        {
            case false://�{�X�P������
                if (aud.clip != Boss1)
                {
                    aud.clip = Boss1;
                    aud.Play();
                }
                break;
            case true:
                if (aud.clip != Boss2)//�{�X��ǉ��o������s��
                {
                    aud.clip = Boss2;
                    aud.Play();
                }
                break;
        }
    }
}
