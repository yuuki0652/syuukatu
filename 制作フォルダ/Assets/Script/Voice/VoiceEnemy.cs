using UnityEngine;

public class VoiceEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource aud;
    public AudioClip Attack1, Attack2, KnockBackvoice3, Die4;
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }
    public void DragonAttack2Voice1()
    {
        aud.PlayOneShot(Attack1);
    }
    public void DragonAttackVoice2()
    {
        aud.PlayOneShot(Attack2);
    }
    public void DragonDownVoice()
    {
        aud.PlayOneShot(KnockBackvoice3);
    }

    public void DragonDie()
    {
        aud.PlayOneShot(Die4);
    }
}
