using UnityEngine;
using System.Collections;
using System;

public class SpellCaster : MonoBehaviour
{
    [Header("Spell System")]
    public SpellInventory spellInventory;
    public Transform firePoint;
    public float castDelay = 0.2f;

    [Header("Current Spell")]
    private int currentSpellIndex = 0;
    [Header("Defense Settings")]
    public Color defenseColor = Color.blue; // Defense color
    public PlayerHealth playerHealth;
    public LooperRecording looperRecording;
    public LoopTimer loopTimer;

    private SpellData currentSpell;

    // Events for UI updates
    public event Action<SpellData> OnSpellSelected;
    private Animator animator;
    private bool isCasting = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (spellInventory != null && spellInventory.GetAllSpells().Count > 0)
        {
            SelectSpell(0);
        }
    }

    private void SelectSpell(int index)
    {
        var spells = spellInventory.GetAllSpells();
        if (index >= 0 && index < spells.Count)
        {
            currentSpellIndex = index;
            currentSpell = spells[index].spellData;
            OnSpellSelected?.Invoke(currentSpell);
            Debug.Log($"Selected spell: {currentSpell.spellName}");
        }
    }


    private void SelectSpellByName(string spellName)
    {
        var spells = spellInventory.GetAllSpells();
        for (int i = 0; i < spells.Count; i++)
        {
            if (spells[i].spellData.spellName.Equals(spellName, System.StringComparison.OrdinalIgnoreCase))
            {
                SelectSpell(i);
                return;
            }
        }
    }

    public void SelectAttackSpell()
    {
        SelectSpellByName("Attack");
    }

    public void SelectStunSpell()
    {
        SelectSpellByName("Stun");
    }

    public void SelectDefenseSpell()
    {
        SelectSpellByName("Defense");
    }

    private bool CanCastCurrentSpell()
    {
        return currentSpell != null &&
               spellInventory.CanCastSpell(currentSpell) &&
               !isCasting;
    }

    public void CastCurrentSpell()
    {
        if (CanCastCurrentSpell())
        {
            StartCoroutine(CastSpellCoroutine());
        }
        else
        {
            Debug.Log($"Cannot cast {currentSpell?.spellName}: " +
                     $"Cooldown remaining: {spellInventory.GetSpellCooldownRemaining(currentSpell):F1}s");
        }
    }

    private IEnumerator CastSpellCoroutine()
    {
        isCasting = true;

        // Play cast animation
        if (animator != null)
        {
            animator.SetInteger(PlayerAnimationConstants.Accessor, PlayerAnimationConstants.Cast);
        }

        // Wait for cast delay
        yield return new WaitForSeconds(castDelay);

        // Cast the spell
        if (spellInventory.CastSpell(currentSpell))
        {
            Debug.Log($"Cast {currentSpell.spellName}!");

            if (currentSpell.spellName.Equals("Defense"))
            {
                EnableDefenseSpell();
                yield return new WaitForSeconds(currentSpell.castTime);
                DisableDefenseSpell();
                if (!loopTimer.isRunning)
                {
                    loopTimer.StartTimer();
                }
                looperRecording.RecordDefense(loopTimer.GetCurrentTimeSeconds(), currentSpell.castTime);
            }
            else
            {
                FireSpellProjectile();
            }
        }

        isCasting = false;
    }

    private void EnableDefenseSpell()
    { 
        playerHealth.DisableDamage(); // Disable damage while defense is active
        gameObject.GetComponent<SpriteRenderer>().color = defenseColor; // Change color to indicate defense
    }

    private void DisableDefenseSpell()
    { 
        playerHealth.EnableDamage(); // Re-enable damage after defense
        gameObject.GetComponent<SpriteRenderer>().color = Color.white; // Reset color after stun
    }

    private void FireSpellProjectile()
    {
        GameObject projectile = SpawnManager.InstantiateAndNotify(
            currentSpell.spellPrefab,
            firePoint.position,
            firePoint.rotation,
            transform.localScale.x <= 0
        );
    }
}