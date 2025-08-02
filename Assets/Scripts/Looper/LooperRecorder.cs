using UnityEngine;

public class LooperRecorder : MonoBehaviour
{
    public LayerMask looperLayer;
    public LoopTimer loopTimer;
    public LooperRecording looperRecording;

    void OnEnable()
    {
        // Subscribe to event
        SpawnEvents.OnObjectInstantiated += HandleSpawn;
    }

    void OnDisable()
    {
        // Unsubscribe from event
        SpawnEvents.OnObjectInstantiated -= HandleSpawn;
    }

    void HandleSpawn(GameObject spawnedObject)
    {
        if (loopTimer.isRunning && spawnedObject.layer == looperLayer)
        {
            float currentTime = loopTimer.GetCurrentTime();
            looperRecording.Record(currentTime, spawnedObject);
        }
    }
}
