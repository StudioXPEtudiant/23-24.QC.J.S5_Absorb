using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    public int maxEnemyHealth;

    void Awake()
    {
        enemyHealth = maxEnemyHealth;
    }

    public void Decrease()
    {
        
    }

    void Update()
    {
        
    }
}
