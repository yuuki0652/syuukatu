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
    private int score;//スコアを入れる変数
    private float scoreConbo = 100;//元スコア
    private float scoreMagnification = 1.0f;//スコアの倍率 
    private float ScoreTime = 4f;//コンボと同じ時間
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
        Calculationsystem.Scoredisplay(ScoreText, ScoreWord, score);//クラス参照計算表を使いスコア表示
        if (score > 0)
        {
            timer += Time.deltaTime;
            if (timer > ScoreTime)//n秒を超えたらコンボ数０
            {
                ResetScore();
            }
        }

        ScoreMax = score;//スコアを入れる
    }
    public void IncreaseScore()
    {
        timer = 0f;//コンボしたらタイマーリセット
        if (score > 1)//最初だけ普通の倍率
        {
            scoreMagnification += 0.2f;//攻撃を続けるほど倍率を上げる
        }
        score = Calculationsystem.CalculateScore(score, scoreMagnification, scoreConbo);//スコア計算
    }
    void ResetScore()
    {
        timer = 0f;
        scoreMagnification = 1.0f;//倍率をもとに戻す
    }
    public static int ScorecounterMaxCnt()
    {
        return ScoreMax;//最後のスコアを出すため
    }
}
