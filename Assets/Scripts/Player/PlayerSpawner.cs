using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public float screenEdgeXOffset = 0.1f;
    public float screenEdgeYOffset = 0.1f;
    public PlayerSpawnDirection playerSpawnDirection;
    public GameObject playerPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera camera = Camera.main;
        Vector3 spawnPosition;
        bool flip = false;

        switch (playerSpawnDirection.GetSpawnDirection())
        {
            case SceneDirections.Up:
                Vector3 bottomEdge = camera.ViewportToWorldPoint(new Vector3(0.5f, 0, camera.nearClipPlane));
                spawnPosition = new Vector3(
                    transform.position.x,
                    bottomEdge.y - screenEdgeXOffset,
                    bottomEdge.z
                );
                break;
            case SceneDirections.Down:
                Vector3 topEdge = camera.ViewportToWorldPoint(new Vector3(0.5f, 1, camera.nearClipPlane));
                spawnPosition = new Vector3(
                    transform.position.x,
                    topEdge.y + screenEdgeXOffset,
                    topEdge.z
                );
                break;
            case SceneDirections.Left:
                Vector3 rightEdge = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane));
                spawnPosition = new Vector3(
                    rightEdge.x - screenEdgeXOffset,
                    transform.position.y + screenEdgeYOffset,
                    transform.position.z
                );
                flip = true;
                break;
            case SceneDirections.Right:
            default:
                Vector3 leftEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
                spawnPosition = new Vector3(
                    leftEdge.x + screenEdgeXOffset,
                    transform.position.y + screenEdgeYOffset,
                    leftEdge.z
                );
                break;
        }

        // Instantiate the player at the calculated spawn position
        SpawnManager.Instantiate(playerPrefab, spawnPosition, Quaternion.identity, flip);
    }
}
