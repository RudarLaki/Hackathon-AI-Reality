using UnityEngine;
using UnityEngine.InputSystem;

public class GunPickup : MonoBehaviour
{
    [SerializeField] GameObject reticle;
    [SerializeField] GameObject gunInHand; // model gun-a koji se prikaze u ruci
    [SerializeField] MonoBehaviour shootingScript; // referenca na Shooting skriptu

    [Header("Interaction")]
    [SerializeField] Transform player;
    [SerializeField] float interactRange = 2f;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        // Pre pickup-a, shooting je iskljucen
        if (shootingScript != null)
            shootingScript.enabled = false;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        bool inRange = dist <= interactRange;

        if (inRange) InteractableRegistry.nextFrameAnyInRange = true;

        if (inRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PickUp();
        }
    }

    void PickUp()
    {
        if (reticle != null)
            reticle.SetActive(true);

        if (gunInHand != null)
            gunInHand.SetActive(true);

        if (shootingScript != null)
            shootingScript.enabled = true;

        Destroy(gameObject);
    }
}