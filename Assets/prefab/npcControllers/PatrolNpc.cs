using UnityEngine;

public class CylinderPatrol : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed = 5f;
    public float reachDistance = 0.2f;

    private int currentPoint = 0;

    void Update()
    {
        if (points.Length == 0) return;

        Vector3 targetPosition = new Vector3(
            points[currentPoint].position.x,
            transform.position.y, // zadrži trenutnu visinu
            points[currentPoint].position.z
        );

        Vector3 direction = (targetPosition - transform.position).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;

        float distance =
            Vector3.Distance(transform.position, targetPosition);

        if (distance <= reachDistance)
        {
            currentPoint++;

            if (currentPoint >= points.Length)
                currentPoint = 0;
        }
    }
}