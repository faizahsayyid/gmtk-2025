using UnityEngine;

public class CollectibleStunBullet : MonoBehaviour
{
   
    public GameObject stunSpellPrefab; // Assign your comet sprite prefab in the Inspector
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
                stunSpellPrefab,
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

  
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider triggered" + other.name);
        Debug.Log("What is game object" + gameObject.name);

        
        Destroy(other.gameObject);
        if (gameObject.name == "Player")
        {
            Debug.Log("Spell collected: " + stunSpellPrefab.name);
            spellInventory.IncreaseSpellQuantity(2, 1); // Add the spell to the player's inventory

        }
    }
}