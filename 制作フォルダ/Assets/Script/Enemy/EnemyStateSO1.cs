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

        public enum type//«—ˆ“I‚É‚Â‚¯‚é
        {
            Normal,
            Dark,
            light,
        }
  
        public int EnemyHP { get => HP;}
        public int EnemyAttack { get => Attack; }
        public int EnemyDF { get => DF; }//¡‚Íg‚í‚È‚¢‚ª«—ˆ“I‚É‚Â‚¯‚é
    }
    
}
