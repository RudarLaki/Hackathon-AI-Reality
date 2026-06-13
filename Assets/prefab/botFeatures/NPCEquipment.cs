// NPCEquipment.cs
using UnityEngine;

public class NPCEquipment : MonoBehaviour
{
    [SerializeField] public WeaponData weapon;
    [SerializeField] public ArmorData armor;
    
    [SerializeField] private Transform weaponSlot; // child GameObject na ruci NPC-a
    [SerializeField] private Transform armorSlot;  // child GameObject na telu NPC-a
    
    private GameObject currentWeaponModel;
    private GameObject currentArmorModel;

    void Start()
    {
        EquipWeapon(weapon);
        EquipArmor(armor);
    }

    public void EquipWeapon(WeaponData newWeapon)
    {
        if (newWeapon == null) return;
        weapon = newWeapon;

        // Obriši stari model
        if (currentWeaponModel != null)
            Destroy(currentWeaponModel);

        // Spawn novi model kad dizajner da
        if (newWeapon.weaponModel != null && weaponSlot != null)
            currentWeaponModel = Instantiate(newWeapon.weaponModel, weaponSlot);
    }

    public void EquipArmor(ArmorData newArmor)
    {
        if (newArmor == null) return;
        armor = newArmor;

        if (currentArmorModel != null)
            Destroy(currentArmorModel);

        if (newArmor.armorModel != null && armorSlot != null)
            currentArmorModel = Instantiate(newArmor.armorModel, armorSlot);
    }
}