using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Butendmsg : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [SerializeField] private TMP_FontAsset Testzero;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject BuutonSUM;

    [SerializeField] private AudioSource aud1;
    private AudioSource aud2;
    [SerializeField] private AudioClip BGM2;
    [SerializeField] private AudioClip SE;
    private string msg;
    
    private float Nunbers = 7f;//���ɂ������b�̕b��(7)

    private void Start()
    {
        // ������Ԃł̓e�L�X�g���\���ɂ���
        textComponent.enabled = false;
        BuutonSUM.active = false;//�ŏ��̓^�C�g���ւ̃{�^���͉B��
        ShowText();
        aud2 = GetComponent<AudioSource>();
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
        msg = "�Ȃ񂾂�������";
        TextChange("msg3", Nunbers);
    }
    private void msg3()
    {
        msg = "�E�E�E�E�E�E�E�E";
        TextChange("msg4", Nunbers);
    }
    private void msg4()
    {
        aud1.clip = BGM2;//BGM�ύX
        aud1.Play();//�ēx�����o��
        msg = "<color=#FF0000>�E�E�v���C���̈ӎ��������m�F</color>";
        TextChange("msg5", Nunbers);
    }
    private void msg5()
    {
        msg = "<color=#FF0000>������L�����Z�b�g���J�n���܂�</color>";
        TextChange("msg6", Nunbers);
    }
    private void msg6()
    {
        msg = "<color=#FF0000>���Z�b�g�������܂����܂��Ȃ��ڊo�߂܂�</color>";
        TextChange("msg7", Nunbers);
    }
    private void msg7()
    {
        msg = "���[�񂱂��͂ǂ���";
        TextChange("msg8", Nunbers);
    }
    private void msg8()
    {
        textComponent.font = Testzero;//���̕ύX
        aud2.PlayOneShot(SE);
        msg = "�E�E<color=#FF0000>�y���E���b�Z�[�W����M���܂����z</color>";
        TextChange("msg9", Nunbers);
    }
    private void msg9()
    {
        textComponent.fontSize = 30;
        msg = "ButEnd";
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
