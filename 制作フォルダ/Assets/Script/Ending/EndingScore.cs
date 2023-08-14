using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingScore : MonoBehaviour
{
    private TextMeshProUGUI ScoreText;
   //[SerializeField] private GameObject endingScoreText;
    void Start()
    {
       ScoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 2f) //2秒後に実行する
        {
            ScoreText.text = "スコア     " + Score.ScorecounterMaxCnt().ToString();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();//ゲーム終了
        }
    }
}
