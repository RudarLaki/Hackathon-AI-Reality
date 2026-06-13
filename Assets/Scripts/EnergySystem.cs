using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public float maxEnergy = 100f;
    public float currentEnergy;
   

    public float regenRate = 10f;

    void Start()
    {
        currentEnergy = maxEnergy;
    }

    void Update()
    {
        Regenerate();
        
    }

    void Regenerate()
    {
        currentEnergy += regenRate * Time.deltaTime;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
    }

    public bool UseEnergy(float amount)
    {
        if (currentEnergy < amount)
            return false;

        currentEnergy -= amount;
        return true;
    }

    public float GetEnergy()
    {
        return currentEnergy;
    }
}