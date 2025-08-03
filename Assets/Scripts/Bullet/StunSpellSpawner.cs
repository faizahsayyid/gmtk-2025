using UnityEngine;

public class StunSpellSpawner : MonoBehaviour
{
    public GameObject stunSpellPrefab;
    public int spawnCount = 3;
    public Vector2 minBounds = new Vector2(-8, -4);
    public Vector2 maxBounds = new Vector2(8, 4);

    void Start()
    {
        if (stunSpellPrefab == null)
        {
            Debug.LogError("stunSpellPrefab is not assigned in the Inspector!");
            return;
        }

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
}