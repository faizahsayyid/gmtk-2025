using UnityEngine;
using TMPro;

public class SpellDisplayUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshPro spellNameText;
    public TextMeshPro spellQuantityText;
    
    [Header("Spell System")]
    public SpellCaster spellCaster;
    public SpellInventory spellInventory;
    
    [Header("Display Settings")]
    public string noSpellText = "No Spell";
    public string quantityFormat = "{0}";
    
    private SpellData currentDisplayedSpell;
    
    private void Start()
    {
        // Subscribe to events
        if (spellInventory != null)
        {
            spellInventory.OnSpellUsed += OnSpellUsed;
        }
        
        if (spellCaster != null)
        {
            spellCaster.OnSpellSelected += OnSpellSelected;
        }
        
        // Initial update
        UpdateDisplay();
    }
    
    private void Update()
    {
        // Update quantity display regularly (in case it changed)
        UpdateQuantityDisplay();
    }
    
    private void UpdateDisplay()
    {
        UpdateSpellNameDisplay();
        UpdateQuantityDisplay();
    }
    
    private void UpdateSpellNameDisplay()
    {
        if (spellNameText != null)
        {
            if (currentDisplayedSpell != null)
            {
                spellNameText.text = currentDisplayedSpell.spellName + ":";
            }
            else
            {
                spellNameText.text = noSpellText;
            }
        }
    }
    
    private void UpdateQuantityDisplay()
    {
        if (spellQuantityText != null && spellInventory != null)
        {
            if (currentDisplayedSpell != null)
            {
                int quantity = spellInventory.GetSpellQuantity(currentDisplayedSpell);
                spellQuantityText.text = string.Format(quantityFormat, quantity);
                
                // Optional: Change color based on quantity
                if (quantity <= 0)
                {
                    spellQuantityText.color = Color.red;
                }
                else
                {
                    spellQuantityText.color = Color.white;
                }
            }
            else
            {
                spellQuantityText.text = "";
            }
        }
    }
    
    private void OnSpellSelected(SpellData selectedSpell)
    {
        currentDisplayedSpell = selectedSpell;
        UpdateDisplay();
    }
    
    private void OnSpellUsed(SpellData usedSpell)
    {
        // Update display when a spell is used
        if (usedSpell == currentDisplayedSpell)
        {
            UpdateQuantityDisplay();
        }
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from events
        if (spellInventory != null)
        {
            spellInventory.OnSpellUsed -= OnSpellUsed;
        }
        
        if (spellCaster != null)
        {
            spellCaster.OnSpellSelected -= OnSpellSelected;
        }
    }
}