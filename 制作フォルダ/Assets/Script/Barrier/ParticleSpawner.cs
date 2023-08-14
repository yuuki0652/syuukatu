using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BarrierPrefab; // �p�[�e�B�N���̃v���n�u
    public Transform playerTransform; // �v���C���[�̃g�����X�t�H�[��

    private float spawnInterval = 10f; // �p�[�e�B�N���̐����Ԋu
    private float IntervalTime = 0f;//�ύX�s��
    public float spawnRangeMinX = -5f; // X���̍ŏ��l
    public float spawnRangeMaxX = 5f; // X���̍ő�l
    public float spawnRangeMinZ = -5f; // Z���̍ŏ��l
    public float spawnRangeMaxZ = 5f; // Z���̍ő�l
    public float PlayeLeaver = 3f; // �v���C���[����̍ŏ�����
    private GameObject enemyLife; // EnemyLife�R���|�[�l���g�̎Q��
   
    private float timer = 0f; // �^�C�}�[

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
                    SpawnParticle(); // �p�[�e�B�N���𐶐�����
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
            //HP���Ȃ��Ȃ��������
        }
    }


    private void SpawnParticle()
    {
        Vector3 randomPosition = GetRandomPosition(); // �����_���Ȉʒu���擾
        randomPosition.y += 1.0f; // Y���������グ��
        Instantiate(BarrierPrefab, randomPosition, Quaternion.identity); // �p�[�e�B�N���𐶐�
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(spawnRangeMinX, spawnRangeMaxX); // �����_����X���W�𐶐�
        float randomZ = Random.Range(spawnRangeMinZ, spawnRangeMaxZ); // �����_����Z���W�𐶐�

        Vector3 randomPosition = new Vector3(randomX, 0f, randomZ); // �����_���Ȉʒu��ݒ�

        // �v���C���[����̋������ŏ����������ł���΁A�ŏ������ɂȂ�悤�Ɉʒu�𒲐�
        if (Vector3.Distance(randomPosition, playerTransform.position) < PlayeLeaver)
        {
            Vector3 directionToPlayer = (randomPosition - playerTransform.position).normalized;
            randomPosition = playerTransform.position + directionToPlayer * PlayeLeaver;
        }

        return randomPosition;
    }
}
