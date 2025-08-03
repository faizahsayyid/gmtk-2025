using UnityEngine;

public class GameLoad : MonoBehaviour
{
    public PlayerSpawnDirection playerSpawnDirection;
    public PlayerHealth playerHealth;
    public SpellInventory spellInventory;
    private static GameLoad Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerSpawnDirection.SetSpawnDirection(SceneDirections.Right);
            playerHealth.ResetHealth();
            spellInventory.ResetSpellInventory();
        }
    }
}
