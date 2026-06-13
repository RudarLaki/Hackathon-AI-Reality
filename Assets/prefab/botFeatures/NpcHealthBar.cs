using UnityEngine;
using UnityEngine.UI;

public class NpcHealthBar : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Canvas healthCanvas;
    
    private float currentHealth;
    private Camera mainCamera;

    void Start()
    {
        currentHealth = maxHealth;
        mainCamera = Camera.main;
        
        if (healthSlider != null)
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
    }

    void Update()
    {
        // Health bar uvek gleda prema kameri
        if (healthCanvas != null)
            healthCanvas.transform.LookAt(mainCamera.transform);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        if (healthSlider != null)
            healthSlider.value = currentHealth;
        
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }
}