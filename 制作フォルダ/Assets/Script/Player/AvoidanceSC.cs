using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceSC : MonoBehaviour
{
    // Start is called before the first frame update

    public ProgressBarCircle pbr;//����Q�[�W

    public float MaxAvoidance;//Valu�̒l�����邽�߂̕ϐ�
    public int valu = 60;
    void Start()
    {
        MaxAvoidance = valu;//�ő�HP�ۑ�(�t���[�g�ϊ�����)
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AvoidanceRecovery();
    }
    private void AvoidanceRecovery()
    {
        pbr.BarValue = MaxAvoidance;
        MaxAvoidance += 2 * Time.deltaTime;//���Ԃ̓�{���ŉ񕜂���
        if (MaxAvoidance >= 100)
        {
            MaxAvoidance = 100;//100�ȏ�ɍs���Ȃ�����
        }
    }
}
