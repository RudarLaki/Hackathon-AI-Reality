// ArmorData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmor", menuName = "NPC/Armor")]
public class ArmorData : ScriptableObject
{
    public string armorName;
    public float damageReduction; // 0.0 - 1.0 (npr 0.3 = 30% manje stete)
    public GameObject armorModel;
}