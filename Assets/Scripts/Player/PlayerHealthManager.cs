using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    void OnEnable()
    {
        playerHealth.OnPlayerDeath += HandlePlayerDeath;
    }

    void OnDisable()
    {
        playerHealth.OnPlayerDeath -= HandlePlayerDeath;
    }

    void OnCollisionEnter2D(Collider2D col)
    {
        if (col.CompareTag(Tags.Enemy))
        {
            playerHealth.TakeDamage(1);
        }
    }
    
    void HandlePlayerDeath()
    {
        Debug.Log("Player has died.");
    }
}
