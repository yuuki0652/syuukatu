using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mes : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI textpro;
    private GameObject enemyLife; // EnemyLifeコンポーネントの参照
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
        if (life <= HPMaxValu / 2 && one)//一度だけ発動する
        {
            firstMesg();
            Invoke("SecndMesg", 10f);
            Destroy(this.gameObject,15f);
            one = false;
        }
    }
    public void firstMesg()
    {
        string message = "まもなく斧の雨が降ってくる<color=#FF0000>バリア</color>の中に入り攻撃を防げ";
        textpro.text = message;
    }
    public void SecndMesg()
    {
       string message = "言い忘れていたが<color=#FF0000>バリア</color>は時間で消える";
        textpro.text = message;
    }
}
