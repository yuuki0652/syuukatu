using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private EndingSUM endingsum;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void NextButon()
    {
        if (endingsum.scoreConboSUM >= 10000)//1���ȏ�ł����
        {
            SceneNavigator.Instance.Change("HappyEND", 1.0f);
        }
        else
        {
            SceneNavigator.Instance.Change("ButEND", 1.0f);
        }
    }
}
