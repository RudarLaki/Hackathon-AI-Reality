using UnityEngine;

public class NPCGlitch : MonoBehaviour
{
    [SerializeField] private float glitchRadius = 5f;
    [SerializeField] private Transform player;
    
    private Vector3 originalPosition;
    private float glitchIntensity = 0f;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        
        // Sto bliže, to jači glitch
        glitchIntensity = 1f - Mathf.Clamp01(distance / glitchRadius);
        
        if (glitchIntensity > 0)
        {
            // Random offset pozicije
            float offsetX = Random.Range(-1f, 1f) * glitchIntensity * 0.3f;
            float offsetZ = Random.Range(-1f, 1f) * glitchIntensity * 0.3f;
            
            transform.position = originalPosition + new Vector3(offsetX, 0, offsetZ);
            
            // Random rotacija
            float rotY = Random.Range(-1f, 1f) * glitchIntensity * 30f;
            transform.rotation = Quaternion.Euler(0, rotY, 0);
            
            // Random scale
            float scale = 1f + Random.Range(-0.1f, 0.1f) * glitchIntensity;
            transform.localScale = new Vector3(scale, scale, scale);
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