using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerMoveMent : MonoBehaviour//プレイヤの行動処理すべて(バグなどが発生した場合に見つけるのを容易にするためプレイヤ行動処理は全てここに記載している)
{
    private Rigidbody rb;
    private CapsuleCollider cap;//プレイヤのコライダ

    [SerializeField] private GameObject PlayrCanvas;
    [SerializeField] private GameObject PlayerDieCanvas;

    [SerializeField] private Player3 player3;//チュートリアルに必要なブール変数を取ってくる

    private Animator anim;

    //----------------------------------回避関係
    private float evade;//現在の回避を入れる変数
    private float evadeTime;//回避する時間を入れる変数
    private const float evadenumber = 1f;//回避する時間
    private const float evadeUse = 30f;//回避を使う変数
    private bool evadeClicked = false;//回避
    private GameObject PlayerSt;//プレイヤの中に入っているステータスを参照するための変数
    //--------------------------------------------------------------------

    //--------------------------------------ノックバック関係
    public float KnockBackPower;
    [SerializeField] private const float KnockTime = 1;//ノックバックする時間
    private float KnockBackTime;//ノックバックする時間を入れる変数
    public Transform Enemy;
    public bool isKnockBack;
    //--------------------------------------

    //--------------------------------------カメラ関係
    public Camera camera;

    private const float CameraHeightPos = 1.5f;//プレイヤカメラの高さを決める変数

    public const float moveSpeed = 6f;

    public float x_sensi;//カメラのスピード

    public float y_sensi;

    private const float potisionResetY = -20;//プレイヤが万が一落ちた時に戻る値を定義した変数

    public const float cameraDistance = 5f;//カメラの後ろの範囲

    public const float cameraHeight = 2f;//カメラの上限高さ

    private float currentHeight;//現在の高さを保存する変数

    private Vector3 cameraAngle;

    [SerializeField]
    private LayerMask obstacleLayer;//カメラがめり込まないところを決めれる
    //----------------------------------------------------------------------

    private Vector3 gravity;//重力

    static readonly int hashAttackType = Animator.StringToHash("AttackType");

    public bool Playerevadeing = false;//回避を確認するブール

    [SerializeField] private GameObject evadeGameObj;

    void Start()
    {
        cap = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        evadeTime = evadenumber;//回避する時間を入れる
        PlayerSt = GameObject.FindWithTag("Player");//プレイヤのタグを探す
        KnockBackTime = KnockTime;
        PlayrCanvas.SetActive(true);
        PlayerDieCanvas.SetActive(false);//最初は死んでいないので開かない
        anim = GetComponent<Animator>();
        gravity = Physics.gravity;//処理を軽くするためStartで重力を代入する
        cameraAngle = transform.rotation.eulerAngles;
        evadeGameObj.SetActive(false);//最初は回避バリアは見えない
    }

    public void Playermove()
    {
        //移動方法キーボードによる移動を可能にしている場合によってはコントローラーにも変更可
        if (anim.GetBool("Attack") == true) return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        currentHeight = transform.position.y;//現在の高さを保存

        Vector3 moveDir = camera.transform.TransformDirection(new Vector3(h, 0, v));
        moveDir.y = 0;
        moveDir.Normalize();

        float move = moveSpeed * Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));//スティックの計算

        if (rb.velocity.y < 0) //処理を軽くするために落下中のみ重力計算する
        {
            Vector3 GravityVelocity = new Vector3(0, gravity.y, 0) * Time.deltaTime;//重力計算
            rb.velocity += GravityVelocity;
            if (rb.velocity.y < potisionResetY)
            {
                SetPosition();//落下したら元に戻る関数
            }

        }

        rb.velocity = moveDir * move + new Vector3(0, rb.velocity.y, 0);//重力を加える
        //プレイヤの回転率を計算
        if (move > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            float rotationSpeed = 8f;//遅いほど回転の速度を遅くする
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);//回転を補完する
            player3.Tutorial1Goukaku = true;//チュートリアル１合格
            animSpeed(moveSpeed);//移動のアニメーションに関する関数
        }
        else if (move == 0)
        {
            animSpeed(0);//移動のアニメーションに関する関数
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (player3.Tutorial4)//チュートリアル4
            {
                animAttack(0);//攻撃に関する関数
                player3.Tutorial4Goukaku = true;//チュートリアル4合格
            }
        }
    }
    public void cameracon()
    {
        //カメラの操作関連
        Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (Input.GetMouseButton(1))
        {
            if (player3.Tutorial2)//チュートリアル2をクリアしたら動く
            {
                float y_Rotation = delta.y * y_sensi;
                cameraAngle.x -= y_Rotation;

                float x_Rotation = delta.x * x_sensi;
                cameraAngle.y += x_Rotation;

                player3.Tutorial2Goukaku = true;//移動のチュートリアル合格
}
        }

        Quaternion cameraRotation = Quaternion.Euler(cameraAngle);
        Vector3 cameraOffset = cameraRotation * new Vector3(0, cameraHeight, -cameraDistance);

        // レイキャストを飛ばしてカメラの移動先を補正
        RaycastHit hit;
        if (Physics.Raycast(transform.position, cameraOffset.normalized, out hit, cameraOffset.magnitude, obstacleLayer))
        {
            cameraOffset = hit.point - transform.position;
        }
        //カメラと障害物の計算をしている
        camera.transform.position = transform.position + cameraOffset;
        camera.transform.rotation = Quaternion.LookRotation(transform.position + new Vector3(0, CameraHeightPos, 0) - camera.transform.position);
    }

    public void Evasivel()
    {
        //回避のシステム
        if (evade >= evadeUse)//回避ゲージが30以上ないと回避できない
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EvasiveButtonClicked();//回避をtrueにする
                player3.Tutorial3Goukaku = true;//チュートリアル3合格
            }
            if (evadeClicked)//回避ボタン
            {
                rb.velocity = Vector3.zero;//回避中移動を受け付けない
                Playerevadeing = true;//回避中を確認（回避中に攻撃を受けないようにするためのブール文）
                cap.height = 0; // カプセルコライダーの変形処理を行う(消すと地面から落ちるため変形させている)
                rb.AddForce(transform.forward * 11f, ForceMode.VelocityChange);
                anim.SetTrigger("Loling");//回避モーション
                evadeTime -= Time.deltaTime;//回避する時間を減らす
                if (evadeTime <= 0f)
                {
                    Playerevadeing = false;//回避中終わり（回避中に攻撃を受けないようにするためのブール文）
                    PlayerSt.GetComponent<AvoidanceSC>().MaxAvoidance -= evadeUse;//回避するとゲージを減らす
                    evadeClicked = false;
                    evadeTime = evadenumber;//回避の時間を変更する
                    cap.height = 1.871724f;// カプセルコライダーの変形を元に戻す処理を行う(キャラクタによって数は変わる)
                }
            }
        }

        if(Playerevadeing == true)//回避中に回避の見た目にする
        {
            evadeGameObj.SetActive(true);//回避のバリアオブジェクトを出す
        }
        else
        {
            evadeGameObj.SetActive(false);//回避のバリアオブジェクトを消す
        }
    }

    public void KnockBack()
    {
        ///ノックバックの計算
        if (isKnockBack)//ノックバックしたとき
        {
            rb.velocity = Vector3.zero;//ノックバック中移動を受け付けない
            Vector3 distination = (transform.position - Enemy.transform.position).normalized;//自分と敵の距離を計算して、距離と方向を出して正規化する
            rb.AddForce(distination * KnockBackPower, ForceMode.VelocityChange);//ノックバックさせる
            KnockBackTime -= Time.deltaTime;
            anim.SetBool("KnockBack", true);
            if (KnockBackTime < 0f)
            {
                Debug.Log("ノックバック可能");
                isKnockBack = false;
                KnockBackTime = KnockTime;
                anim.SetBool("KnockBack", false);
            }
        }
        Debug.Log(KnockBackTime);//ノックバックをする時間を調べる

    }
    public void PlayerDie()
    {
        //プレイヤの死んだときの関数
        anim.SetTrigger("Die");
        cap.height = 0.5f;//カプセルコライダーを無効にすると重力で落ちるためコライダの数字を変更する
        cap.center = new Vector3(0, 0, 0);
        cap.radius = 0.2f;
        UnityEngine.Cursor.visible = true;//カーソルも出す
        PlayrCanvas.SetActive(false);
        PlayerDieCanvas.SetActive(true);//死んだので選択画面を開く
        Debug.Log("死にました");
    }
    bool EvasiveButtonClicked()
    {
        //回避のボタンを押したときの関数
        evadeClicked = true;//回避イベント
        return evadeClicked;
    }
  public void evadTime()
    {
        evade = PlayerSt.GetComponent<AvoidanceSC>().MaxAvoidance;//回避を入れる
    }
    private void SetPosition()//落下したら元に戻る関数
    {
        // 指定された位置にオブジェクトを移動する
        Vector3 WakePosition = new Vector3(0, 20, 0);
        rb.transform.position = WakePosition;
    }
    private void animSpeed(float Speed)
    {
        //アニメーション制御関数
        anim.SetFloat("Speed", Speed);
    }
    private void animAttack(int Type)
    {

        anim.SetTrigger("Attack");
        anim.SetInteger(hashAttackType, Type);
    }
}
