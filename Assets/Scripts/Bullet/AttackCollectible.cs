using UnityEngine;

public class AttackCollectible : MonoBehaviour
{
    public SpellInventory spellInventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (spellInventory != null)
            {
                Debug.Log("Attack spell collected!");
                spellInventory.IncreaseSpellQuantity(0, 1); // Add attack spell to inventory
            }
            else
            {
                Debug.LogError("SpellInventory is null!");
            }
            
            Destroy(gameObject); // Destroy the collectible
        }
    }
}