using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject,10f);
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag =="Player")
        {
            Destroy(other.gameObject);
            Debug.Log("êGÇÍÇƒÇÈ");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyWapon2")
        {
            Destroy(other.gameObject);
            Debug.Log("êGÇÍÇƒÇÈ");
        }
    }
}
