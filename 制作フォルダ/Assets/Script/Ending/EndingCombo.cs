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
        if (Time.time > 4f) //4�b��Ɏ��s����
        {
            ComboText.text = "�ő�R���{��   " + ComboSystem.counterMaxCnt() + "�R���{".ToString();
        }
    }
}
