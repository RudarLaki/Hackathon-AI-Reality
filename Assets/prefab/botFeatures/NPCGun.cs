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
        Debug.Log(gameObject.name + " pucao, steta: " + equipment.weapon.damage);
        // player.TakeDamage(equipment.weapon.damage);
    }
}