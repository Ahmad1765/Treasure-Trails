using Unity.Profiling;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{   
    public int health = 50;

    public float detectionRadius = 10f; // Radius within which the enemy detects the player
    public float attackRadius = 2f;     // Radius within which the enemy attacks the player
    public float attackCooldown = 1f;   // Time between attacks
    public int damage = 10;             // Damage dealt to the player

    private Transform player;
    private NavMeshAgent agent;
    private float lastAttackTime;

    Animator anim;

    Transform enemyTrans;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionRadius && !IsPlayerHiding())
        {
            agent.SetDestination(player.position);
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsIdle", false);

            if (distanceToPlayer <= attackRadius)
            {
                agent.isStopped = true;
                AttackPlayer();
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            agent.SetDestination(transform.position);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsIdle", true);
        }
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Perform attack (e.g., reduce player's health)
            Debug.Log("Enemy attacks the player!");
            // Assuming the player has a method to take damage
            player.GetComponent<PlayerHealth>().TakeDamage(damage);

            lastAttackTime = Time.time;
        }
    }

    bool IsPlayerHiding()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, player.position, out hit))
        {
            if (hit.collider.CompareTag("Object"))
            {
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        // Draw the detection and attack radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsDead",true);
        Debug.Log("Enemy is dead");
        Destroy(gameObject);
    }
}
