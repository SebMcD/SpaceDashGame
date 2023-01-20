using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyHealthPoints = 3;

    public int EnemyHealthPoints { 
        get
        {
            return enemyHealthPoints;
        }
        set
        {
            enemyHealthPoints = value;
        }
    }

    private void OnEnable()
    {
        enemyHealthPoints = 3;
    }

    public void UpdateEnemyHealth(int hp)
    {
        EnemyHealthPoints -= hp;
    }
}
