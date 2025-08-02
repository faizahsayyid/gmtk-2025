using UnityEngine;

public static class SpawnManager
{
    public static GameObject InstantiateAndNotify(GameObject prefab, Vector3 position, Quaternion rotation, bool flip)
    {
        GameObject instantiatedGameObject = Instantiate(prefab, position, rotation, flip);
        SpawnEvents.OnObjectInstantiated?.Invoke(prefab, position, rotation, flip);
        return instantiatedGameObject;
    }

    public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, bool flip)
    {
        GameObject instantiatedGameObject = Object.Instantiate(prefab, position, rotation);

        if (instantiatedGameObject != null && flip)
        {
            instantiatedGameObject.transform.localScale = new Vector3(
                instantiatedGameObject.transform.localScale.x * -1,
                instantiatedGameObject.transform.localScale.y,
                instantiatedGameObject.transform.localScale.z
            );
        }

        return instantiatedGameObject;
    }

    public static void Destroy(GameObject gameObject)
    {
        SpawnEvents.OnObjectDestroyed?.Invoke(gameObject);
        Object.Destroy(gameObject);
    }
}
