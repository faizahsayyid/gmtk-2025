using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "Scriptable Objects/PlayerHealth")]
public class PlayerHealth : ScriptableObject
{
    public Action OnPlayerDeath;
    public Action OnShowBlackScreen;
    public float maxHealthPoints = 5f;
    private float healthPoints;

    private bool canTakeDamage = true;

    public void ResetHealth()
    {
        canTakeDamage = true;
        healthPoints = maxHealthPoints;
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage) return;
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            healthPoints = 0;
            OnPlayerDeath?.Invoke();
        }
        Debug.Log($"Player Health: {healthPoints}/{maxHealthPoints}");
    }

    public void ShowBlackScreen()
    {
        Debug.Log("ShowBlackScreen");
        OnShowBlackScreen?.Invoke();
    }

    public void EnableDamage()
    {
        canTakeDamage = true;
    }

    public void DisableDamage()
    {
        canTakeDamage = false;
    }

    public float GetHealthPercentage()
    {
        return healthPoints / maxHealthPoints;
    }
}
