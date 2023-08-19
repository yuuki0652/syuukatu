using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class StartMainGameMozi : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    private string msg;
    private float displayDuration = 5f; // �\������
    [SerializeField] private Player3 player3;
    [SerializeField]Event1 event1;//�`���[�g���A�����N���A�����������h���S���̂��ƂɌ��������߂ɕK�v

    private float NunbersTutorial1 = 2f;//���ɂ������b�̕b��(2)
    private float Nunbers = 5f;//���ɂ������b�̕b��(5)
    private float NunbersTutorial4 = 7f;//���ɂ������b�̕b��(7)

    private bool Tutorial1Clrear;//���̃X�N���v�g���炫���`���[�g���A�����i�̒m�点���󂯎��
    private bool Tutorial2Clrear;
    private bool Tutorial3Clrear;
    private bool Tutorial4Clrear;

    private AudioSource aud;
    [SerializeField] private AudioClip CorrectMusic;
    [SerializeField] private AudioClip ReceiveMessage;
    //��x�̂ݎ��s������ 
    private bool stop1=false;//�`���[�g���A���P����x�������s
    private bool stop2 = false;//�`���[�g���A��2����x�������s
    private bool stop3 = false;//�`���[�g���A��3����x�������s
    private bool stop4 = false;//�`���[�g���A��4����x�������s
    private void Start()
    {
        // ������Ԃł̓e�L�X�g���\���ɂ���
        textComponent.enabled = false;
        aud = GetComponent<AudioSource>();
        TextChange("ShowText", NunbersTutorial1);
    }
    private void Update()
    {
        TextConversion();
        Tutorial1Clrearmove();//�`���[�g���A��2���N���A�������Ď�������
        Tutorial2Clrearmove();//�`���[�g���A��2���N���A�������Ď�������
        Tutorial3Clrearmove();//�`���[�g���A��3���N���A�������Ď�������
        Tutorial4Clrearmove();//�`���[�g���A��4���N���A�������Ď�������
    }
    private void Tutorial1Clrearmove()
    {
        if (!stop1)
        {
            Tutorial1Clrear = player3.Tutorial1Goukaku;

            if (Tutorial1Clrear)
            {
                aud.PlayOneShot(CorrectMusic);
                TextChange("msg7", 2.0f);
                stop1 = true;
                Debug.Log("�`���[�g���A���P���i");
            }
        }
    }
    private void Tutorial2Clrearmove()
    {
        if (!stop2)
        {
            Tutorial2Clrear = player3.Tutorial2Goukaku;

            if (Tutorial2Clrear)
            {
                aud.PlayOneShot(CorrectMusic);
                TextChange("msg8", Nunbers);
                Debug.Log("�`���[�g���A2���i");
                stop2 = true;
            }
        }
    }
    private void Tutorial3Clrearmove()
    {
        if (!stop3)
        {
            Tutorial3Clrear = player3.Tutorial3Goukaku;

            if (Tutorial3Clrear)
            {
                aud.PlayOneShot(CorrectMusic);
                TextChange("msg9", Nunbers);
                Debug.Log("�`���[�g���A3���i");
                stop3= true;
            }
        }
    }
    private void Tutorial4Clrearmove()
    {
        if (!stop4)
        {
            Tutorial4Clrear = player3.Tutorial4Goukaku;

            if (Tutorial4Clrear)
            {
                aud.PlayOneShot(CorrectMusic);
                TextChange("msg15", Nunbers);
                Debug.Log("�`���[�g���A4���i");
                stop4 = true;
            }
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
        msg = "�y���E���b�Z�[�W����M���܂����z";
        aud.PlayOneShot(ReceiveMessage);
        TextChange("msg1", Nunbers);
    }
    private void msg1()
    {
       msg = "���E�E�������邩";
       TextChange("msg2", Nunbers);
    }
    private void msg2()
    {
        msg = "�ǂ����N�͂��̐��E�ɖ�������ł��܂����炵��";
        TextChange("msg3", Nunbers);
    }
    private void msg3()
    {
        msg = "���̐��E����E�o���邽�߂ɂ́A���̌������ɂ���<color=#FF0000>�h���S��</color>�𓢔���" +
            "\n�܂�<color=#FF0000>�X�R�A���P���ȏ�</color>�҂��Ȃ���Ό��̐��E�ɂ͖߂�Ȃ�";
        TextChange("msg4", Nunbers);
    }
    private void msg4()
    {
        msg = "�����Ȃ肱��Ȃ��Ƃ������ł邩������Ȃ����{����";
        TextChange("msg5", Nunbers);
    }
    private void msg5()
    {
        msg = "�܂��N�ɂ͂��̑̂̓��������������鎄�̌����ʂ�ɂ��Ă���";
        TextChange("msg6", Nunbers);
    }
    private void msg6()
    {
        msg = "�܂��ړ����@��<color=#FF0000>W�EA�ES�ED</color>�œ������Ƃ��o���铮���Ă݂Ă���";
        player3.Tutorial1 = true;//������悤�ɂȂ�
    }
    private void msg7()
    {
        msg = "��o��������<color=#FF0000>�}�E�X�E�N���b�N�𒷉���</color>���Ȃ���}�E�X�𓮂����Ă݂Ă���J�����������͂���";
        player3.Tutorial2 = true;//�J�������_��������
    }
    private void msg8()
    {
        msg = "��������Ȃ��Ȏ��ɉ���̗��K��<color=#FF0000>�X�y�[�X</color>�������Ɖ���o�������Ă݂Ă���";
        player3.Tutorial3 = true;//����\
    }
    private void msg9()
    {
        msg = "���߂łƂ�������o���Ă��邾���������Ƃ��ɏ������ӂ��K�v������b�𕷂��Ă���";
        TextChange("msg10", Nunbers);
    }
    private void msg10()
    {
        msg = "����ɂ͉E��ɂ���Q�[�W���K�v�����̃Q�[�W��<color=#FF0000>\n30%</color>�g�p���邱�Ƃŉ�����ł���";
        TextChange("msg11",NunbersTutorial4);
    }
    private void msg11()
    {
        msg = "�@���̂����N�Ȃ�����킩���Ă���Ǝv����<color=#FF0000>30%</color>�ȉ��ɂȂ�Ή���͂ł��Ȃ�";
        TextChange("msg12", NunbersTutorial4);
    }
    private void msg12()
    {
        msg = "�������S���Ă���Q�[�W�͎��ԂƋ��ɉ񕜂��Ă���";
        TextChange("msg13", Nunbers);
    }
    private void msg13()
    {
        msg = "����̘b���I������̂ł��悢��Ō�̐�����";
        TextChange("msg14", Nunbers);
    }
    private void msg14()
    {
        msg = "<color=#FF0000>�}�E�X�����N���b�N</color>���Ă݂Ăق���";
        player3.Tutorial4 = true;//�U���\
    }
    private void msg15()
    {
        msg = "���ꂪ�h���S���ւ̍U����i<color=#FF0000>�}�W�b�N�{�[��</color>���I";
        TextChange("msg16", Nunbers);
    }
    private void msg16()
    {
        msg = "<color=#FF0000>�}�W�b�N�{�[��</color>�͌��ĂΓG�Ɏ����I�ɒǐՂ��Ă����";
        TextChange("msg17", Nunbers);
    }
    private void msg17()
    {
        msg = "�����K��������킯�ł͂Ȃ����璍�ӂ��Ă���";
        TextChange("msg18", Nunbers);
    }
    private void msg18()
    {
        msg = "�܂�<color=#FF0000>�}�W�b�N�{�[��</color>�͓G�ɘA�����ĐG����<color=#FF0000>�U���͂��オ��</color>" +
            "\n������ʉE�ɏo�Ă���R���{���r�؂��ƍU���͌��ɖ߂邩�璍�ӂ��Ă���";
        TextChange("msg19", NunbersTutorial4);
    }
    private void msg19()
    {
        msg = "�����͈ȏゾ";
        TextChange("msg20", Nunbers);
    }
    private void msg20()
    {
        msg = "���b�Z�[�W�I������ɑ̂𓖂Ă�΃h���S���Ƒΐ�ł���ł͌������F��";
        TextChange("lastmsg", Nunbers);
    }
    private void lastmsg()
    {
        msg = "�y���E���b�Z�[�W�I���z";
        aud.PlayOneShot(ReceiveMessage);
        TextChange("HideText", Nunbers);
        Debug.Log("�Ō�̕����\��");
    }
    private void TextChange(string functionname,float nunber)
    {
        Invoke(functionname, nunber);
    }
    private void HideText()
    {
        event1.AllCrearTutorial = true;//�`���[�g���A���S�ăN���A
        msg = " ";
        Destroy(this.gameObject,2.0f);//�S�Ă��I�������j������
    }
}
