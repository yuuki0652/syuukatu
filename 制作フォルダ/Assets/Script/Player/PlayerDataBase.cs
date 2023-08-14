using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class PlayerDataBase : ScriptableObject
{
    public List<PlayerStateSO>playersatasList = new List<PlayerStateSO>();
}