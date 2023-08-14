using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpen : MonoBehaviour
{
    public GameObject menu;
    private bool menuFlag = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToggleMenu();
        }
    }

    // メニューを開閉する
    public void ToggleMenu()
    {
        menuFlag = !menuFlag;

        if (menuFlag)
        {
            menu.SetActive(true);
            UnityEngine.Cursor.visible = true;//カーソル表示
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = 0;//時間を止める
        }
        else
        {
            menu.SetActive(false);
            UnityEngine.Cursor.visible = false;//カーソル非表示
            Time.timeScale = 1;
        }
    }
}
