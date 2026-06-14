using UnityEngine;

public class NPCChase : MonoBehaviour
{
    [SerializeField] private Transform target; // drag cylinder ovde u Inspector
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stoppingDistance = 1f;

    void Update()
    {
        if (target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget > stoppingDistance)
        {
            Vector3 direction = (target.position - transform.position);
            direction.y = 0;
            direction = direction.normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}