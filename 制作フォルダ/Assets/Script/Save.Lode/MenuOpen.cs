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

    // ���j���[���J����
    public void ToggleMenu()
    {
        menuFlag = !menuFlag;

        if (menuFlag)
        {
            menu.SetActive(true);
            UnityEngine.Cursor.visible = true;//�J�[�\���\��
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = 0;//���Ԃ��~�߂�
        }
        else
        {
            menu.SetActive(false);
            UnityEngine.Cursor.visible = false;//�J�[�\����\��
            Time.timeScale = 1;
        }
    }
}
