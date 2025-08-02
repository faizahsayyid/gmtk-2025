using UnityEngine;
using System;


[CreateAssetMenu(fileName = "LoopTimer", menuName = "Scriptable Objects/LoopTimer")]
public class LoopTimer : ScriptableObject
{
    public float duration = 60f; // Duration of the timer in seconds
    private float timeRemaining;
    public bool isRunning;
    public event Action OnTimerStopped;
    public event Action<int> onTimerTick;

    private int secondsOnLastTick;

    public void StartTimer()
    {
        timeRemaining = duration;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
        OnTimerStopped?.Invoke();
    }

    public void UpdateTimer(float deltaTime)
    {
        if (!isRunning) return;

        timeRemaining -= deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = duration; // Reset the timer to loop
        }
        // Trigger the tick event if its at the end of a second
        if (secondsOnLastTick != GetCurrentTimeSeconds())
        {
            secondsOnLastTick = GetCurrentTimeSeconds();
            // Invoke the timer tick event
            onTimerTick?.Invoke(secondsOnLastTick);
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
