using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Event2 : MonoBehaviour
{
    private PlayableDirector event2;
    private int Life;
    private bool hasEventPlayed = false;
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyLife enemylife;
    [SerializeField] private GameObject EnemyHPBar;
    private void Start()
    {
        event2 = GetComponent<PlayableDirector>();
    }
    private void Update()
    {
        if (!hasEventPlayed)
        {
            try
            {
                Life = enemylife.Max;
            }
            catch
            {
                //�������Ȃ�
            }
            if (Life <= 0)//HP���O�ȉ��̎�
            {
                try
                {
                    enemylife.OrizinalColers();//���̐F�ɖ߂�
                }
                catch 
                {
                    //�擾���Ȃ�����try�֐��ŋL�q
                }
                Invoke("moveStart", 2.0f);//�C�x���g2���s
                enemy.enabled = false;//�G�L������~
                EnemyHPBar.SetActive(false);//�GHP�o�[����
            }
        }
        else
        {
            if (event2.state == PlayState.Paused)
            {
                SceneManager.LoadScene("Ending");
            }
        }
    }
    private void moveStart()
    {
        event2.Play();//�C�x���g2���s
        hasEventPlayed = true;
    }
}

