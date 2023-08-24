using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerMoveMent : MonoBehaviour//�v���C���̍s���������ׂ�(�o�O�Ȃǂ����������ꍇ�Ɍ�����̂�e�Ղɂ��邽�߃v���C���s�������͑S�Ă����ɋL�ڂ��Ă���)
{
    private Rigidbody rb;
    private CapsuleCollider cap;//�v���C���̃R���C�_

    [SerializeField] private GameObject PlayrCanvas;
    [SerializeField] private GameObject PlayerDieCanvas;

    [SerializeField] private Player3 player3;//�`���[�g���A���ɕK�v�ȃu�[���ϐ�������Ă���

    private Animator anim;

    //----------------------------------����֌W
    private float evade;//���݂̉��������ϐ�
    private float evadeTime;//������鎞�Ԃ�����ϐ�
    private const float evadenumber = 1f;//������鎞��
    private const float evadeUse = 30f;//������g���ϐ�
    private bool evadeClicked = false;//���
    private GameObject PlayerSt;//�v���C���̒��ɓ����Ă���X�e�[�^�X���Q�Ƃ��邽�߂̕ϐ�
    //--------------------------------------------------------------------

    //--------------------------------------�m�b�N�o�b�N�֌W
    public float KnockBackPower;
    [SerializeField] private const float KnockTime = 1;//�m�b�N�o�b�N���鎞��
    private float KnockBackTime;//�m�b�N�o�b�N���鎞�Ԃ�����ϐ�
    public Transform Enemy;
    public bool isKnockBack;
    //--------------------------------------

    //--------------------------------------�J�����֌W
    public Camera camera;

    private const float CameraHeightPos = 1.5f;//�v���C���J�����̍��������߂�ϐ�

    public const float moveSpeed = 6f;

    public float x_sensi;//�J�����̃X�s�[�h

    public float y_sensi;

    private const float potisionResetY = -20;//�v���C���������ꗎ�������ɖ߂�l���`�����ϐ�

    public const float cameraDistance = 5f;//�J�����̌��͈̔�

    public const float cameraHeight = 2f;//�J�����̏������

    private float currentHeight;//���݂̍�����ۑ�����ϐ�

    private Vector3 cameraAngle;

    [SerializeField]
    private LayerMask obstacleLayer;//�J�������߂荞�܂Ȃ��Ƃ�������߂��
    //----------------------------------------------------------------------

    private Vector3 gravity;//�d��

    static readonly int hashAttackType = Animator.StringToHash("AttackType");

    public bool Playerevadeing = false;//������m�F����u�[��

    [SerializeField] private GameObject evadeGameObj;

    void Start()
    {
        cap = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        evadeTime = evadenumber;//������鎞�Ԃ�����
        PlayerSt = GameObject.FindWithTag("Player");//�v���C���̃^�O��T��
        KnockBackTime = KnockTime;
        PlayrCanvas.SetActive(true);
        PlayerDieCanvas.SetActive(false);//�ŏ��͎���ł��Ȃ��̂ŊJ���Ȃ�
        anim = GetComponent<Animator>();
        gravity = Physics.gravity;//�������y�����邽��Start�ŏd�͂�������
        cameraAngle = transform.rotation.eulerAngles;
        evadeGameObj.SetActive(false);//�ŏ��͉���o���A�͌����Ȃ�
    }

    public void Playermove()
    {
        if (anim.GetBool("Attack") == true) return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        currentHeight = transform.position.y;//���݂̍�����ۑ�

        Vector3 moveDir = camera.transform.TransformDirection(new Vector3(h, 0, v));
        moveDir.y = 0;
        moveDir.Normalize();

        float move = moveSpeed * Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));//�X�e�B�b�N�̌v�Z

        if (rb.velocity.y < 0) //�������y�����邽�߂ɗ������̂ݏd�͌v�Z����
        {
            Vector3 GravityVelocity = new Vector3(0, gravity.y, 0) * Time.deltaTime;//�d�͌v�Z
            rb.velocity += GravityVelocity;
            if (rb.velocity.y < potisionResetY)
            {
                SetPosition();//���������猳�ɖ߂�֐�
            }

        }

        rb.velocity = moveDir * move + new Vector3(0, rb.velocity.y, 0);//�d�͂�������

        if (move > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            float rotationSpeed = 8f;//�x���قǉ�]�̑��x��x������
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);//��]��⊮����
            player3.Tutorial1Goukaku = true;//�`���[�g���A���P���i
            animSpeed(moveSpeed);//�ړ��̃A�j���[�V�����Ɋւ���֐�
        }
        else if (move == 0)
        {
            animSpeed(0);//�ړ��̃A�j���[�V�����Ɋւ���֐�
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (player3.Tutorial4)//�`���[�g���A��4
            {
                animAttack(0);//�U���Ɋւ���֐�
                player3.Tutorial4Goukaku = true;//�`���[�g���A��4���i
            }
        }
    }
    public void cameracon()
    {
        Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (Input.GetMouseButton(1))
        {
            if (player3.Tutorial2)//�`���[�g���A��2���N���A�����瓮��
            {
                float y_Rotation = delta.y * y_sensi;
                cameraAngle.x -= y_Rotation;

                float x_Rotation = delta.x * x_sensi;
                cameraAngle.y += x_Rotation;

                player3.Tutorial2Goukaku = true;//�ړ��̃`���[�g���A�����i
}
        }

        Quaternion cameraRotation = Quaternion.Euler(cameraAngle);
        Vector3 cameraOffset = cameraRotation * new Vector3(0, cameraHeight, -cameraDistance);

        // ���C�L���X�g���΂��ăJ�����̈ړ����␳
        RaycastHit hit;
        if (Physics.Raycast(transform.position, cameraOffset.normalized, out hit, cameraOffset.magnitude, obstacleLayer))
        {
            cameraOffset = hit.point - transform.position;
        }

        camera.transform.position = transform.position + cameraOffset;
        camera.transform.rotation = Quaternion.LookRotation(transform.position + new Vector3(0, CameraHeightPos, 0) - camera.transform.position);
    }

    public void Evasivel()
    {
        if (evade >= evadeUse)//����Q�[�W��30�ȏ�Ȃ��Ɖ���ł��Ȃ�
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EvasiveButtonClicked();//�����true�ɂ���
                player3.Tutorial3Goukaku = true;//�`���[�g���A��3���i
            }
            if (evadeClicked)//����{�^��
            {
                rb.velocity = Vector3.zero;//��𒆈ړ����󂯕t���Ȃ�
                Playerevadeing = true;//��𒆂��m�F�i��𒆂ɍU�����󂯂Ȃ��悤�ɂ��邽�߂̃u�[�����j
                cap.height = 0; // �J�v�Z���R���C�_�[�̕ό`�������s��(�����ƒn�ʂ��痎���邽�ߕό`�����Ă���)
                rb.AddForce(transform.forward * 11f, ForceMode.VelocityChange);
                anim.SetTrigger("Loling");//������[�V����
                evadeTime -= Time.deltaTime;//������鎞�Ԃ����炷
                if (evadeTime <= 0f)
                {
                    Playerevadeing = false;//��𒆏I���i��𒆂ɍU�����󂯂Ȃ��悤�ɂ��邽�߂̃u�[�����j
                    PlayerSt.GetComponent<AvoidanceSC>().MaxAvoidance -= evadeUse;//�������ƃQ�[�W�����炷
                    evadeClicked = false;
                    evadeTime = evadenumber;//����̎��Ԃ�ύX����
                    cap.height = 1.871724f;// �J�v�Z���R���C�_�[�̕ό`�����ɖ߂��������s��(�L�����N�^�ɂ���Đ��͕ς��)
                }
            }
        }

        if(Playerevadeing == true)//��𒆂ɉ���̌����ڂɂ���
        {
            evadeGameObj.SetActive(true);//����̃o���A�I�u�W�F�N�g���o��
        }
        else
        {
            evadeGameObj.SetActive(false);//����̃o���A�I�u�W�F�N�g������
        }
    }

    public void KnockBack()
    {
        if (isKnockBack)//�m�b�N�o�b�N�����Ƃ�
        {
            rb.velocity = Vector3.zero;//�m�b�N�o�b�N���ړ����󂯕t���Ȃ�
            Vector3 distination = (transform.position - Enemy.transform.position).normalized;//�����ƓG�̋������v�Z���āA�����ƕ������o���Đ��K������
            rb.AddForce(distination * KnockBackPower, ForceMode.VelocityChange);//�m�b�N�o�b�N������
            KnockBackTime -= Time.deltaTime;
            anim.SetBool("KnockBack", true);
            if (KnockBackTime < 0f)
            {
                Debug.Log("�m�b�N�o�b�N�\");
                isKnockBack = false;
                KnockBackTime = KnockTime;
                anim.SetBool("KnockBack", false);
            }
        }
        Debug.Log(KnockBackTime);//�m�b�N�o�b�N�����鎞�Ԃ𒲂ׂ�

    }
    public void PlayerDie()
    {
        anim.SetTrigger("Die");
        cap.height = 0.5f;//�J�v�Z���R���C�_�[�𖳌��ɂ���Əd�͂ŗ����邽�߃R���C�_�̐�����ύX����
        cap.center = new Vector3(0, 0, 0);
        cap.radius = 0.2f;
        UnityEngine.Cursor.visible = true;//�J�[�\�����o��
        PlayrCanvas.SetActive(false);
        PlayerDieCanvas.SetActive(true);//���񂾂̂őI����ʂ��J��
        Debug.Log("���ɂ܂���");
    }
    bool EvasiveButtonClicked()
    {
        evadeClicked = true;//����C�x���g
        return evadeClicked;
    }
  public void evadTime()
    {
        evade = PlayerSt.GetComponent<AvoidanceSC>().MaxAvoidance;//���������
    }
    private void SetPosition()//���������猳�ɖ߂�֐�
    {
        // �w�肳�ꂽ�ʒu�ɃI�u�W�F�N�g���ړ�����
        Vector3 WakePosition = new Vector3(0, 20, 0);
        rb.transform.position = WakePosition;
    }
    private void animSpeed(float Speed)
    {
        anim.SetFloat("Speed", Speed);
    }
    private void animAttack(int Type)
    {
        anim.SetTrigger("Attack");
        anim.SetInteger(hashAttackType, Type);
    }
}
