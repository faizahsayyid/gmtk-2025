using UnityEngine;

public class LooperRecorder : MonoBehaviour
{
    public int looperLayer;
    public LoopTimer loopTimer;
    public LooperRecording looperRecording;

    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
    }
    void OnEnable()
    {
        // Enable the input actions
        inputActions.Enable();
        // Subscribe to event
        SpawnEvents.OnObjectInstantiated += HandleSpawn;
        inputActions.Player.StopLooper.performed += ctx => HandleStopLooper();;
    }

    void OnDisable()
    {
        // Disable the input actions
        inputActions.Disable();
        // Unsubscribe from event
        SpawnEvents.OnObjectInstantiated -= HandleSpawn;
        // Stop the looper
        HandleStopLooper();
    }

    private void Start()
    {
        HandleStopLooper();
    }

    void HandleSpawn(GameObject spawnedObject, Vector3 position, Quaternion rotation, bool flip)
    {
        if (!loopTimer.isRunning)
        {
            loopTimer.StartTimer();
        }
        if (loopTimer.isRunning && looperLayer == spawnedObject.layer)
        {
            int timeStamp = loopTimer.GetCurrentTimeSeconds();

            looperRecording.Record(timeStamp, spawnedObject, position, rotation, flip);
        }
    }
    
    void HandleStopLooper()
    {
        if (loopTimer.isRunning)
        {
            loopTimer.StopTimer();
        }
        // Reset the recording
        looperRecording.ResetRecording();
    }
}
