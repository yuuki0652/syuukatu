using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Event1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private PlayableDirector PlayableDirector;

    public bool event1 = true;
    [SerializeField]
    public GameObject BGM;

    public bool AllCrearTutorial=false;
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (AllCrearTutorial)//�S�Ẵ`���[�g���A�����N���A������s����
            {
                if (event1)
                {
                    PlayableDirector.Play();
                    Debug.Log("�C�x���g�J�n");
                    BGM.SetActive(true);
                    event1 = false;//��x�̂�
                }
            }
            
        }
    }
}
