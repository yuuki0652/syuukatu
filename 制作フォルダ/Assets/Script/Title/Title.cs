using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    [SerializeField] private float blinkInterval = 0.5f; // 点滅する間隔（秒）
    private AudioSource aud;
    [SerializeField] private AudioClip TitleCole1;

    private bool isMouseOver = false;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();//ゲーム終了
        }
    }
    public void next()
    {
        aud.PlayOneShot(TitleCole1);  
        SceneNavigator.Instance.Change("mainGame", 1.0f);
    }
    public void MouseEnter()
    {
        isMouseOver = true;
        StartCoroutine(Blink());
    }

    public void MouseExit()
    {
        isMouseOver = false;
        StopCoroutine(Blink());
        Color textColor = textMesh.color;
        textColor.a = 1f;
        textMesh.color = textColor;
    }

    private IEnumerator Blink()
    {
        while (isMouseOver)
        {
            Color textColor = textMesh.color;
            textColor.a = 1f - textColor.a; // アルファ値を反転させることで点滅を実現
            textMesh.color = textColor;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
