using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private float patrolDistance = 10f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float idleTime = 2f;
    [SerializeField] private float stoppingDistance = 0.5f;
    
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float idleCounter;
    private bool isMoving = false;
    private Animator animator;

    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        idleCounter = idleTime;
        isMoving = false;
        SetNewTarget();
    }

    void Update()
    {
        if (isMoving)
            Patrol();
        else
            Idle();
        
        if (animator != null)
        {
            float speed = isMoving ? 1f : 0f;
            animator.SetFloat("Speed", speed);
        }
    }

    void Idle()
    {
        idleCounter -= Time.deltaTime;
        if (idleCounter <= 0)
        {
            SetNewTarget();
            isMoving = true;
        }
    }

    void Patrol()
    {
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            Debug.Log("Distance: " + distanceToTarget);

        
        if (distanceToTarget < stoppingDistance)
        {
            isMoving = false;
            idleCounter = idleTime;
            return;
        }

        Vector3 directionToTarget = targetPosition - transform.position;
        directionToTarget.y = 0;
        Vector3 direction = directionToTarget.normalized;
        
        transform.position += direction * moveSpeed * Time.deltaTime;
        
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    void SetNewTarget()
    {
        Vector3 randomDir = Random.insideUnitSphere * patrolDistance;
        randomDir.y = 0;
        targetPosition = startPosition + randomDir;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, patrolDistance);
    }
}