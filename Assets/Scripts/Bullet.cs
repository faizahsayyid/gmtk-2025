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
