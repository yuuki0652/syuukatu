using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class Enemy : MonoBehaviour
{
    private enum EnemyState
    {
        PlayerSearch,//プレイヤ索敵
        PlayerDiscover,//プレイヤ発見
        Atttack1,
        Atttack2
    }
    private EnemyState enemyState;
    public float lookRadius = 10f; // プレイヤーを発見する範囲
    public float moveSpeed = 5f; // 移動速度
    public float attackRange = 2f; // 攻撃範囲
    public int attack1Cooldown = 3; // 攻撃クールダウン
    public int attack2Cooldown = 6; // 攻撃クールダウン
    public float attackDelay = 2f; // 攻撃開始までの待機時間
    private int enemylife;//ここに敵のHPを代入する(いじる必要なし)
    private int enemylifeMaxValu;//最大HP保存用変数
    public GameObject weaponPrefab;//落ちてくる斧
    private int Attac2Time;

    [SerializeField] private Transform target; // プレイヤーのTransform
    private Animator anim; // Animatorコンポーネント
    private bool isAttacking1 = false; // 1攻撃中かどうか
    private bool isAttacking2 = false; // 2攻撃中かどうか
    private bool isDelaying = false; // 攻撃開始までの待機中かどうか

    [SerializeField] private GameObject Hand;
    [SerializeField] private GameObject EnemyHeart;//心臓のオブジェクトを入れる
    [SerializeField] private EnemyLife enemyLife;
    [SerializeField, Tooltip("魔法陣")]
    private GameObject MagicCircle;//魔法陣

    private GameObject enemyShot;
    void Start()
    {
        enemyState = EnemyState.PlayerSearch;//最初敵(プレイヤを探す)
        Hand.SetActive(false);//攻撃時以外はfalse
        anim = GetComponent<Animator>();
        //enemylife = EnemyLife.GetComponent<EnemyLife>().Max;//最初に保存された敵のHPをenemyifeに代入する
        enemylife = enemyLife.Max;//最初に保存された敵のHPをenemyifeに代入する
        //enemylifeMaxValu = EnemyLife.GetComponent<EnemyLife>().valu;
        enemylifeMaxValu = enemyLife.valu;
        enemyShot = GameObject.FindWithTag("Enemy");//敵のタグを探す
    }
    void FixedUpdate()
    {
        switch (enemyState)
        {
            case EnemyState.PlayerSearch:
                PlayerSearchState();//プレイヤ索敵
                break;
            case EnemyState.PlayerDiscover:
                PlayerDiscoverState();//移動
                break;
            case EnemyState.Atttack1:
                Attack1();//攻撃１
                break;
            case EnemyState.Atttack2:
                Attack2();//攻撃２
                break;
        }
        LifeChange();//HP変化の関数
    }

    private void PlayerSearchState()//プレイヤを見つけるステート
    {
        if (enemylife > 0)
        {
            //プレイヤとの距離を計算
            float distance = Vector3.Distance(target.position, transform.position);
            StopMoving();
            if (distance <= lookRadius)//プレイヤが範囲に入ってきたら移動ステートに移行する
            {
                anim.SetBool("Walk", false);//攻撃が終わった後歩きのモーションに移行してしまうためここで元に戻している
                enemyState = EnemyState.PlayerDiscover;
            }
        }
    }

    private void PlayerDiscoverState()//プレイヤに向かって移動し攻撃するステート
    {
        if (enemylife > 0)
        {
            //移動の判定
            if (!isAttacking1 && !isAttacking2 && !isDelaying)//攻撃中でなければ移動可能
            {
                MoveEnemy();
            }
            //プレイヤとの距離を計算
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= attackRange && !isAttacking1 && !isAttacking2 && !isDelaying)//範囲内であれば攻撃する
            {
                switch (enemylife >= enemylifeMaxValu / 2)//HPが半分以下の時(見やすいようにスイッチ文)
                {
                    case true:
                        enemyState = EnemyState.Atttack1;
                        break;
                    case false:
                        enemyState = EnemyState.Atttack2;
                        break;

                }
            }
            else if (distance > lookRadius)//範囲外に出る
            {
                anim.SetBool("Walk", false);
                enemyState = EnemyState.PlayerSearch;
            }
        }
    }

    private void Attack1()
    {
        if (!isAttacking1)
        {
            StartCoroutine(AttackDelay());
            isDelaying = true;
            enemyState = EnemyState.PlayerSearch;//探す状態に戻す
        }
    }
    private void Attack2()
    {
        if (!isAttacking2)
        {
            Hand.SetActive(false);
            StartCoroutine(SecondAttack());
            Debug.Log("敵の体力が半分以下になりました");
            enemyState = EnemyState.PlayerSearch;//探す状態に戻す
            isDelaying = true;
        }
    }
    void MoveEnemy() // プレイヤーに向かって移動する
    {
        anim.SetBool("rest", false);//攻撃クールタイムが終わったら元に戻す    
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        anim.SetBool("Walk", true);
    }
    void StopMoving()
    {
        transform.Translate(Vector3.zero);
    }
    void HandleAttack1Animation()  // 攻撃1のアニメーションの処理を制御する
    {
        float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animTime > 0.7f)
        {
            Hand.SetActive(false);
        }
        else if (animTime >= 0.3f)
        {
            Hand.SetActive(true);
        }
    }
    void Die()// 死亡処理を行う
    {
        Destroy(this.EnemyHeart);
        Hand.SetActive(false);
        anim.SetTrigger("Die");
        MagicCircle.SetActive(true);
        Debug.Log("敵が死にました?");
    }


    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay); // 攻撃開始までの待機時間を待つ
        transform.Translate(Vector3.zero); //動かけない
        isDelaying = false; // 待機中ではなくなる
        // 1つ目の攻撃
        transform.LookAt(target.position); // プレイヤーの方向を向く
        anim.SetTrigger("Attack"); // 攻撃アニメーションを再生
        isAttacking1 = true; // 攻撃中に設定
        StartCoroutine(AttackCooldown()); // 攻撃クールダウンを開始
    }

    IEnumerator SecondAttack()
    {
        yield return new WaitForSeconds(attackDelay); // 1秒待つ
        transform.Translate(Vector3.zero); //動かけない
        isDelaying = false; // 待機中ではなくなる
        isAttacking2 = true;//攻撃中に設定
        Hand.SetActive(false); // 攻撃中でなければHandオブジェクトを非アクティブにする
        transform.Translate(Vector3.zero); //動かけない
        transform.Translate(Vector3.zero);
        //anim.SetBool("Attack1",true); // 攻撃アニメーションを再生
        anim.SetTrigger("Attack1");
        //シードを設定してランダムな位置の計算に影響を与える
        System.Random rand = new System.Random((int)DateTime.Now.Ticks);

        for (int i = 0; i < 200; i++)
        {
            if (enemyLife.Max == 0) { break; }//死んだとき武器を降らせないため
            transform.LookAt(target);
            Vector3 weaponPos = new Vector3(UnityEngine.Random.Range(-5f, 5f), 30f, UnityEngine.Random.Range(-5f, 5f));
            GameObject weapon = Instantiate(weaponPrefab, target.position + weaponPos, Quaternion.Euler(180f, 0f, 0f));
            Rigidbody weaponRb = weapon.AddComponent<Rigidbody>();
            weaponRb.AddForce(Vector3.down * 1000f);
            Destroy(weapon, 1.5f);
            yield return new WaitForSeconds(0.1f);
        }
        anim.SetBool("rest", true);//Attack2Cooldownに書くとアニメーションをしてくれないためここに記述する 
        StartCoroutine(Attack2Cooldown()); // 攻撃クールダウンを開始
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attack1Cooldown); // 攻撃クールダウンを待つ
        Hand.SetActive(false); // 攻撃中でなければHandオブジェクトを非アクティブにする
        isAttacking1 = false; // 攻撃中ではなくなる
        Debug.Log("冷却中");
    }

    IEnumerator Attack2Cooldown()
    {
        yield return new WaitForSeconds(attack2Cooldown); // 攻撃クールダウンを待つ
        Hand.SetActive(false); // 攻撃中でなければHandオブジェクトを非アクティブにする
        isAttacking2 = false; // 攻撃中ではなくなる
        Debug.Log("zzz");
    }
    IEnumerator Weapon1Prefbu()
    {
        transform.LookAt(target);
        Vector3 weaponPos = new Vector3(UnityEngine.Random.Range(-5f, 5f), 30f, UnityEngine.Random.Range(-5f, 5f));
        GameObject weapon = Instantiate(weaponPrefab, target.position + weaponPos, Quaternion.Euler(180f, 0f, 0f));
        Rigidbody weaponRb = weapon.AddComponent<Rigidbody>();
        weaponRb.AddForce(Vector3.down * 1000f);
        Destroy(weapon, 1.5f);
        yield return new WaitForSeconds(0.1f);
    }

    private void InstantiateWeapon()
    {
        Vector3 weaponPos = new Vector3(UnityEngine.Random.Range(-5f, 5f), 30f, UnityEngine.Random.Range(-5f, 5f));
        GameObject weapon = Instantiate(weaponPrefab, target.position + weaponPos, Quaternion.Euler(180f, 0f, 0f));
        Rigidbody weaponRb = weapon.AddComponent<Rigidbody>();
        weaponRb.AddForce(Vector3.down * 1000f);
        Destroy(weapon, 1.5f);
    }

    private void LifeChange()
    {
        if (enemylife != enemyLife.Max)
        {
            enemylife = enemyLife.Max;
            Debug.Log("敵のHPが変更された");
        }

        if (enemylife <= 0)//本来はステートで管理するはずだがバグるため直接関数を呼び出している。
        {
            Die();
        }
        else if (enemylife > 0)
        {
            // 攻撃中の処理
            if (isAttacking1)
            {
                HandleAttack1Animation();
            }
        }
    }
}


