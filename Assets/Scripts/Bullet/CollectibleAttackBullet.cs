using UnityEngine;

public class CollectibleAttackBullet : MonoBehaviour
{
   
    public GameObject attackSpellPrefab; // Assign your comet sprite prefab in the Inspector
    public int spawnCount = 3;     // Number of comets to spawn
    public Vector2 minBounds = new Vector2(-8, -4);
    public Vector2 maxBounds = new Vector2(8, 4);

    public SpellInventory spellInventory;

    private Rigidbody2D rb;
    // private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnManager.Instantiate(
                attackSpellPrefab,
                new Vector3(
                    Random.Range(minBounds.x, maxBounds.x),
                    Random.Range(minBounds.y, maxBounds.y),
                    0
                ),
                Quaternion.identity,
                false
            );
        }
         
    }

  
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Destroy(other.gameObject);
        if (gameObject.name == "Player")
        {
            Debug.Log("Spell collected: " + attackSpellPrefab.name);
            spellInventory.IncreaseSpellQuantity(1, 1); // Add the spell to the player's inventory
        }
    }
}
