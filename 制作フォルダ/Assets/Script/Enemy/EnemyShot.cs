using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject ball;
    private float ballSpeed = 10.0f;
    public float timer = 2.0f;
    private float time;
    public bool startAttack;

    private void Start()
    {
        startAttack = false;
        time = timer;
    }
    void Update()
    {
        if (startAttack) 
        {
            transform.LookAt(player.transform);
        
            time -= Time.deltaTime;
            if (time <= 0)
            {
                BallShot();
                time = timer;
            }
        }
    }

    void BallShot()
    {
        GameObject shotObj = Instantiate(ball, transform.position, Quaternion.identity);
        shotObj.GetComponent<Rigidbody>().velocity = transform.forward * ballSpeed;
    }
}
