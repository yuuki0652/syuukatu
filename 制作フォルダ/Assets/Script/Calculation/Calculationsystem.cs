using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calculationsystem//�v�Z�V�X�e��
{
    public static void Combocalculation(TextMeshProUGUI conboText_, int comboCount, string comboWord_)//�R���{�\��
    {
        conboText_.text = comboCount + comboWord_;
    }

    public static void Scoredisplay(TextMeshProUGUI ScoreText_, string ScoreWord_, int ScoreCount_)//�X�R�A�\��
    {
        ScoreText_.text = ScoreWord_ + ScoreCount_;
    }
    public static int CalculateScore(int score, float scoreMagnification, float scoreConbo)
    {
        return score + Mathf.RoundToInt(scoreConbo * scoreMagnification);//�l�̌ܓ��Ɍv�Z����
    }
}
