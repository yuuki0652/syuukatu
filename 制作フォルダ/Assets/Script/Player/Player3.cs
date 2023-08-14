using Cinemachine;
using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static Player3;
using static UnityEngine.Rendering.DebugUI;

public class Player3 : MonoBehaviour
{
    [SerializeField] private PlayerMoveMent movent;

    private GameObject PlayerSt;//プレイヤの中に入っているステータスを参照するための変数

    private int PlayerNowLife;//プレイヤの現在のHPを入れる変数

    private int playerlife;//プレイヤの初期HP代入変数

    public bool Tutorial1=false;//移動のチュートリアル
    public bool Tutorial1Goukaku = false;//移動のチュートリアル合格

    public bool Tutorial2 = false;//カメラのチュートリアル
    public bool Tutorial2Goukaku = false;//カメラのチュートリアル合格

    public bool Tutorial3 = false;//回避のチュートリアル
    public bool Tutorial3Goukaku = false;//回避のチュートリアル合格

    public bool Tutorial4 = false;//攻撃のチュートリアル
    public bool Tutorial4Goukaku = false;//攻撃のチュートリアル合格

    void Start()
    {
        playerlife = GetComponent<PlayerStatus>().valu;//プレイヤの初期HP代入
        PlayerSt = GameObject.FindWithTag("Player");//プレイヤのタグを探す
    }

    void FixedUpdate()
    {
        PlayernowLife();//変更されたHPを受け取る
        movent.evadTime();//回避を常に変更する関数(PlayerMoveMentから受け取る)
        if (playerlife > 0)//HPがあるとき
        {
            if (Tutorial1)//移動のチュートリアル
            {
                movent.Playermove();//プレイヤ操作に関する関数
            }
            if (Tutorial3)//回避のチュートリアル
            {
                movent.Evasivel();//回避に関する関数(PlayerMoveMentから受け取る)
            }
            movent.cameracon();//カメラ操作に関する関数
            movent.KnockBack();//ノックバックに関する関数(PlayerMoveMentから受け取る)
            LifeChange();//HPが変化したときに発動する関数
        }
        else//HPが0
        {
            movent.PlayerDie();//死んだときに発動する関数(PlayerMoveMentから受け取る)
        }
    }
    private void LifeChange()
    {
        if (playerlife != PlayerNowLife)//プレイヤのHPが変更された場合(計算を減らすための式)
        {
            playerlife = PlayerNowLife;//減った分のHPを代入する
            Debug.Log("プレイヤのHPが変更された");
        }
    }
    private void PlayernowLife()
    {
        PlayerNowLife = PlayerSt.GetComponent<PlayerStatus>().Max;//変更されたHPを受け取る
    }
}