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
            FireSpellProjectile();
            Debug.Log($"Cast {currentSpell.spellName}!");
        }
        
        isCasting = false;
    }
    
    private void FireSpellProjectile()
    {
        if (currentSpell.spellPrefab != null && firePoint != null)
        {
            GameObject projectile = SpawnManager.Instantiate(
                currentSpell.spellPrefab, 
                firePoint.position, 
                firePoint.rotation,
                false
            );
            
            // Determine shoot direction based on player facing
            Vector2 shootDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            
            // Initialize projectile (works for both Bullet and StunBullet)
            var bullet = projectile.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.Initialize(shootDirection);
            }
            
            var stunBullet = projectile.GetComponent<StunBullet>();
            if (stunBullet != null)
            {
                stunBullet.Initialize(shootDirection);
            }
        }
    }
}