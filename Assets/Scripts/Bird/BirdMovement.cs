using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float stopDistance = 5f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
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
}
