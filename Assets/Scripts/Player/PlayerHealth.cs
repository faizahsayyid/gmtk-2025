using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "Scriptable Objects/PlayerHealth")]
public class PlayerHealth : ScriptableObject
{
    public Action OnPlayerDeath;
    public int maxHealthPoints = 5;
    private int healthPoints;

    public void ResetHealth()
    {
        healthPoints = maxHealthPoints;
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            healthPoints = 0;
            OnPlayerDeath?.Invoke();
        }
    }
}
