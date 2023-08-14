using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceSC : MonoBehaviour
{
    // Start is called before the first frame update

    public ProgressBarCircle pbr;//回避ゲージ

    public float MaxAvoidance;//Valuの値を入れるための変数
    public int valu = 60;
    void Start()
    {
        MaxAvoidance = valu;//最大HP保存(フロート変換する)
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AvoidanceRecovery();
    }
    private void AvoidanceRecovery()
    {
        pbr.BarValue = MaxAvoidance;
        MaxAvoidance += 2 * Time.deltaTime;//時間の二倍速で回復する
        if (MaxAvoidance >= 100)
        {
            MaxAvoidance = 100;//100以上に行かないため
        }
    }
}
