using UnityEngine;

public class StunCollectible : MonoBehaviour
{
    public SpellInventory spellInventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (spellInventory != null)
            {
                Debug.Log("Stun spell collected!");
                spellInventory.IncreaseSpellQuantity(1, 1); // Add stun spell to inventory
            }
            else
            {
                Debug.LogError("SpellInventory is null!");
            }
            
            Destroy(gameObject); // Destroy the collectible
        }
    }
}