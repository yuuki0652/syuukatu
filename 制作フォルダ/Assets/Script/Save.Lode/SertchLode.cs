using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SertchLode : MonoBehaviour
{
    // Start is called before the first frame update
     [SerializeField]private  GameObject TutorialMG;
  
    [SerializeField] private Event1 Event1Script;
    [SerializeField]private Player3 player3;
    [SerializeField]private  Event1 event1;

    private bool stop = true;

    private void Start()
    {
        TitleTutrial();
        TiTleTutrialDestroy();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (stop)
            {
                TutrialCrear();
                Event1Script.BGM.SetActive(true);
                Event1Script.event1 = false;
                stop = false;//��x�̂ݒʂ�
            }
        }
    }

    private void Update()
    {
        
    }
    public void TutrialCrear()
    {
        player3.Tutorial1 = true;//�`���[�g���A���͑S�ďI����Ă��鈵��
        player3.Tutorial2 = true;
        player3.Tutorial3 = true;
        player3.Tutorial4 = true;
        event1.AllCrearTutorial = false;//���ɐG���ƃ��[�r�[������Ă��܂�����
    }
    public void TitleTutrial()
    {
        player3.Tutorial1 = TutrialKakuninn.TitleTutrial1;
        player3.Tutorial2 = TutrialKakuninn.TitleTutrial2;
        player3.Tutorial3 = TutrialKakuninn.TitleTutrial3;
        player3.Tutorial4 = TutrialKakuninn.TitleTutrial4;
    }
    private void TiTleTutrialDestroy()
    {
        if (player3.Tutorial1 && player3.Tutorial2 &&player3.Tutorial3 && player3.Tutorial4)
        {
            event1.AllCrearTutorial = true;//�`���[�g���A���S�ăN���A
            Destroy(TutorialMG);
        }
    }
}
