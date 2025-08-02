using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LoopTimer", menuName = "Scriptable Objects/LoopTimer")]
public class LoopTimer : ScriptableObject
{
    public float duration = 60f; // Duration of the timer in seconds
    private float timeRemaining = 0f;
    public bool isRunning = false;
    public event Action OnTimerStopped;
    public event Action OnTimerStarted;
    public event Action<int> onTimerTick;
    public bool isFirstCycleComplete = false;

    private int secondsOnLastTick;
    public bool debug = false;

    public void StartTimer()
    {
        isFirstCycleComplete = false; // Reset the first cycle flag
        timeRemaining = duration;
        isRunning = true;
        OnTimerStarted?.Invoke();
        if (debug)
        {
            Debug.Log("LoopTimer started");
        }
    }

    public void StopTimer()
    {
        isRunning = false;
        OnTimerStopped?.Invoke();
        if (debug)
        {
            Debug.Log("LoopTimer stopped");
        }
    }

    public void UpdateTimer(float deltaTime)
    {
        if (!isRunning) return;

        timeRemaining -= deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = duration; // Reset the timer to loop
            if (!isFirstCycleComplete)
            {
                if (debug)
                {
                    Debug.Log("LoopTimer first cycle complete");
                }
                isFirstCycleComplete = true;
            }
        }
        // Trigger the tick event if its at the end of a second
        if (secondsOnLastTick != GetCurrentTimeSeconds())
        {
            secondsOnLastTick = GetCurrentTimeSeconds();
            // Invoke the timer tick event
            onTimerTick?.Invoke(secondsOnLastTick);

            if (debug)
            {
                Debug.Log($"LoopTimer: {GetCurrentTimeSeconds()} seconds remaining.");
            }
        }
    }

    public float GetCurrentTime()
    {
        return duration - timeRemaining;
    }

    public int GetCurrentTimeSeconds()
    {
        return (int)Mathf.Floor(GetCurrentTime());
    }
}
