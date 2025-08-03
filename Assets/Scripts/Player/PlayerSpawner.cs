using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public float screenEdgeXOffset = 0.1f;
    public float screenEdgeYOffset = 0.1f;
    public Transform leftEnterSpawnPoint;
    public Transform rightEnterSpawnPoint;
    public PlayerSpawnDirection playerSpawnDirection;
    public GameObject playerPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 spawnPosition;
        bool flip = false;

        switch (playerSpawnDirection.GetSpawnDirection())
        {
            case SceneDirections.Left:
                spawnPosition = rightEnterSpawnPoint.position;
                flip = true;
                break;
            case SceneDirections.Right:
            default:
                spawnPosition = leftEnterSpawnPoint.position;
                break;
        }

        // Instantiate the player at the calculated spawn position
        SpawnManager.Instantiate(playerPrefab, spawnPosition, Quaternion.identity, flip);
    }
}
