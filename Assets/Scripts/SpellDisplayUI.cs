using UnityEngine;
using TMPro;

public class SpellDisplayUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI spellNameText;
    public TextMeshProUGUI spellQuantityText;
    
    [Header("Spell System")]
    public SpellInventory spellInventory;
    
    [Header("Display Settings")]
    public string noSpellText = "No Spell";
    public string quantityFormat = "{0}";
    
    private SpellData currentDisplayedSpell;
    private SpellCaster spellCaster;

    private void Start()
    {
        SetSpellCaster();

        // Initial update
        UpdateDisplay();
    }

    private void SetSpellCaster()
    {
        // Find Player
        GameObject player = GameObject.FindWithTag(Tags.Player);
        if (player != null)
        {
            Debug.Log("Player found for SpellDisplayUI.");
            spellCaster = player.GetComponent<SpellCaster>();
        }
        
        // Subscribe to events
        if (spellInventory != null)
        {
            spellInventory.OnSpellUsed += OnSpellUsed;
        }

        if (spellCaster != null)
        {
            var spells = spellInventory.GetAllSpells();
            currentDisplayedSpell = spells[0].spellData;
            spellCaster.OnSpellSelected += OnSpellSelected;
        }
    }
    
    private void Update()
    {
        if (spellCaster == null)
        {
            SetSpellCaster();
        }
        // Update quantity display regularly (in case it changed)
        UpdateQuantityDisplay();
        UpdateSpellNameDisplay();
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