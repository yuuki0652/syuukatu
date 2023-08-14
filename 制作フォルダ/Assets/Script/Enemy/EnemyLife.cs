using System.Collections;
using UnityEngine;

public class EnemyLife : MonoBehaviour,EnemyHitIDamegeble
{ 
    // Start is called before the first frame update
    public EnemyProgressBar pb;//敵のHP

    public int Max;//Valuの値を入れるための変数(何も書かない)
    public int valu=15000;
    private int DMG;
    [SerializeField] private PlayerDataBase playerDataBase;//プレイヤの中身参照
    private PlayerStateSO SO;
    public float damageMultiplier; // ダメージ倍率
    private GameObject ComboSystemScript;
    private GameObject ScoreSystemScript;
    private float ConboAttackTime = 4f;//コンボと同じ時間
    private float timer = 0f;
    private int ConboAttackMG;
    private int DMG2;

    private Renderer enemyRenderer; // 敵オブジェクトのレンダラー
    private Color originalColor; // オリジナルの色
    private Coroutine blinkCoroutine; // 点滅用のコルーチン
    [SerializeField] GameObject EnemyRender;
    private float blinkTimer = 1.0f; // 追加：点滅の終了までの時間
    [SerializeField] private ParticleSystem DMG1Particle;

    void Start()
    {
        Max = valu;//最大HP保存
        SO = playerDataBase.playersatasList[0];
        ComboSystemScript = GameObject.FindWithTag("ComboTag");//二つのタグ取得
        ScoreSystemScript = GameObject.FindWithTag("ScoreTag");
        ConboAttackMG = 0;
        DMG2 = 0;
        enemyRenderer = EnemyRender.GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color;
        damageMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        pb.BarValue = Max;
        if (ConboAttackMG > 0)
        {
            timer += Time.deltaTime;
            if (timer > ConboAttackTime)//n秒を超えたらコンボ数０
            {
                ResetConboAttack();
                Debug.Log("入りました");
            }
        }
    }

    public void PlayerAttackDamage(int EnemyLife)//プレイヤのマジック攻撃を受ける
    {
        Max -= EnemyLife;//プレイヤマジックダメージ計算をする(被ダメージ側で実装)
    }
    public void OnTriggerEnter(Collider other)
    {
        EnemyHitIDamegeble Enemydamegeble = gameObject.GetComponent<EnemyHitIDamegeble>();//ダメージルール(敵側取得)
        if (other.gameObject.tag == "Mgic1")
        {
            DMG = (SO.PlayerMagic / 100 * 30 * (int)damageMultiplier) + DMG2;//マジック力の30%をダメージとして与える
            Instantiate(DMG1Particle, this.transform.position, Quaternion.identity);
            if (Enemydamegeble != null)
            {
                PlayerAttackDamage(DMG);//ここでダメージを与える(依存性の少ないインターフェイスでダメージ処理を行う)
                ComboSystemScript.GetComponent<ComboSystem>().IncreaseCombo();//コンボの数を呼び出す
                ScoreSystemScript.GetComponent<Score>().IncreaseScore();//スコアを呼び出す 
                ConboAttackMG++;
                IncreaseConboAttack();
                // 点滅コルーチンを開始
                if (blinkCoroutine == null)
                {
                    blinkCoroutine = StartCoroutine(BlinkEnemy());
                }

                Debug.Log(DMG + "プレイヤのマジック攻撃が当たった");
            }
        }
    }
    void ResetConboAttack()
    {
        timer = 0f;
        DMG2 = 0;//追加ダメージをもとに戻す
        ConboAttackMG = 0;
    }
    public void IncreaseConboAttack()
    {
        timer = 0f;//コンボしたらタイマーリセット
        if (ConboAttackMG >= 1)//最初だけ普通の倍率
        {
            DMG2 += 50;//攻撃を続けるほど追加ダメージを上げる
            if (DMG2 >= 1000)//追加ダメージ1000が上限
            {
                DMG2 = 1000;
            }
            Debug.Log("追加ダメージ" + DMG2);
        }
    }
    IEnumerator BlinkEnemy()
    {
        int blinkDuration = 3; // 点滅する回数
        float blinkInterval = 0.2f; // 点滅の間隔
        int timer = 0;
        bool isBlinking = true;

        // オリジナルの色を保存
        Color originalColor = enemyRenderer.material.color;

        while (isBlinking)
        {
            if (timer >= blinkDuration)
            {
                isBlinking = false;
                timer = 0;
                break;
            }

            // レンダラーの色を変更して点滅させる
            enemyRenderer.material.color = Color.red;
            yield return new WaitForSeconds(blinkInterval);

            enemyRenderer.material.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);
            timer++;
        }

        // 点滅終了後、色を元に戻す
        //enemyRenderer.material.color = originalColor;
        OrizinalColers();
        Debug.Log("色変化");
        blinkCoroutine = null;
    }

    public void OrizinalColers()
    {
        enemyRenderer.material.color = originalColor;
    }
}