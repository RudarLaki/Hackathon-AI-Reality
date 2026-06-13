// FistWeapon.cs - priloži na oba Fist GameObject-a
using UnityEngine;

public class FistController : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float punchSpeed = 3f;
    [SerializeField] private float punchDistance = 0.5f;
    [SerializeField] private bool isLeftFist = true;

    private Vector3 startLocalPosition;

    void Start()
    {
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        // Leva i desna pesnica naizmenicno udaraju
        float offset = isLeftFist ? 0f : Mathf.PI;
        float newZ = startLocalPosition.z + Mathf.Sin(Time.time * punchSpeed + offset) * punchDistance;
        transform.localPosition = new Vector3(startLocalPosition.x, startLocalPosition.y, newZ);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<NpcHealthBar>().TakeDamage(damage);
        }
    }
}