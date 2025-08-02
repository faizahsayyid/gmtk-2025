using UnityEngine;

public static class SpawnManager
{
    public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject instantiatedGameObject = Object.Instantiate(prefab, position, rotation);
        SpawnEvents.OnObjectInstantiated?.Invoke(instantiatedGameObject);
        return instantiatedGameObject;
    }

    public static void Destroy(GameObject gameObject)
    {
        SpawnEvents.OnObjectDestroyed?.Invoke(gameObject);
        Object.Destroy(gameObject);
    }
}
