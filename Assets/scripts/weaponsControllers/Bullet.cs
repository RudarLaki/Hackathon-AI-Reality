// Bullet.cs - priloži na bullet prefab
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float damage = 10f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void SetDamage(float amount)
    {
        damage = amount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pogodjen igrac, steta: " + damage);
             other.GetComponent<NpcHealthBar>().TakeDamage(damage);
       
            Destroy(gameObject);
        }
        
        if (other.CompareTag("Ground")) 
            Destroy(gameObject);
    }
}