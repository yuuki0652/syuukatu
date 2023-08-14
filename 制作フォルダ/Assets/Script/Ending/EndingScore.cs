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
        if (Time.time > 2f) //2�b��Ɏ��s����
        {
            ScoreText.text = "�X�R�A     " + Score.ScorecounterMaxCnt().ToString();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();//�Q�[���I��
        }
    }
}
