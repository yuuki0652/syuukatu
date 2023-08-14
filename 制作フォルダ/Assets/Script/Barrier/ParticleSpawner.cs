using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BarrierPrefab; // パーティクルのプレハブ
    public Transform playerTransform; // プレイヤーのトランスフォーム

    private float spawnInterval = 10f; // パーティクルの生成間隔
    private float IntervalTime = 0f;//変更不可
    public float spawnRangeMinX = -5f; // X軸の最小値
    public float spawnRangeMaxX = 5f; // X軸の最大値
    public float spawnRangeMinZ = -5f; // Z軸の最小値
    public float spawnRangeMaxZ = 5f; // Z軸の最大値
    public float PlayeLeaver = 3f; // プレイヤーからの最小距離
    private GameObject enemyLife; // EnemyLifeコンポーネントの参照
   
    private float timer = 0f; // タイマー

    private void Start()
    {
        enemyLife = GameObject.FindGameObjectWithTag("Enemy");
    }
    private void Update()
    {
        try
        {
            int life = enemyLife.GetComponent<EnemyLife>().Max;
            int Maxvalu = enemyLife.GetComponent<EnemyLife>().valu;

            if (life <= Maxvalu / 2 && life > 0)
            {
                if (IntervalTime <= 0f)
                {
                    SpawnParticle(); // パーティクルを生成する
                    IntervalTime = spawnInterval;
                }

                if (IntervalTime > 0f)
                {
                    IntervalTime -= Time.deltaTime;
                }
            }
        }
        catch
        {
            //HPがなくなったら消す
        }
    }


    private void SpawnParticle()
    {
        Vector3 randomPosition = GetRandomPosition(); // ランダムな位置を取得
        randomPosition.y += 1.0f; // Y軸を少し上げる
        Instantiate(BarrierPrefab, randomPosition, Quaternion.identity); // パーティクルを生成
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(spawnRangeMinX, spawnRangeMaxX); // ランダムなX座標を生成
        float randomZ = Random.Range(spawnRangeMinZ, spawnRangeMaxZ); // ランダムなZ座標を生成

        Vector3 randomPosition = new Vector3(randomX, 0f, randomZ); // ランダムな位置を設定

        // プレイヤーからの距離が最小距離未満であれば、最小距離になるように位置を調整
        if (Vector3.Distance(randomPosition, playerTransform.position) < PlayeLeaver)
        {
            Vector3 directionToPlayer = (randomPosition - playerTransform.position).normalized;
            randomPosition = playerTransform.position + directionToPlayer * PlayeLeaver;
        }

        return randomPosition;
    }
}
