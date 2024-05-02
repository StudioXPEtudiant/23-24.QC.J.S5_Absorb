using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttack : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode MeleeKey = KeyCode.Mouse0;
    
    public bool onCooldown = false;
    public float cooldownDuration = 0.5f;
    public float attackRange = 2.5f;
    public int activeFrames = 5;
    public int attackDamage;
    public LayerMask enemyLayer;

    private bool connectHit = false;

    public GameObject weapon;
    private Animator anim;
    public Transform hitboxPos;

    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!onCooldown)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        onCooldown = true;
        Animator anim = weapon.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        audioSource.PlayOneShot(clip, volume);
        
        for(int i = 0; i < activeFrames; i++)
        {    
            Collider[] hitEnemies = Physics.OverlapSphere(hitboxPos.position, attackRange, enemyLayer);
            
            foreach(Collider enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().Decrease(20);
                connectHit = true;
            }

            if(connectHit == true)
                break;
        }
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        onCooldown = false;
    }
}
