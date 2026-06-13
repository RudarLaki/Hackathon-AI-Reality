using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float swingSpeed = 2f;
    [SerializeField] private float swingHeight = 1.5f;

    private Vector3 startLocalPosition;
    private float swingTimer = 0f;

    void Start()
    {
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        swingTimer += Time.deltaTime;
        float angle = Mathf.Sin(swingTimer * swingSpeed) * swingHeight * 45f;
        transform.localRotation = Quaternion.Euler(angle, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<NpcHealthBar>().TakeDamage(damage);
        }
    }
}