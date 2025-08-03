using UnityEngine;

public class GameLoad : MonoBehaviour
{
    public PlayerSpawnDirection playerSpawnDirection;
    public PlayerHealth playerHealth;
    private static GameLoad Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerSpawnDirection.SetSpawnDirection(SceneDirections.Right);
            playerHealth.ResetHealth();
        }
    }
}
