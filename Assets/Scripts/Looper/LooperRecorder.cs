using UnityEngine;

public class LooperRecorder : MonoBehaviour
{
    public int looperLayer;
    public LoopTimer loopTimer;
    public LooperRecording looperRecording;

    void OnEnable()
    {
        // Subscribe to event
        SpawnEvents.OnObjectInstantiated += HandleSpawn;
        loopTimer.OnTimerStopped += HandleTimerStopped;
    }

    void OnDisable()
    {
        // Unsubscribe from event
        SpawnEvents.OnObjectInstantiated -= HandleSpawn;
        loopTimer.OnTimerStopped -= HandleTimerStopped;
        // Reset the recording
        looperRecording.ResetRecording();
    }

    void HandleSpawn(GameObject spawnedObject, Vector3 position, Quaternion rotation, bool flip)
    {
        if (loopTimer.isRunning && looperLayer == spawnedObject.layer)
        {
            int currentTime = loopTimer.GetCurrentTimeSeconds();
            looperRecording.Record(currentTime, spawnedObject, position, rotation, flip);
        }
    }
    
    void HandleTimerStopped()
    {
        looperRecording.ResetRecording();
    }
}
