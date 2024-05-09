using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    public int maxEnemyHealth;

    public bool _dead;
    public bool absorbable = false;

    private Rigidbody rb;
    public AudioSource audioSource;
    public AudioClip hit_sfx;

    public float volume = 0.5f;

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
        audioSource.PlayOneShot(hit_sfx, volume);
    }

    private void Death()
    {

        if(enemyHealth <= 0)
        {
            _dead = true;
            absorbable = true;

            rb.AddForce(transform.forward * -3f, ForceMode.Impulse);
        }
    }

    void Update()
    {
        Death();
    }
}
