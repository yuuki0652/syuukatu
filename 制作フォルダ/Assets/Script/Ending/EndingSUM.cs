using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingSUM : MonoBehaviour
{

    private TextMeshProUGUI SUMText;
    public int scoreConboSUM;
    [SerializeField] private GameObject NextButton;
    void Start()
    {
        SUMText = GetComponent<TextMeshProUGUI>();
        scoreConboSUM = 0;//リセットする  
        scoreConboSUM = (Score.ScorecounterMaxCnt() + (ComboSystem.counterMaxCnt() * 250));
        NextButton.SetActive(false);

        Cursor.visible = true;//カーソルを出す
        Cursor.lockState = CursorLockMode.Confined;// カーソルを画面内で動かせる
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 6f) //6秒後に実行する
        {
            SUMText.text = "合計   " + scoreConboSUM .ToString();
            NextButton.active = true;
        }
    }
}
