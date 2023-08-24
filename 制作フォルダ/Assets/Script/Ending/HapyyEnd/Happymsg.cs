using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Happymsg : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject BuutonSUM;
    private string msg;
    
    private float Nunbers = 7f;//一回にかかる会話の秒数(7)

    private void Start()
    {
        // 初期状態ではテキストを非表示にする
        textComponent.enabled = false;
        BuutonSUM.SetActive(false);//最初はタイトルへのボタンは隠す
        ShowText();
    }
    private void Update()
    {
        TextConversion();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();//ゲーム終了
        }
    }

    private void TextConversion()
    {
        textComponent.text = msg;
    }

    private void ShowText()
    {
        firstmsg();
        textComponent.text = msg;
        textComponent.enabled = true; // テキストを表示
    }
    private void firstmsg()
    {
        msg = "こ・・ここは";
        TextChange("msg1", Nunbers);
    }
    private void msg1()
    {
        msg = "俺は戻ることができたのか";
        TextChange("msg2", Nunbers);
    }
    private void msg2()
    {
        msg = "ってもうこんな時間か速く家に帰ろ";
        TextChange("msg3", Nunbers);
    }
    private void msg3()
    {
        msg = "・・・・・・・・";
        TextChange("msg4", Nunbers);
    }
    private void msg4()
    {
        textComponent.fontSize = 30;
        msg = "<color=#FF0000>また会おう若きゲームプレイヤー達</color>";
        TextChange("msg5", Nunbers);
    }
    private void msg5()
    {
        msg = "HappyEND";
        TextChange("HideText", Nunbers);
    }
    private void TextChange(string functionname, float nunber)
    {
        Invoke(functionname, nunber);
    }
    private void HideText()
    {
        msg = " ";
        BuutonSUM.active = true;
        Destroy(image);//全てが終わったら破棄する
    }
}
