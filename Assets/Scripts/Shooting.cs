using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] float range = 100f;
    [SerializeField] LayerMask hitMask; // optional - sta sve moze biti pogodjeno

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        
        if (Physics.Raycast(ray, out RaycastHit hit, range, hitMask))
        {
            Debug.Log($"Hit: {hit.collider.name}");

            // Primer: ako pogodjeni objekat ima neki skript, pozovi nesto na njemu
            // hit.collider.GetComponent<SomeScript>()?.TakeDamage(10);
        }
    }
}