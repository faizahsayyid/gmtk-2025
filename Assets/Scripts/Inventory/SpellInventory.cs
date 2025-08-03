using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SpellSlot
{
    public SpellData spellData;
    public int quantity;
    public float lastUsedTime;

    public SpellSlot(SpellData spell, int qty = 1)
    {
        spellData = spell;
        quantity = qty;
        lastUsedTime = 0f;
    }

    public bool CanCast()
    {
        // return quantity > 0 && Time.time >= lastUsedTime + spellData.cooldown;
        return quantity > 0;
    }

    public void UseSpell()
    {
        if (CanCast())
        {
            quantity--;
            lastUsedTime = Time.time;
        }
    }
    
    public void AddSpell(int qty)
    {
        quantity += qty;
    }
}

[CreateAssetMenu(fileName = "SpellInventory", menuName = "Scriptable Objects/SpellInventory")]
public class SpellInventory : ScriptableObject
{
    [Header("Inventory Settings")]
    public int maxSlots = 10;
    
    [SerializeField]
    private List<SpellSlot> spellSlots = new List<SpellSlot>();
    
    public event Action<SpellData> OnSpellUsed;
    
    public List<SpellSlot> GetAllSpells()
    {
        return new List<SpellSlot>(spellSlots);
    }
    
    public bool HasSpell(SpellData spell)
    {
        return spellSlots.Exists(slot => slot.spellData == spell && slot.quantity > 0);
    }
    
    public bool CanCastSpell(SpellData spell)
    {
        SpellSlot slot = spellSlots.Find(s => s.spellData == spell);
        return slot != null && slot.CanCast();
    }
    
    public bool CastSpell(SpellData spell)
    {
        SpellSlot slot = spellSlots.Find(s => s.spellData == spell);
        
        if (slot != null && slot.CanCast())
        {
            slot.UseSpell();
            OnSpellUsed?.Invoke(spell);
            return true;
        }
        
        return false;
    }
    public void IncreaseSpellQuantity(int index, int amount)
    {
        SpellSlot slot = index == 0 ? spellSlots.Find(s => s.spellData.spellName == "Attack") : spellSlots.Find(s => s.spellData.spellName == "Stun");

        Debug.Log(slot.spellData.spellName);

        if (slot != null)
        {
            slot.AddSpell(amount);
        }
       
    }
    
    public int GetSpellQuantity(SpellData spell)
    {
        SpellSlot slot = spellSlots.Find(s => s.spellData == spell);
        return slot?.quantity ?? 0;
    }
    
    public float GetSpellCooldownRemaining(SpellData spell)
    {
        SpellSlot slot = spellSlots.Find(s => s.spellData == spell);
        
        if (slot != null)
        {
            float timeElapsed = Time.time - slot.lastUsedTime;
            return Mathf.Max(0f, spell.cooldown - timeElapsed);
        }
        
        return 0f;
    }
}