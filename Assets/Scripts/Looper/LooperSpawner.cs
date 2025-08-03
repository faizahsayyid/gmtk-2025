using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

// TODO: Implement
public class LooperSpawner : MonoBehaviour
{
    public float screenEdgeOffset = 0.1f; // Offset from the screen edges to spawn objects
    public LoopTimer loopTimer;
    public LooperRecording looperRecording;
    public PlayerHealth playerHealth;
    public Color defenseColor = Color.blue; // Color to indicate defense state
    public SpriteRenderer playerSpriteRenderer;

    private bool isDefenseActive = false;

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

        LoopDefenseLog defenseLog = looperRecording.DefenseLogAtTime(seconds);
        if (defenseLog != null && !isDefenseActive)
        { 
            StartCoroutine(HandleDefenseReplay(defenseLog));
        }
    }

    IEnumerator HandleDefenseReplay(LoopDefenseLog defenseLog)
    {
        Debug.Log("Handling defense replay");
        isDefenseActive = true;
        playerHealth.DisableDamage(); // Disable damage while defense is active
        playerSpriteRenderer.color = defenseColor; // Change color to indicate defense
        yield return new WaitForSeconds(defenseLog.castTime);
        playerHealth.EnableDamage(); // Re-enable damage after defense
        playerSpriteRenderer.color = Color.white; // Reset color after stun
        isDefenseActive = false;
    }
}
