using UnityEngine;
using System.Collections;

public class StunBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 8f;
    public float lifetime = 3f;
    public float damage = 5f;

    [Header("Stun Effect")]
    public float stunDuration = 2f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Tags.Bird) || col.CompareTag(Tags.Player) || col.CompareTag(Tags.Ground))
        {
            Destroy(gameObject);
        }
    }
}