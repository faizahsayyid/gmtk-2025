using UnityEngine;

public class GameLoad : MonoBehaviour
{
    public PlayerSpawnDirection playerSpawnDirection;
    private static GameLoad Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerSpawnDirection.SetSpawnDirection(SceneDirections.Right);
        }
    }
}
