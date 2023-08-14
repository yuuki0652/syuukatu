using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public string sceneName;//リトライしたいゲームシーン
    public Vector3 playerStartPosition; // プレイヤーの初期位置
    public void Title()
    {
        SceneNavigator.Instance.Change("Title", 1.0f);
    }

    public void GameEND()
    {
        Application.Quit();//ゲーム終了
    }
    public void RetryMainGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//メインゲームをプレイする際バグを起こしたくないので再度読み込み処理を行う
                                                                   
        SceneManager.sceneLoaded += OnSceneLoaded;// シーンが再ロードされた後にプレイヤーの位置を変更する
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // プレイヤーを検索して位置を変更する
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerStartPosition;
        // イベントの解除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
