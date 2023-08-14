using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class MagicPrefab : MonoBehaviour
{
    public GameObject magicBallPrefab;  // ���@�̋���Prefab
    public GameObject Ball { get; private set; }

    public float ballSpeed = 10.0f;     // ���@�̋��̑��x
    public float ballLifeTime = 1.5f;   // ���@�̋��̎���
    public Transform target;            // �ǐՂ���^�[�Q�b�g��Transform

    public void Prefab()//�A�j���[�V�����Ń{�[�����o��
    {
        // �v���C���[�̑O���Ɍ����Ė��@�̋��𐶐�����
        Ball = Instantiate(magicBallPrefab, transform.position + transform.forward * 2.0f, transform.rotation);

        // ���@�̋��� Rigidbody �R���|�[�l���g������ꍇ�A�ǐՃR���|�[�l���g��ǉ�����
        Rigidbody rb = Ball.GetComponent<Rigidbody>();

        // �ǐՂ���^�[�Q�b�g���ݒ肳��Ă���ꍇ�A�^�[�Q�b�g�Ɍ����Đi��
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * ballSpeed;
            Ball.transform.LookAt(target);
        }
        else
        {
            // �ǐՂ���^�[�Q�b�g���Ȃ��ꍇ�́A�v���C���[�̑O���ɐi��
            rb.velocity = transform.forward * ballSpeed;
        }

        // ���@�̋�����莞�Ԍ�ɔj������
        Destroy(this.Ball, ballLifeTime);
    }
}

