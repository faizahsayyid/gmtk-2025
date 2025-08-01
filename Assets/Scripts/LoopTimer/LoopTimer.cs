using UnityEngine;
using System;


[CreateAssetMenu(fileName = "LoopTimer", menuName = "Scriptable Objects/LoopTimer")]
public class LoopTimer : ScriptableObject
{
    public float duration = 60f; // Duration of the timer in seconds
    private float timeRemaining;
    public bool isRunning;

    public void StartTimer()
    {
        timeRemaining = duration;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void UpdateTimer(float deltaTime)
    {
        if (!isRunning) return;

        timeRemaining -= deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = duration; // Reset the timer to loop
        }
    }

    public float GetCurrentTime()
    {
        return duration - timeRemaining;
    }
}
