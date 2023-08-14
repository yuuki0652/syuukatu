using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataBaseMG : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerDataBase playerDataBase;

    public void AddPlayerData(PlayerStateSO SO)
    {
        //plaDataBase.playersatasList.Add(SO);
    }

    private void Start()
    {
        int count = playerDataBase.playersatasList.Count;//playerstatusList‚Ì—v‘f
        for(int i = 0; i < count; i++)
        {
            PlayerStateSO SO = ScriptableObject.CreateInstance<PlayerStateSO>();
            SO = playerDataBase.playersatasList[i];
            Debug.Log(SO.PlayerHP);
        }
    }
}
