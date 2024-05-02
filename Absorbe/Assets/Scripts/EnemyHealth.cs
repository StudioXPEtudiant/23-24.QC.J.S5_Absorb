using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    public int maxEnemyHealth;

    public bool _dead;
    public bool absorbable;

    private Rigidbody rb;

    void Awake()
    {
        enemyHealth = maxEnemyHealth;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Decrease(int damage)
    {
        enemyHealth -= damage;
    }

    private void Death()
    {
        if(enemyHealth <= 0)
        {
            _dead = true;
            absorbable = false;

            rb.AddForce(transform.forward * -2f, ForceMode.Impulse);
        }
    }

    void Update()
    {
        
    }
}
