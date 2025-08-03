using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour
{

    [Header("General Settings")]
    public Animator animator;

    [Header("Player Tracking Settings")]
    public Transform player;
    [Header("Movement Settings")] public float moveSpeed = 3f;
    public float stopDistance = 5f;
    [Header("Attack Settings")]
    public float hostileRange = 15f;
    public float attackRange = 10f;
    public float fireRate = 1.5f;
    public float castDelay = 0.2f;
    public Transform firePoint;
    public GameObject attackPrefab;

    [Header("Health Settings")]
    public int maxHealth = 3;
    public int currentHealth;

    // Private Variables
    private SpriteRenderer spriteRenderer;
    private float nextFireTime;
    private enum BirdStates { Idle, Attacking, Stunned, Dead }
    private BirdStates currentState = BirdStates.Idle;


    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        // Check Player Distance
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= hostileRange && (currentState != BirdStates.Stunned && currentState != BirdStates.Dead))
        {
            currentState = BirdStates.Attacking;
            if (distance > stopDistance)
            {
                transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            }
            if (distance < attackRange)
            {
                EnemyShoot();
            }
            if (direction.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
        else if (distance > hostileRange && currentState != BirdStates.Dead)
        {
            currentState = BirdStates.Idle;
        }
    }

    // Shooting Function
    private void EnemyShoot()
    {
        if (Time.time >= nextFireTime && attackPrefab != null && firePoint != null)
        {
            nextFireTime = Time.time + fireRate;

            //animator.SetInteger(PlayerAnimationConstants.Accessor, PlayerAnimationConstants.Cast);

            StartCoroutine(FireBulletAfterDelayEnemy());
        }
    }
    private IEnumerator FireBulletAfterDelayEnemy()
    {
        yield return new WaitForSeconds(castDelay);

        Vector2 shootDirection = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(attackPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));

        BirdBullet bulletScript = bullet.GetComponent<BirdBullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(shootDirection);
        }
    }


    // Public Methods and Helpers
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        currentState = BirdStates.Dead;
        // TODO: 
        // Handle death (e.g., play animation, disable components)
    }
    public void Stun()
    {
        if (currentState != BirdStates.Dead)
        {
            currentState = BirdStates.Stunned;
            animator.SetBool("Stunned", true);
        }
    }

}
