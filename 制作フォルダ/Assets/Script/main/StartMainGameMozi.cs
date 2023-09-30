using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class StartMainGameMozi : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    private string msg;
    private float displayDuration = 5f; // 表示時間
    [SerializeField] private Player3 player3;
    [SerializeField]Event1 event1;//チュートリアルをクリアしたら扉からドラゴンのもとに向かうために必要

    private float NunbersTutorial1 = 2f;//一回にかかる会話の秒数(2)
    private float Nunbers = 5f;//一回にかかる会話の秒数(5)
    private float NunbersTutorial4 = 7f;//一回にかかる会話の秒数(7)

    private bool Tutorial1Clrear;//他のスクリプトからきたチュートリアル合格の知らせを受け取る
    private bool Tutorial2Clrear;
    private bool Tutorial3Clrear;
    private bool Tutorial4Clrear;

    private AudioSource aud;
    [SerializeField] private AudioClip CorrectMusic;
    [SerializeField] private AudioClip ReceiveMessage;
    //一度のみ実行したい 
    private bool stop1=false;//チュートリアル１を一度だけ実行
    private bool stop2 = false;//チュートリアル2を一度だけ実行
    private bool stop3 = false;//チュートリアル3を一度だけ実行
    private bool stop4 = false;//チュートリアル4を一度だけ実行
    private void Start()
    {
        // 初期状態ではテキストを非表示にする
        textComponent.enabled = false;
        aud = GetComponent<AudioSource>();
        TextChange("ShowText", NunbersTutorial1);
    }
    private void Update()
    {
        TextConversion();
        Tutorial1Clrearmove();//チュートリアル2をクリアしたか監視をする
        Tutorial2Clrearmove();//チュートリアル2をクリアしたか監視をする
        Tutorial3Clrearmove();//チュートリアル3をクリアしたか監視をする
        Tutorial4Clrearmove();//チュートリアル4をクリアしたか監視をする
    }
    private void Tutorial1Clrearmove()
    {
        if (!stop1)
        {
            Tutorial1Clrear = player3.Tutorial1Goukaku;

            if (Tutorial1Clrear)
            {
                aud.PlayOneShot(CorrectMusic);
                TextChange("msg7", 2.0f);
                stop1 = true;
                Debug.Log("チュートリアル１合格");
            }
        }
    }
    private void Tutorial2Clrearmove()
    {
        if (!stop2)
        {
            Tutorial2Clrear = player3.Tutorial2Goukaku;

            if (Tutorial2Clrear)
            {
                aud.PlayOneShot(CorrectMusic);
                TextChange("msg8", Nunbers);
                Debug.Log("チュートリア2合格");
                stop2 = true;
            }
        }
    }
    private void Tutorial3Clrearmove()
    {
        if (!stop3)
        {
            Tutorial3Clrear = player3.Tutorial3Goukaku;

            if (Tutorial3Clrear)
            {
                aud.PlayOneShot(CorrectMusic);
                TextChange("msg9", Nunbers);
                Debug.Log("チュートリア3合格");
                stop3= true;
            }
        }
    }
    private void Tutorial4Clrearmove()
    {
        if (!stop4)
        {
            Tutorial4Clrear = player3.Tutorial4Goukaku;

            if (Tutorial4Clrear)
            {
                aud.PlayOneShot(CorrectMusic);
                TextChange("msg15", Nunbers);
                Debug.Log("チュートリア4合格");
                stop4 = true;
            }
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
        msg = "【メッセージを受信しました】";
        aud.PlayOneShot(ReceiveMessage);
        TextChange("msg1", Nunbers);
    }
    private void msg1()
    {
       msg = "き・・聞こえるか";
       TextChange("msg2", Nunbers);
    }
    private void msg2()
    {
        msg = "どうやら君はこの世界に迷い込んでしまったらしい";
        TextChange("msg3", Nunbers);
    }
    private void msg3()
    {
        msg = "この世界から脱出するためには、扉の向こうにいる<color=#FF0000>ドラゴン</color>を討伐し" +
            "\nまた<color=#FF0000>スコアを１万以上</color>稼がなければ元の世界には戻れない";
        TextChange("msg4", Nunbers);
    }
    private void msg4()
    {
        msg = "いきなりこんなことを言われ焦るかもしれないが本当だ";
        TextChange("msg5", Nunbers);
    }
    private void msg5()
    {
        msg = "まず君にはその体の動かし方を教える私の言う通りにしてくれ";
        TextChange("msg6", Nunbers);
    }
    private void msg6()
    {
        msg = "まず移動方法は<color=#FF0000>W・A・S・D</color>で動くことが出来る動いてみてくれ";
        player3.Tutorial1 = true;//動けるようになる
    }
    private void msg7()
    {
        msg = "上出来だ次は<color=#FF0000>マウス右クリックを長押し</color>しながらマウスを動かしてみてくれカメラが動くはずだ";
        player3.Tutorial2 = true;//カメラ視点動かせる
    }
    private void msg8()
    {
        msg = "これも問題ないな次に回避の練習だ<color=#FF0000>スペース</color>を押すと回避出来るやってみてくれ";
        player3.Tutorial3 = true;//回避可能
    }
    private void msg9()
    {
        msg = "おめでとう回避も出来ているだが回避するときに少し注意が必要だから話を聞いてくれ";
        TextChange("msg10", Nunbers);
    }
    private void msg10()
    {
        msg = "回避には右上にあるゲージが必要だこのゲージを<color=#FF0000>\n30%</color>使用することで回避ができる";
        TextChange("msg11",NunbersTutorial4);
    }
    private void msg11()
    {
        msg = "察しのいい君ならもうわかっていると思うが<color=#FF0000>30%</color>以下になれば回避はできない";
        TextChange("msg12", NunbersTutorial4);
    }
    private void msg12()
    {
        msg = "だが安心してくれゲージは時間と共に回復していく";
        TextChange("msg13", Nunbers);
    }
    private void msg13()
    {
        msg = "回避の話が終わったのでいよいよ最後の説明だ";
        TextChange("msg14", Nunbers);
    }
    private void msg14()
    {
        msg = "<color=#FF0000>マウスを左クリック</color>してみてほしい";
        player3.Tutorial4 = true;//攻撃可能
    }
    private void msg15()
    {
        msg = "それがドラゴンへの攻撃手段<color=#FF0000>マジックボール</color>だ！";
        TextChange("msg16", Nunbers);
    }
    private void msg16()
    {
        msg = "<color=#FF0000>マジックボール</color>は撃てば敵に自動的に追跡してくれる";
        TextChange("msg17", Nunbers);
    }
    private void msg17()
    {
        msg = "だが必ず当たるわけではないから注意してくれ";
        TextChange("msg18", Nunbers);
    }
    private void msg18()
    {
        msg = "また<color=#FF0000>マジックボール</color>は敵に連続して触れると<color=#FF0000>攻撃力が上がる</color>" +
            "\nだが画面右に出てくるコンボが途切れると攻撃力元に戻るから注意してくれ";
        TextChange("msg19", NunbersTutorial4);
    }
    private void msg19()
    {
        msg = "説明は以上だ";
        TextChange("msg20", Nunbers);
    }
    private void msg20()
    {
        msg = "メッセージ終了後扉に体を当てればドラゴンと対戦できるでは健闘を祈る";
        TextChange("lastmsg", Nunbers);
    }
    private void lastmsg()
    {
        msg = "【メッセージ終了】";
        aud.PlayOneShot(ReceiveMessage);
        TextChange("HideText", Nunbers);
        Debug.Log("最後の文字表示");
    }
    private void TextChange(string functionname,float nunber)
    {
        Invoke(functionname, nunber);
    }
    private void HideText()
    {
        event1.AllCrearTutorial = true;//チュートリアル全てクリア
        msg = " ";
        Destroy(this.gameObject,2.0f);//全てが終わったら破棄する
    }
}
