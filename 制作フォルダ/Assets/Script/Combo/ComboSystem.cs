using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    [SerializeField] string comboWord;
    [SerializeField] GameObject comboObj;
    private TextMeshProUGUI comboText;
    int counter = 0;
    private float conboTime = 4f;
    private float timer = 0f;
    public static int counterMax;

    private void Awake()
    {
        comboText = comboObj.GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        comboText.enabled = false;
    }

    public void FixedUpdate()
    {
        CountMove();
    }
    private void CountMove()
    {
        Calculationsystem.Combocalculation(comboText, counter, comboWord);//�R���{�̌v�Z�N���X

        if (counter > 0)
        {
            timer += Time.deltaTime;
            Show();
            if (timer > conboTime)//n�b�𒴂�����R���{���O
            {
                ResetCombo();
            }
        }

        if (counter > counterMax)//�J�E���^�[�̍ő�l�������p��
        {
            counterMax = counter;
            Debug.Log(counterMax);
        }
    }
    public void IncreaseCombo()
    {
        counter++;
        timer = 0f;//�R���{������^�C�}�[���Z�b�g
        Show();
    }
     public void Show()
    {
        comboText.enabled = true;
    }
    void ResetCombo()
    {
        counter = 0;
        timer = 0f;
        comboText.enabled = false;
    }

    public static int counterMaxCnt()
    {
        return counterMax;//�Ō�̃X�R�A���o������
    }
}
