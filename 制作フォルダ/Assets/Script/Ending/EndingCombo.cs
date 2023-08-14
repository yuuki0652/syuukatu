using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingCombo : MonoBehaviour
{

    private TextMeshProUGUI ComboText;
    //[SerializeField] private GameObject endingScoreText;
    void Start()
    {
        ComboText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 4f) //4秒後に実行する
        {
            ComboText.text = "最大コンボ数   " + ComboSystem.counterMaxCnt() + "コンボ".ToString();
        }
    }
}
