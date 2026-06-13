using UnityEngine;

public class NPCGun : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    private NPCEquipment equipment;
    private float fireCounter;

    void Start()
    {
        equipment = GetComponent<NPCEquipment>();
    }

    void Update()
    {
        if (player == null || equipment == null || equipment.weapon == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > equipment.weapon.range) return;

        fireCounter -= Time.deltaTime;
        if (fireCounter <= 0f)
        {
            Shoot();
            fireCounter = equipment.weapon.fireRate;
        }
    }

void Shoot()
{
    float distance = Vector3.Distance(transform.position, player.position);
    if (distance > equipment.weapon.range) return;

    if (equipment.weapon.bulletPrefab != null)
    {
        // Spawna metak i rotira ga prema igracu
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject bullet = Instantiate(equipment.weapon.bulletPrefab, transform.position, rotation);
        bullet.GetComponent<Bullet>().SetDamage(equipment.weapon.damage);
    }
}
}