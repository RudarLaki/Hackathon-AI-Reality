using UnityEngine;

public class FistCollider : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    private bool canDamage = false;

    public void EnableDamage() => canDamage = true;
    public void DisableDamage() => canDamage = false;

    void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return;

        if (other.CompareTag("Player"))
        {
            NpcHealthBar npcHealthBar = other.GetComponent<NpcHealthBar>();
            if (npcHealthBar != null)
                npcHealthBar.TakeDamage(damage);
        }
    }
}