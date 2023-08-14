using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStateSO : ScriptableObject
{
    [SerializeField] int HP;
    [SerializeField] int MP;
    [SerializeField] int Attack;
    [SerializeField] int Magic;
    [SerializeField] int DF;

    public int PlayerHP { get => HP;}
    public int PlayerAttack { get => Attack; }
    public int PlayerMagic { get => Magic; }
    public int PlayerDF { get => DF; }
}
