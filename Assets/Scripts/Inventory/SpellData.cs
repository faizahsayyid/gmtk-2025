using UnityEngine;

[CreateAssetMenu(fileName = "SpellData", menuName = "Scriptable Objects/SpellData")]
public class SpellData : ScriptableObject
{
    [Header("Basic Info")]
    public string spellName;
    public string description;
    public Sprite icon;
    
    [Header("Stats")]
    public float damage;
    public float manaCost;
    public float cooldown;
    public float castTime;
    
    [Header("Effects")]
    public GameObject spellPrefab;
    public AudioClip castSound;
    public ParticleSystem castEffect;
}