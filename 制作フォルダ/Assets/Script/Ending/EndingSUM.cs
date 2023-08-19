using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingSUM : MonoBehaviour
{

    private TextMeshProUGUI SUMText;
    public int scoreConboSUM;
    [SerializeField] private GameObject NextButton;
    void Start()
    {
        SUMText = GetComponent<TextMeshProUGUI>();
        scoreConboSUM = 0;//���Z�b�g����  
        scoreConboSUM = (Score.ScorecounterMaxCnt() + (ComboSystem.counterMaxCnt() * 250));
        NextButton.active = false;

        Cursor.visible = true;//�J�[�\�����o��
        Cursor.lockState = CursorLockMode.Confined;// �J�[�\������ʓ��œ�������
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 6f) //6�b��Ɏ��s����
        {
            SUMText.text = "���v   " + scoreConboSUM .ToString();
            NextButton.active = true;
        }
    }
}
