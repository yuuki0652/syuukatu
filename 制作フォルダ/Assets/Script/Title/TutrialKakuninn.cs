using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutrialKakuninn : MonoBehaviour
{
    // Start is called before the first frame update
    private Toggle togle;

    public static bool TitleTutrial1;//�ŏ��̃^�C�g���`���[�g���A�����΂����߂ɍ����
    public static bool TitleTutrial2;
    public static bool TitleTutrial3;
    public static bool TitleTutrial4;
    void Start()
    {
        togle = GetComponent<Toggle>();
        togle.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(togle.isOn)//�`�F�b�N�}�[�N��������Ă���Ƃ��̓`���[�g���A�������΂�
        {
            TitleTutrial1= true;
            TitleTutrial2= true;
            TitleTutrial3= true;
            TitleTutrial4= true;
            Debug.Log("�`���[�g���A�����΂�");
        }
        else
        {
            TitleTutrial1 = false;
            TitleTutrial2 = false;
            TitleTutrial3 = false;
            TitleTutrial4 = false;
            Debug.Log("�`���[�g���A�����΂��Ȃ�");
        }
    }
}
