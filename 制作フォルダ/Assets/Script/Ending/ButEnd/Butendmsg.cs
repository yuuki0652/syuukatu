using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Butendmsg : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [SerializeField] private TMP_FontAsset Testzero;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject BuutonSUM;

    [SerializeField] private AudioSource aud1;
    private AudioSource aud2;
    [SerializeField] private AudioClip BGM2;
    [SerializeField] private AudioClip SE;
    private string msg;
    
    private float Nunbers = 7f;//一回にかかる会話の秒数(7)

    private void Start()
    {
        // 初期状態ではテキストを非表示にする
        textComponent.enabled = false;
        BuutonSUM.active = false;//最初はタイトルへのボタンは隠す
        ShowText();
        aud2 = GetComponent<AudioSource>();
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
        msg = "なんだか眠いな";
        TextChange("msg3", Nunbers);
    }
    private void msg3()
    {
        msg = "・・・・・・・・";
        TextChange("msg4", Nunbers);
    }
    private void msg4()
    {
        aud1.clip = BGM2;//BGM変更
        aud1.Play();//再度音を出す
        msg = "<color=#FF0000>・・プレイヤの意識消失を確認</color>";
        TextChange("msg5", Nunbers);
    }
    private void msg5()
    {
        msg = "<color=#FF0000>これより記憶リセットを開始します</color>";
        TextChange("msg6", Nunbers);
    }
    private void msg6()
    {
        msg = "<color=#FF0000>リセット完了しましたまもなく目覚めます</color>";
        TextChange("msg7", Nunbers);
    }
    private void msg7()
    {
        msg = "うーんここはどこだ";
        TextChange("msg8", Nunbers);
    }
    private void msg8()
    {
        textComponent.font = Testzero;//自体変更
        aud2.PlayOneShot(SE);
        msg = "・・<color=#FF0000>【世界メッセージを受信しました】</color>";
        TextChange("msg9", Nunbers);
    }
    private void msg9()
    {
        textComponent.fontSize = 30;
        msg = "ButEnd";
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
