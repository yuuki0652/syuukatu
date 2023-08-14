using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic1DamegeScript : MonoBehaviour
{
    private AudioSource aud;
    private bool isHitStopped = false;//ヒットストップしているかどうか
    private float hitStopDuration = 0.15f;//0.15秒のみヒットストップ
    private float hitStopTimer = 0f;//ヒットストップを元に戻す
    public void Start()
    {
        aud = GetComponent<AudioSource>();
    }
    public void HitStop()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        isHitStopped = true;
        hitStopTimer = 0f;
    }

    public void Update()
    {
        if (isHitStopped)
        {
            hitStopTimer += Time.unscaledDeltaTime;
            if (hitStopTimer >= hitStopDuration)
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f;
                isHitStopped = false;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            aud.PlayOneShot(aud.clip);
            HitStop();
            Destroy(gameObject, 1);//Update関数を発動するためにオブジェクトを消すのを遅らせている
        }
        
    }
}
