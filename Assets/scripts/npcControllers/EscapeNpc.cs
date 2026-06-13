using UnityEngine;

public class NPCFlee : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float fleeRadius = 5f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float returnSpeed = 2f;
    [SerializeField] private float stoppingDistance = 0.5f;

    private Vector3 startPosition;
    private bool isFleeing = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < fleeRadius)
        {
            isFleeing = true;
        }
        else
        {
            isFleeing = false;
        }

        if (isFleeing)
        {
            Flee();
        }
        else
        {
            ReturnToStart();
        }
    }

    void Flee()
    {
        float speed = moveSpeed * 1.5f;

        Vector3 direction = (transform.position - player.position);
        direction.y = 0;
        direction = direction.normalized;

        transform.position += direction * speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void ReturnToStart()
    {
        float distanceToStart = Vector3.Distance(transform.position, startPosition);

        if (distanceToStart < stoppingDistance) return;

        Vector3 direction = (startPosition - transform.position);
        direction.y = 0;
        direction = direction.normalized;

        transform.position += direction * returnSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, fleeRadius);
    }
}