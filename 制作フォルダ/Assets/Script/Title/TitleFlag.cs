using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFlag : MonoBehaviour
{
    
    public GameObject mainCanvas;
    public GameObject SetCanvas;
    private bool Titleflag = false;
    
    public void SetOpen()
    {
        switch(Titleflag)
        {
            case false:
                mainCanvas.SetActive(true);
                SetCanvas.SetActive(false);
                Titleflag = true;
                break;
                case true:
                mainCanvas.SetActive(false);
                SetCanvas.SetActive(true);
                Titleflag = false;
                break;
        }
    }
}
