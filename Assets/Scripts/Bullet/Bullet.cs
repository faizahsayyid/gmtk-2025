using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float lifetime = 5f;
    public float damage = 1f;

    private Rigidbody2D rb;
    // private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
    
    }

    public void Initialize(Vector2 shootDirection)
    {
        Vector2 direction = shootDirection.normalized;
        rb.linearVelocity = direction * speed;
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Flip sprite based on direction
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = shootDirection.x < 0; // Flip when going left
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "TempPlatform")
        {
            Destroy(gameObject);
        }
    }
}