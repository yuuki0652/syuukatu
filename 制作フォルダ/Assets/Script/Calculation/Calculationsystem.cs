using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calculationsystem//計算システム
{
    public static void Combocalculation(TextMeshProUGUI conboText_, int comboCount, string comboWord_)//コンボ表示
    {
        conboText_.text = comboCount + comboWord_;
    }

    public static void Scoredisplay(TextMeshProUGUI ScoreText_, string ScoreWord_, int ScoreCount_)//スコア表示
    {
        ScoreText_.text = ScoreWord_ + ScoreCount_;
    }
    public static int CalculateScore(int score, float scoreMagnification, float scoreConbo)
    {
        return score + Mathf.RoundToInt(scoreConbo * scoreMagnification);//四捨五入に計算する
    }
}
