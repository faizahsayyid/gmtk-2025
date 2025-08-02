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
        
    }
    
    public void Initialize(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
        rb.linearVelocity = direction * speed;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Flip sprite based on direction
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = shootDirection.x < 0; // Flip when going left
        }
    }
}