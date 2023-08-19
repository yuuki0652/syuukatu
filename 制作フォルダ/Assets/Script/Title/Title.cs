using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    [SerializeField] private float blinkInterval = 0.5f; // �_�ł���Ԋu�i�b�j
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
            Application.Quit();//�Q�[���I��
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
            textColor.a = 1f - textColor.a; // �A���t�@�l�𔽓]�����邱�Ƃœ_�ł�����
            textMesh.color = textColor;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
