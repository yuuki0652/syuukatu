using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ComboSystem;

public class Score : MonoBehaviour
{
    [SerializeField] string ScoreWord;
    private GameObject ScoreObj;
    private TextMeshProUGUI ScoreText;
    private int score;//�X�R�A������ϐ�
    private float scoreConbo = 100;//���X�R�A
    private float scoreMagnification = 1.0f;//�X�R�A�̔{�� 
    private float ScoreTime = 4f;//�R���{�Ɠ�������
    private float timer = 0f;
    public static int ScoreMax;
    private void Awake()
    {
        ScoreObj = GameObject.FindWithTag("ScoreTag");
        ScoreText = ScoreObj.GetComponent<TextMeshProUGUI>();
    }
    public void FixedUpdate()
    {
        ScoreSUM();
    }
    private void ScoreSUM()
    {
        Calculationsystem.Scoredisplay(ScoreText, ScoreWord, score);//�N���X�Q�ƌv�Z�\���g���X�R�A�\��
        if (score > 0)
        {
            timer += Time.deltaTime;
            if (timer > ScoreTime)//n�b�𒴂�����R���{���O
            {
                ResetScore();
            }
        }

        ScoreMax = score;//�X�R�A������
    }
    public void IncreaseScore()
    {
        timer = 0f;//�R���{������^�C�}�[���Z�b�g
        if (score > 1)//�ŏ��������ʂ̔{��
        {
            scoreMagnification += 0.2f;//�U���𑱂���قǔ{�����グ��
        }
        score = Calculationsystem.CalculateScore(score, scoreMagnification, scoreConbo);//�X�R�A�v�Z
    }
    void ResetScore()
    {
        timer = 0f;
        scoreMagnification = 1.0f;//�{�������Ƃɖ߂�
    }
    public static int ScorecounterMaxCnt()
    {
        return ScoreMax;//�Ō�̃X�R�A���o������
    }
}
