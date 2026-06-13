// SwordWeapon.cs - priloži na Sword GameObject
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float swingSpeed = 2f;
    [SerializeField] private float swingHeight = 1.5f;

    private Vector3 startLocalPosition;

    void Start()
    {
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        // Gore dole kretanje
        float newY = startLocalPosition.y + Mathf.Sin(Time.time * swingSpeed) * swingHeight;
        transform.localPosition = new Vector3(startLocalPosition.x, newY, startLocalPosition.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<NpcHealthBar>().TakeDamage(damage);
        }
    }
}