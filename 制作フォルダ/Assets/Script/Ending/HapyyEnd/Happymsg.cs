using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Happymsg : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject BuutonSUM;
    private string msg;
    
    private float Nunbers = 7f;//���ɂ������b�̕b��(7)

    private void Start()
    {
        // ������Ԃł̓e�L�X�g���\���ɂ���
        textComponent.enabled = false;
        BuutonSUM.SetActive(false);//�ŏ��̓^�C�g���ւ̃{�^���͉B��
        ShowText();
    }
    private void Update()
    {
        TextConversion();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();//�Q�[���I��
        }
    }

    private void TextConversion()
    {
        textComponent.text = msg;
    }

    private void ShowText()
    {
        firstmsg();
        textComponent.text = msg;
        textComponent.enabled = true; // �e�L�X�g��\��
    }
    private void firstmsg()
    {
        msg = "���E�E������";
        TextChange("msg1", Nunbers);
    }
    private void msg1()
    {
        msg = "���͖߂邱�Ƃ��ł����̂�";
        TextChange("msg2", Nunbers);
    }
    private void msg2()
    {
        msg = "���Ă�������Ȏ��Ԃ������ƂɋA��";
        TextChange("msg3", Nunbers);
    }
    private void msg3()
    {
        msg = "�E�E�E�E�E�E�E�E";
        TextChange("msg4", Nunbers);
    }
    private void msg4()
    {
        textComponent.fontSize = 30;
        msg = "<color=#FF0000>�܂�����Ⴋ�Q�[���v���C���[�B</color>";
        TextChange("msg5", Nunbers);
    }
    private void msg5()
    {
        msg = "HappyEND";
        TextChange("HideText", Nunbers);
    }
    private void TextChange(string functionname, float nunber)
    {
        Invoke(functionname, nunber);
    }
    private void HideText()
    {
        msg = " ";
        BuutonSUM.active = true;
        Destroy(image);//�S�Ă��I�������j������
    }
}
