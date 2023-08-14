using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Event2 : MonoBehaviour
{
    private PlayableDirector event2;
    private int Life;
    private bool hasEventPlayed = false;
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyLife enemylife;
    [SerializeField] private GameObject EnemyHPBar;
    private void Start()
    {
        event2 = GetComponent<PlayableDirector>();
    }
    private void Update()
    {
        if (!hasEventPlayed)
        {
            try
            {
                Life = enemylife.Max;
            }
            catch
            {
                //何もしない
            }
            if (Life <= 0)//HPが０以下の時
            {
                try
                {
                    enemylife.OrizinalColers();//元の色に戻す
                }
                catch 
                {
                    //取得しないためtry関数で記述
                }
                Invoke("moveStart", 2.0f);//イベント2実行
                enemy.enabled = false;//敵キャラ停止
                EnemyHPBar.SetActive(false);//敵HPバー消す
            }
        }
        else
        {
            if (event2.state == PlayState.Paused)
            {
                SceneManager.LoadScene("Ending");
            }
        }
    }
    private void moveStart()
    {
        event2.Play();//イベント2実行
        hasEventPlayed = true;
    }
}

