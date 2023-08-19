using System.Collections;
using UnityEngine;

public class EnemyLife : MonoBehaviour,EnemyHitIDamegeble
{ 
    // Start is called before the first frame update
    public EnemyProgressBar pb;//�G��HP

    public int Max;//Valu�̒l�����邽�߂̕ϐ�(���������Ȃ�)
    public int valu=15000;
    private int DMG;
    [SerializeField] private PlayerDataBase playerDataBase;//�v���C���̒��g�Q��
    private PlayerStateSO SO;
    public float damageMultiplier; // �_���[�W�{��
    private GameObject ComboSystemScript;
    private GameObject ScoreSystemScript;
    private float ConboAttackTime = 4f;//�R���{�Ɠ�������
    private float timer = 0f;
    private int ConboAttackMG;
    private int DMG2;

    private Renderer enemyRenderer; // �G�I�u�W�F�N�g�̃����_���[
    private Color originalColor; // �I���W�i���̐F
    private Coroutine blinkCoroutine; // �_�ŗp�̃R���[�`��
    [SerializeField] GameObject EnemyRender;
    private float blinkTimer = 1.0f; // �ǉ��F�_�ł̏I���܂ł̎���
    [SerializeField] private ParticleSystem DMG1Particle;

    void Start()
    {
        Max = valu;//�ő�HP�ۑ�
        SO = playerDataBase.playersatasList[0];
        ComboSystemScript = GameObject.FindWithTag("ComboTag");//��̃^�O�擾
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
            if (timer > ConboAttackTime)//n�b�𒴂�����R���{���O
            {
                ResetConboAttack();
                Debug.Log("����܂���");
            }
        }
    }

    public void PlayerAttackDamage(int EnemyLife)//�v���C���̃}�W�b�N�U�����󂯂�
    {
        Max -= EnemyLife;//�v���C���}�W�b�N�_���[�W�v�Z������(��_���[�W���Ŏ���)
    }
    public void OnTriggerEnter(Collider other)
    {
        EnemyHitIDamegeble Enemydamegeble = gameObject.GetComponent<EnemyHitIDamegeble>();//�_���[�W���[��(�G���擾)
        if (other.gameObject.tag == "Mgic1")
        {
            DMG = (SO.PlayerMagic / 100 * 30 * (int)damageMultiplier) + DMG2;//�}�W�b�N�͂�30%���_���[�W�Ƃ��ė^����
            Instantiate(DMG1Particle, this.transform.position, Quaternion.identity);
            if (Enemydamegeble != null)
            {
                PlayerAttackDamage(DMG);//�����Ń_���[�W��^����(�ˑ����̏��Ȃ��C���^�[�t�F�C�X�Ń_���[�W�������s��)
                ComboSystemScript.GetComponent<ComboSystem>().IncreaseCombo();//�R���{�̐����Ăяo��
                ScoreSystemScript.GetComponent<Score>().IncreaseScore();//�X�R�A���Ăяo�� 
                ConboAttackMG++;
                IncreaseConboAttack();
                // �_�ŃR���[�`�����J�n
                if (blinkCoroutine == null)
                {
                    blinkCoroutine = StartCoroutine(BlinkEnemy());
                }

                Debug.Log(DMG + "�v���C���̃}�W�b�N�U������������");
            }
        }
    }
    void ResetConboAttack()
    {
        timer = 0f;
        DMG2 = 0;//�ǉ��_���[�W�����Ƃɖ߂�
        ConboAttackMG = 0;
    }
    public void IncreaseConboAttack()
    {
        timer = 0f;//�R���{������^�C�}�[���Z�b�g
        if (ConboAttackMG >= 1)//�ŏ��������ʂ̔{��
        {
            DMG2 += 50;//�U���𑱂���قǒǉ��_���[�W���グ��
            if (DMG2 >= 1000)//�ǉ��_���[�W1000�����
            {
                DMG2 = 1000;
            }
            Debug.Log("�ǉ��_���[�W" + DMG2);
        }
    }
    IEnumerator BlinkEnemy()
    {
        int blinkDuration = 3; // �_�ł����
        float blinkInterval = 0.2f; // �_�ł̊Ԋu
        int timer = 0;
        bool isBlinking = true;

        // �I���W�i���̐F��ۑ�
        Color originalColor = enemyRenderer.material.color;

        while (isBlinking)
        {
            if (timer >= blinkDuration)
            {
                isBlinking = false;
                timer = 0;
                break;
            }

            // �����_���[�̐F��ύX���ē_�ł�����
            enemyRenderer.material.color = Color.red;
            yield return new WaitForSeconds(blinkInterval);

            enemyRenderer.material.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);
            timer++;
        }

        // �_�ŏI����A�F�����ɖ߂�
        //enemyRenderer.material.color = originalColor;
        OrizinalColers();
        Debug.Log("�F�ω�");
        blinkCoroutine = null;
    }

    public void OrizinalColers()
    {
        enemyRenderer.material.color = originalColor;
    }
}