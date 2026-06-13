using UnityEngine;

public class NPCGlitch : MonoBehaviour
{
    [SerializeField] private float glitchRadius = 5f;
    [SerializeField] private Transform player;
    [SerializeField] private float glitchUpdateInterval = 0.05f;

    private Vector3 originalPosition;
    private float glitchIntensity = 0f;
    private float glitchTimer = 0f;
    private Vector3 currentOffset;
    private float currentRotY;
    private float currentScale;

    void Start()
    {
        originalPosition = transform.position;
        currentScale = 1f;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        glitchIntensity = 1f - Mathf.Clamp01(distance / glitchRadius);

        if (glitchIntensity > 0)
        {
            // Update glitch vrednosti na svakih glitchUpdateInterval sekundi
            glitchTimer -= Time.deltaTime;
            if (glitchTimer <= 0f)
            {
                glitchTimer = glitchUpdateInterval;
                currentOffset = new Vector3(
                    Random.Range(-1f, 1f) * glitchIntensity * 0.3f,
                    0,
                    Random.Range(-1f, 1f) * glitchIntensity * 0.3f
                );
                currentRotY = Random.Range(-1f, 1f) * glitchIntensity * 30f;
                currentScale = 1f + Random.Range(-0.1f, 0.1f) * glitchIntensity;
            }

            transform.position = originalPosition + currentOffset;
            transform.rotation = Quaternion.Euler(0, currentRotY, 0);
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
        else
        {
            transform.position = originalPosition;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, glitchRadius);
    }
}