using UnityEngine;
using System;

public static class SpawnEvents
{
    public static Action<GameObject, Vector3, Quaternion, bool> OnObjectInstantiated;
    public static Action<GameObject> OnObjectDestroyed;
}
