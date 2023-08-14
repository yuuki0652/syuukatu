using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public string sceneName;//���g���C�������Q�[���V�[��
    public Vector3 playerStartPosition; // �v���C���[�̏����ʒu
    public void Title()
    {
        SceneNavigator.Instance.Change("Title", 1.0f);
    }

    public void GameEND()
    {
        Application.Quit();//�Q�[���I��
    }
    public void RetryMainGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//���C���Q�[�����v���C����ۃo�O���N���������Ȃ��̂ōēx�ǂݍ��ݏ������s��
                                                                   
        SceneManager.sceneLoaded += OnSceneLoaded;// �V�[�����ă��[�h���ꂽ��Ƀv���C���[�̈ʒu��ύX����
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �v���C���[���������Ĉʒu��ύX����
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerStartPosition;
        // �C�x���g�̉���
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
