using UnityEngine;
using System;

public static class SpawnEvents
{
    public static Action<GameObject> OnObjectInstantiated;
    public static Action<GameObject> OnObjectDestroyed;
}
