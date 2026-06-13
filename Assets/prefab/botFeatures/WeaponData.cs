using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "NPC/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float fireRate;
    public float range;
    public GameObject weaponModel; // 3D model kad dizajner da
    public GameObject bulletPrefab; 

}