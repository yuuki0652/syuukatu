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
        PlayerSearch,//�v���C�����G
        PlayerDiscover,//�v���C������
        Atttack1,
        Atttack2
    }
    private EnemyState enemyState;
    public float lookRadius = 10f; // �v���C���[�𔭌�����͈�
    public float moveSpeed = 5f; // �ړ����x
    public float attackRange = 2f; // �U���͈�
    public int attack1Cooldown = 3; // �U���N�[���_�E��
    public int attack2Cooldown = 6; // �U���N�[���_�E��
    public float attackDelay = 2f; // �U���J�n�܂ł̑ҋ@����
    private int enemylife;//�����ɓG��HP��������(������K�v�Ȃ�)
    private int enemylifeMaxValu;//�ő�HP�ۑ��p�ϐ�
    public GameObject weaponPrefab;//�����Ă��镀
    private int Attac2Time;

    [SerializeField] private Transform target; // �v���C���[��Transform
    private Animator anim; // Animator�R���|�[�l���g
    private bool isAttacking1 = false; // 1�U�������ǂ���
    private bool isAttacking2 = false; // 2�U�������ǂ���
    private bool isDelaying = false; // �U���J�n�܂ł̑ҋ@�����ǂ���

    [SerializeField] private GameObject Hand;
    [SerializeField] private GameObject EnemyHeart;//�S���̃I�u�W�F�N�g������
    [SerializeField] private EnemyLife enemyLife;
    [SerializeField, Tooltip("���@�w")]
    private GameObject MagicCircle;//���@�w

    private GameObject enemyShot;
    void Start()
    {
        enemyState = EnemyState.PlayerSearch;//�ŏ��G(�v���C����T��)
        Hand.SetActive(false);//�U�����ȊO��false
        anim = GetComponent<Animator>();
        //enemylife = EnemyLife.GetComponent<EnemyLife>().Max;//�ŏ��ɕۑ����ꂽ�G��HP��enemyife�ɑ������
        enemylife = enemyLife.Max;//�ŏ��ɕۑ����ꂽ�G��HP��enemyife�ɑ������
        //enemylifeMaxValu = EnemyLife.GetComponent<EnemyLife>().valu;
        enemylifeMaxValu = enemyLife.valu;
        enemyShot = GameObject.FindWithTag("Enemy");//�G�̃^�O��T��
    }
    void FixedUpdate()
    {
        switch (enemyState)
        {
            case EnemyState.PlayerSearch:
                PlayerSearchState();//�v���C�����G
                break;
            case EnemyState.PlayerDiscover:
                PlayerDiscoverState();//�ړ�
                break;
            case EnemyState.Atttack1:
                Attack1();//�U���P
                break;
            case EnemyState.Atttack2:
                Attack2();//�U���Q
                break;
        }
        LifeChange();//HP�ω��̊֐�
    }

    private void PlayerSearchState()//�v���C����������X�e�[�g
    {
        if (enemylife > 0)
        {
            //�v���C���Ƃ̋������v�Z
            float distance = Vector3.Distance(target.position, transform.position);
            StopMoving();
            if (distance <= lookRadius)//�v���C�����͈͂ɓ����Ă�����ړ��X�e�[�g�Ɉڍs����
            {
                anim.SetBool("Walk", false);//�U�����I�����������̃��[�V�����Ɉڍs���Ă��܂����߂����Ō��ɖ߂��Ă���
                enemyState = EnemyState.PlayerDiscover;
            }
        }
    }

    private void PlayerDiscoverState()//�v���C���Ɍ������Ĉړ����U������X�e�[�g
    {
        if (enemylife > 0)
        {
            //�ړ��̔���
            if (!isAttacking1 && !isAttacking2 && !isDelaying)//�U�����łȂ���Έړ��\
            {
                MoveEnemy();
            }
            //�v���C���Ƃ̋������v�Z
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= attackRange && !isAttacking1 && !isAttacking2 && !isDelaying)//�͈͓��ł���΍U������
            {
                switch (enemylife >= enemylifeMaxValu / 2)//HP�������ȉ��̎�(���₷���悤�ɃX�C�b�`��)
                {
                    case true:
                        enemyState = EnemyState.Atttack1;
                        break;
                    case false:
                        enemyState = EnemyState.Atttack2;
                        break;

                }
            }
            else if (distance > lookRadius)//�͈͊O�ɏo��
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
            enemyState = EnemyState.PlayerSearch;//�T����Ԃɖ߂�
        }
    }
    private void Attack2()
    {
        if (!isAttacking2)
        {
            Hand.SetActive(false);
            StartCoroutine(SecondAttack());
            Debug.Log("�G�̗̑͂������ȉ��ɂȂ�܂���");
            enemyState = EnemyState.PlayerSearch;//�T����Ԃɖ߂�
            isDelaying = true;
        }
    }
    void MoveEnemy() // �v���C���[�Ɍ������Ĉړ�����
    {
        anim.SetBool("rest", false);//�U���N�[���^�C�����I������猳�ɖ߂�    
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        anim.SetBool("Walk", true);
    }
    void StopMoving()
    {
        transform.Translate(Vector3.zero);
    }
    void HandleAttack1Animation()  // �U��1�̃A�j���[�V�����̏����𐧌䂷��
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
    void Die()// ���S�������s��
    {
        Destroy(this.EnemyHeart);
        Hand.SetActive(false);
        anim.SetTrigger("Die");
        MagicCircle.SetActive(true);
        Debug.Log("�G�����ɂ܂���?");
    }


    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay); // �U���J�n�܂ł̑ҋ@���Ԃ�҂�
        transform.Translate(Vector3.zero); //�������Ȃ�
        isDelaying = false; // �ҋ@���ł͂Ȃ��Ȃ�
        // 1�ڂ̍U��
        transform.LookAt(target.position); // �v���C���[�̕���������
        anim.SetTrigger("Attack"); // �U���A�j���[�V�������Đ�
        isAttacking1 = true; // �U�����ɐݒ�
        StartCoroutine(AttackCooldown()); // �U���N�[���_�E�����J�n
    }

    IEnumerator SecondAttack()
    {
        yield return new WaitForSeconds(attackDelay); // 1�b�҂�
        transform.Translate(Vector3.zero); //�������Ȃ�
        isDelaying = false; // �ҋ@���ł͂Ȃ��Ȃ�
        isAttacking2 = true;//�U�����ɐݒ�
        Hand.SetActive(false); // �U�����łȂ����Hand�I�u�W�F�N�g���A�N�e�B�u�ɂ���
        transform.Translate(Vector3.zero); //�������Ȃ�
        transform.Translate(Vector3.zero);
        //anim.SetBool("Attack1",true); // �U���A�j���[�V�������Đ�
        anim.SetTrigger("Attack1");
        //�V�[�h��ݒ肵�ă����_���Ȉʒu�̌v�Z�ɉe����^����
        System.Random rand = new System.Random((int)DateTime.Now.Ticks);

        for (int i = 0; i < 200; i++)
        {
            if (enemyLife.Max == 0) { break; }//���񂾂Ƃ�������~�点�Ȃ�����
            transform.LookAt(target);
            Vector3 weaponPos = new Vector3(UnityEngine.Random.Range(-5f, 5f), 30f, UnityEngine.Random.Range(-5f, 5f));
            GameObject weapon = Instantiate(weaponPrefab, target.position + weaponPos, Quaternion.Euler(180f, 0f, 0f));
            Rigidbody weaponRb = weapon.AddComponent<Rigidbody>();
            weaponRb.AddForce(Vector3.down * 1000f);
            Destroy(weapon, 1.5f);
            yield return new WaitForSeconds(0.1f);
        }
        anim.SetBool("rest", true);//Attack2Cooldown�ɏ����ƃA�j���[�V���������Ă���Ȃ����߂����ɋL�q���� 
        StartCoroutine(Attack2Cooldown()); // �U���N�[���_�E�����J�n
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attack1Cooldown); // �U���N�[���_�E����҂�
        Hand.SetActive(false); // �U�����łȂ����Hand�I�u�W�F�N�g���A�N�e�B�u�ɂ���
        isAttacking1 = false; // �U�����ł͂Ȃ��Ȃ�
        Debug.Log("��p��");
    }

    IEnumerator Attack2Cooldown()
    {
        yield return new WaitForSeconds(attack2Cooldown); // �U���N�[���_�E����҂�
        Hand.SetActive(false); // �U�����łȂ����Hand�I�u�W�F�N�g���A�N�e�B�u�ɂ���
        isAttacking2 = false; // �U�����ł͂Ȃ��Ȃ�
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
            Debug.Log("�G��HP���ύX���ꂽ");
        }

        if (enemylife <= 0)//�{���̓X�e�[�g�ŊǗ�����͂������o�O�邽�ߒ��ڊ֐����Ăяo���Ă���B
        {
            Die();
        }
        else if (enemylife > 0)
        {
            // �U�����̏���
            if (isAttacking1)
            {
                HandleAttack1Animation();
            }
        }
    }
}


