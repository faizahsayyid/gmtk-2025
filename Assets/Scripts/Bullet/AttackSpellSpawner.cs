using UnityEngine;

public class AttackSpellSpawner : MonoBehaviour
{
    public GameObject attackSpellPrefab;
    public int spawnCount = 3;
    public Vector2 minBounds = new Vector2(-8, -4);
    public Vector2 maxBounds = new Vector2(8, 4);

    void Start()
    {
        if (attackSpellPrefab == null)
        {
            Debug.LogError("attackSpellPrefab is not assigned in the Inspector!");
            return;
        }

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
}