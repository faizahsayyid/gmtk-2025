using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayMovement playerMovement;
    public SpriteRenderer playerSpriteRenderer;
    public Color stunColor = new Color(255, 234, 119, 255); // Yellow color for stun effect
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
        if (collision.gameObject.CompareTag(Tags.StunBullet))
        {
            StunBullet stunBullet = collision.gameObject.GetComponent<StunBullet>();
            if (stunBullet != null)
            {
                playerHealth.TakeDamage(stunBullet.damage);
                StartCoroutine(HandlePlayerStun(stunBullet.stunDuration));
                return;
            }
        }
    }

    IEnumerator HandlePlayerStun(float stunDuration)
    {
        playerMovement.enabled = false;
        playerSpriteRenderer.color = stunColor; // Change color to indicate stun
        yield return new WaitForSeconds(stunDuration); // Stun duration
        playerMovement.enabled = true;
        playerSpriteRenderer.color = Color.white; // Reset color after stun
    }

    void HandlePlayerDeath()
    {
        playerHealth.ShowBlackScreen();        
    }
}
