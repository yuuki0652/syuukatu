using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStateSO1 : ScriptableObject
{
    public List<EnemyStatus> enemyStatusList = new List<EnemyStatus>();
    [System.Serializable]
    public class EnemyStatus
    {
          [SerializeField] string Name;
          [SerializeField] int HP;
          [SerializeField] int MP;
          [SerializeField] int Attack;
          [SerializeField] int DF;
          [SerializeField] type Type;

        public enum type//将来的につける
        {
            Normal,
            Dark,
            light,
        }
  
        public int EnemyHP { get => HP;}
        public int EnemyAttack { get => Attack; }
        public int EnemyDF { get => DF; }//今は使わないが将来的につける
    }
    
}
