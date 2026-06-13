using UnityEngine;

public class FistController : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float punchSpeed = 3f;
    [SerializeField] private float punchDistance = 0.5f;
    [SerializeField] private bool isLeftFist = true;

    private Vector3 startLocalPosition;
    private float punchTimer = 0f;

    void Start()
    {
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        punchTimer += Time.deltaTime;
        float offset = isLeftFist ? 0f : Mathf.PI;
        float newZ = startLocalPosition.z + Mathf.Sin(punchTimer * punchSpeed + offset) * punchDistance;
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