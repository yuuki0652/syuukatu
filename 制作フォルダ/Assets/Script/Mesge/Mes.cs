using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mes : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI textpro;
    private GameObject enemyLife; // EnemyLife�R���|�[�l���g�̎Q��
    private int HPMaxValu;
    private int life;
    private bool one = true;
    void Start()
    {
        textpro = GetComponent<TextMeshProUGUI>();
        enemyLife = GameObject.FindGameObjectWithTag("Enemy");
        HPMaxValu = enemyLife.GetComponent<EnemyLife>().valu;
        life = enemyLife.GetComponent<EnemyLife>().Max;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            life = enemyLife.GetComponent<EnemyLife>().Max;
        }
        catch
        {

        }
        if (life <= HPMaxValu / 2 && one)//��x������������
        {
            firstMesg();
            Invoke("SecndMesg", 10f);
            Destroy(this.gameObject,15f);
            one = false;
        }
    }
    public void firstMesg()
    {
        string message = "�܂��Ȃ����̉J���~���Ă���<color=#FF0000>�o���A</color>�̒��ɓ���U����h��";
        textpro.text = message;
    }
    public void SecndMesg()
    {
       string message = "�����Y��Ă�����<color=#FF0000>�o���A</color>�͎��Ԃŏ�����";
        textpro.text = message;
    }
}
