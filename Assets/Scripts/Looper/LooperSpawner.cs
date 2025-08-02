using UnityEngine;
using System.Collections.Generic;

// TODO: Implement
public class LooperSpawner : MonoBehaviour
{
    public float screenEdgeOffset = 0.1f; // Offset from the screen edges to spawn objects
    public LoopTimer loopTimer;
    public LooperRecording looperRecording;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        // Subscribe to event
        loopTimer.onTimerTick += HandleTimerTick;
    }

    void OnDisable()
    {
        // Unsubscribe from event
        loopTimer.onTimerTick -= HandleTimerTick;
    }

    void HandleTimerTick(int seconds)
    {

        if (!loopTimer.isFirstCycleComplete) return;
        
        List<LoopRecordingLog> logs = looperRecording.GetLogsAtTime(seconds);

        for (int i = 0; i < logs.Count; i++)
        {
            LoopRecordingLog log = logs[i];
            Vector3 leftEdgePosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector3 rightEdgePosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));
            float spawnX = log.flip ? rightEdgePosition.x - screenEdgeOffset : leftEdgePosition.x + screenEdgeOffset;
            Vector3 newPosition = new Vector3(spawnX, log.position.y, log.position.z);
            SpawnManager.Instantiate(log.gameObject, newPosition, log.rotation, log.flip);
        }
    }
}
