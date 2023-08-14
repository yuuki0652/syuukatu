using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    public GameObject playerPos;
   
    public void GoSave()
    {
       PlayerPrefs.SetFloat("PlayerX", playerPos.transform.position.x);
       PlayerPrefs.SetFloat("PlayerY", playerPos.transform.position.y);
       PlayerPrefs.SetFloat("PlayerZ", playerPos.transform.position.z);
       PlayerPrefs.Save();
    }
    public void Lode()
    {
       playerPos.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX", 0), PlayerPrefs.GetFloat("PlayerY", 0), PlayerPrefs.GetFloat("PlayerZ", 0));//åªç›à íuÇÉçÅ[Éh
    }
    public void Delete()
    {
        PlayerPrefs.DeleteAll();
    }
}
