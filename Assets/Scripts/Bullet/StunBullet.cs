using UnityEngine;
using System.Collections;

public class StunBullet : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 8f;
    public float lifetime = 3f;

    [Header("Stun Effect")]
    public float damage = 5f;
    public float stunDuration = 2f;

    private Vector2 direction;
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
        if (col.name == "TempPlatform")
        {
            Destroy(gameObject);
        }
    }
}