using Cinemachine;
using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static Player3;
using static UnityEngine.Rendering.DebugUI;

public class Player3 : MonoBehaviour
{
    [SerializeField] private PlayerMoveMent movent;

    private GameObject PlayerSt;//�v���C���̒��ɓ����Ă���X�e�[�^�X���Q�Ƃ��邽�߂̕ϐ�

    private int PlayerNowLife;//�v���C���̌��݂�HP������ϐ�

    private int playerlife;//�v���C���̏���HP����ϐ�

    public bool Tutorial1=false;//�ړ��̃`���[�g���A��
    public bool Tutorial1Goukaku = false;//�ړ��̃`���[�g���A�����i

    public bool Tutorial2 = false;//�J�����̃`���[�g���A��
    public bool Tutorial2Goukaku = false;//�J�����̃`���[�g���A�����i

    public bool Tutorial3 = false;//����̃`���[�g���A��
    public bool Tutorial3Goukaku = false;//����̃`���[�g���A�����i

    public bool Tutorial4 = false;//�U���̃`���[�g���A��
    public bool Tutorial4Goukaku = false;//�U���̃`���[�g���A�����i

    void Start()
    {
        playerlife = GetComponent<PlayerStatus>().valu;//�v���C���̏���HP���
        PlayerSt = GameObject.FindWithTag("Player");//�v���C���̃^�O��T��
    }

    void FixedUpdate()
    {
        PlayernowLife();//�ύX���ꂽHP���󂯎��
        movent.evadTime();//�������ɕύX����֐�(PlayerMoveMent����󂯎��)
        if (playerlife > 0)//HP������Ƃ�
        {
            if (Tutorial1)//�ړ��̃`���[�g���A��
            {
                movent.Playermove();//�v���C������Ɋւ���֐�
            }
            if (Tutorial3)//����̃`���[�g���A��
            {
                movent.Evasivel();//����Ɋւ���֐�(PlayerMoveMent����󂯎��)
            }
            movent.cameracon();//�J��������Ɋւ���֐�
            movent.KnockBack();//�m�b�N�o�b�N�Ɋւ���֐�(PlayerMoveMent����󂯎��)
            LifeChange();//HP���ω������Ƃ��ɔ�������֐�
        }
        else//HP��0
        {
            movent.PlayerDie();//���񂾂Ƃ��ɔ�������֐�(PlayerMoveMent����󂯎��)
        }
    }
    private void LifeChange()
    {
        if (playerlife != PlayerNowLife)//�v���C����HP���ύX���ꂽ�ꍇ(�v�Z�����炷���߂̎�)
        {
            playerlife = PlayerNowLife;//����������HP��������
            Debug.Log("�v���C����HP���ύX���ꂽ");
        }
    }
    private void PlayernowLife()
    {
        PlayerNowLife = PlayerSt.GetComponent<PlayerStatus>().Max;//�ύX���ꂽHP���󂯎��
    }
}