using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour,PlayerHitIDamegeble
{
    // Start is called before the first frame update
    public ProgressBar progeruBar;
    [SerializeField] private PlayerDataBase playerDataBase;

    public int Max;//Valuの値を入れるための変数(変える必要なし)
    public int valu = 60;

    private const int Dmg = 150;//ダメージ
    private const int Dmg2 = 50;//ダメージ
    private float DmgCoolTime1 = 3.0f;//ダメージクールタイムを設定する
    private float damageTimer1 = 0f;//変更不可

    [SerializeField] private GameObject PlayerRend;
    private Renderer playerRenderer; // プレイヤのレンダラー
    private Color originalColor; // オリジナルの色
    private Coroutine blinkCoroutine; // 点滅用のコルーチン

    [SerializeField] private PlayerMoveMent playerMovement;
    void Start()
    {
        PlayerStateSO SO = playerDataBase.playersatasList[0];
        valu = SO.PlayerHP;//データベース上のプレイヤのHP参照
        Max = valu;//最大HP保存
        playerRenderer = PlayerRend.GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        WaponTime1();
    }
    private void WaponTime1()
    {
        progeruBar.BarValue = Max;
        if (damageTimer1 > 0f)//Wapon1のクールタイム
        {
            damageTimer1 -= Time.deltaTime;
        }
    }
    public void EnemyAttackDamage(int playerLife)
    {
        Max -= playerLife;//敵のメイン斧のダメージ計算式(被ダメージ側)
    }

    public void PlayerAttackDamage(int EnemyLife)
    {
        //プレイヤの欄のため何も書かない
    }
    public void OnTriggerEnter(Collider other)
    {   //一つ目の武器
        PlayerHitIDamegeble playerdamegeble = gameObject.GetComponent<PlayerHitIDamegeble>();
        PlayerMoveMent movement = gameObject.GetComponent<PlayerMoveMent>();
        if (other.gameObject.tag == "EnemyWapon1" && damageTimer1 <= 0f)//敵の攻撃一つ目(後々攻撃を増やしていく)
        {
            if (playerMovement.Playerevadeing == false)//回避中はダメージ無効
            {
                // プレイヤ滅コルーチンを開始
                if (blinkCoroutine == null)
                {
                    blinkCoroutine = StartCoroutine(BlinkEnemy());
                }
                playerdamegeble.EnemyAttackDamage(Dmg);//HPを減らす式
                Debug.Log("斧がプレイヤにHPを与えた");
                damageTimer1 = DmgCoolTime1;//ダメージタイマーにダメージクールタイムn秒が代入される
                movement.isKnockBack = true;//PlayerMoventに入っているノックバックをtrue
            }
        }
        //-----------------------------------------------------------------------------
        if (other.gameObject.tag == "EnemyWapon2" && damageTimer1 <= 0f)//敵の攻撃2つ目(後々攻撃を増やしていく)
        {
            if (playerMovement.Playerevadeing == false)//回避中はダメージ無効
            {
                // プレイヤ滅コルーチンを開始
                if (blinkCoroutine == null)
                {
                    blinkCoroutine = StartCoroutine(BlinkEnemy());
                }
                playerdamegeble.EnemyAttackDamage(Dmg2);//HPを減らす式(インターフェースで実装)
                Debug.Log("雨がプレイヤにHPを与えた");
                damageTimer1 = DmgCoolTime1;//ダメージタイマーにダメージクールタイムn秒が代入される
                movement.isKnockBack = true;//PlayerMoventに入っているノックバックをtrue
            }
        }
    }
    IEnumerator BlinkEnemy()
    {
        int blinkDuration = 2; // 点滅する回数
        float blinkInterval = 0.2f; // 点滅の間隔
        int timer = 0;
        bool isBlinking = true;

        // オリジナルの色を保存
        Color originalColor = playerRenderer.material.color;

        while (isBlinking)
        {
            if (timer >= blinkDuration)
            {
                isBlinking = false;
                timer = 0;
                break;
            }

            // レンダラーの色を変更して点滅させる
            playerRenderer.material.color = Color.red;
            yield return new WaitForSeconds(blinkInterval);

            playerRenderer.material.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);
            timer++;
        }

        // 点滅終了後、色を元に戻す
        playerRenderer.material.color = originalColor;
        blinkCoroutine = null;
    }
}
