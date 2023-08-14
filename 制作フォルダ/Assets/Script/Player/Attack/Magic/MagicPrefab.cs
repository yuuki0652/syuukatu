using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class MagicPrefab : MonoBehaviour
{
    public GameObject magicBallPrefab;  // 魔法の球のPrefab
    public GameObject Ball { get; private set; }

    public float ballSpeed = 10.0f;     // 魔法の球の速度
    public float ballLifeTime = 1.5f;   // 魔法の球の寿命
    public Transform target;            // 追跡するターゲットのTransform

    public void Prefab()//アニメーションでボールを出す
    {
        // プレイヤーの前方に向けて魔法の球を生成する
        Ball = Instantiate(magicBallPrefab, transform.position + transform.forward * 2.0f, transform.rotation);

        // 魔法の球に Rigidbody コンポーネントがある場合、追跡コンポーネントを追加する
        Rigidbody rb = Ball.GetComponent<Rigidbody>();

        // 追跡するターゲットが設定されている場合、ターゲットに向けて進む
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * ballSpeed;
            Ball.transform.LookAt(target);
        }
        else
        {
            // 追跡するターゲットがない場合は、プレイヤーの前方に進む
            rb.velocity = transform.forward * ballSpeed;
        }

        // 魔法の球を一定時間後に破棄する
        Destroy(this.Ball, ballLifeTime);
    }
}

