using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbsorbEnemy : MonoBehaviour
{
    [Header("Detection")]
    public float reach = 20f;
    public LayerMask enemyLayers;
    private RaycastHit _hit;

    [Header("Keybinds")]
    public KeyCode absorbEnemy;

    [Header("References")]
    public Transform cam;
    
    private EnemyHealth enemyStatus;
    private EnemyHealth pointedEnemy;
    private Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
        absorbEnemy = KeyCode.E;
    }


    void Update()
    {
        DetectEnemy();
    }

    private void DetectEnemy()
    {
        pointedEnemy = null;
        if(Physics.Raycast(cam.position, transform.forward, out _hit, reach, enemyLayers))
            pointedEnemy = _hit.transform.GetComponent<EnemyHealth>();

        if(pointedEnemy == enemyStatus.absorbable)
        {
            Debug.Log("absorbing");
        }
            
    }
}
