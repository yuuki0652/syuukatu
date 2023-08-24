using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour,PlayerHitIDamegeble
{
    // Start is called before the first frame update
    public ProgressBar progeruBar;
    [SerializeField] private PlayerDataBase playerDataBase;

    public int Max;//Valu�̒l�����邽�߂̕ϐ�(�ς���K�v�Ȃ�)
    public int valu = 60;

    private const int Dmg = 150;//�_���[�W
    private const int Dmg2 = 50;//�_���[�W
    private float DmgCoolTime1 = 3.0f;//�_���[�W�N�[���^�C����ݒ肷��
    private float damageTimer1 = 0f;//�ύX�s��

    [SerializeField] private GameObject PlayerRend;
    private Renderer playerRenderer; // �v���C���̃����_���[
    private Color originalColor; // �I���W�i���̐F
    private Coroutine blinkCoroutine; // �_�ŗp�̃R���[�`��

    [SerializeField] private PlayerMoveMent playerMovement;
    void Start()
    {
        PlayerStateSO SO = playerDataBase.playersatasList[0];
        valu = SO.PlayerHP;//�f�[�^�x�[�X��̃v���C����HP�Q��
        Max = valu;//�ő�HP�ۑ�
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
        if (damageTimer1 > 0f)//Wapon1�̃N�[���^�C��
        {
            damageTimer1 -= Time.deltaTime;
        }
    }
    public void EnemyAttackDamage(int playerLife)
    {
        Max -= playerLife;//�G�̃��C�����̃_���[�W�v�Z��(��_���[�W��)
    }

    public void PlayerAttackDamage(int EnemyLife)
    {
        //�v���C���̗��̂��߉��������Ȃ�
    }
    public void OnTriggerEnter(Collider other)
    {   //��ڂ̕���
        PlayerHitIDamegeble playerdamegeble = gameObject.GetComponent<PlayerHitIDamegeble>();
        PlayerMoveMent movement = gameObject.GetComponent<PlayerMoveMent>();
        if (other.gameObject.tag == "EnemyWapon1" && damageTimer1 <= 0f)//�G�̍U�����(��X�U���𑝂₵�Ă���)
        {
            if (playerMovement.Playerevadeing == false)//��𒆂̓_���[�W����
            {
                // �v���C���ŃR���[�`�����J�n
                if (blinkCoroutine == null)
                {
                    blinkCoroutine = StartCoroutine(BlinkEnemy());
                }
                playerdamegeble.EnemyAttackDamage(Dmg);//HP�����炷��
                Debug.Log("�����v���C����HP��^����");
                damageTimer1 = DmgCoolTime1;//�_���[�W�^�C�}�[�Ƀ_���[�W�N�[���^�C��n�b����������
                movement.isKnockBack = true;//PlayerMovent�ɓ����Ă���m�b�N�o�b�N��true
            }
        }
        //-----------------------------------------------------------------------------
        if (other.gameObject.tag == "EnemyWapon2" && damageTimer1 <= 0f)//�G�̍U��2��(��X�U���𑝂₵�Ă���)
        {
            if (playerMovement.Playerevadeing == false)//��𒆂̓_���[�W����
            {
                // �v���C���ŃR���[�`�����J�n
                if (blinkCoroutine == null)
                {
                    blinkCoroutine = StartCoroutine(BlinkEnemy());
                }
                playerdamegeble.EnemyAttackDamage(Dmg2);//HP�����炷��(�C���^�[�t�F�[�X�Ŏ���)
                Debug.Log("�J���v���C����HP��^����");
                damageTimer1 = DmgCoolTime1;//�_���[�W�^�C�}�[�Ƀ_���[�W�N�[���^�C��n�b����������
                movement.isKnockBack = true;//PlayerMovent�ɓ����Ă���m�b�N�o�b�N��true
            }
        }
    }
    IEnumerator BlinkEnemy()
    {
        int blinkDuration = 2; // �_�ł����
        float blinkInterval = 0.2f; // �_�ł̊Ԋu
        int timer = 0;
        bool isBlinking = true;

        // �I���W�i���̐F��ۑ�
        Color originalColor = playerRenderer.material.color;

        while (isBlinking)
        {
            if (timer >= blinkDuration)
            {
                isBlinking = false;
                timer = 0;
                break;
            }

            // �����_���[�̐F��ύX���ē_�ł�����
            playerRenderer.material.color = Color.red;
            yield return new WaitForSeconds(blinkInterval);

            playerRenderer.material.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);
            timer++;
        }

        // �_�ŏI����A�F�����ɖ߂�
        playerRenderer.material.color = originalColor;
        blinkCoroutine = null;
    }
}
