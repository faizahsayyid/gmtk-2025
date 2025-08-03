using UnityEngine;
using UnityEngine.SceneManagement;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Bird))
        {
            playerHealth.TakeDamage(1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag(Tags.Bullet))
        {
            Debug.Log("Bullet hit the player");
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                Debug.Log("Bullet script found, applying damage");
                playerHealth.TakeDamage(bullet.damage);
                return;
            }
        }
        if (collision.gameObject.CompareTag(Tags.BirdBullet))
        {
            BirdBullet birdBullet = collision.gameObject.GetComponent<BirdBullet>();
            if (birdBullet != null)
            {
                playerHealth.TakeDamage(birdBullet.damage);
                return;
            }
        }
    }

    void HandlePlayerDeath()
    {
        playerHealth.ShowBlackScreen();        
    }
}
