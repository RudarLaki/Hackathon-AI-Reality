using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Collider))]
public class InstantBlinkTurnBot : MonoBehaviour
{
    [Header("Blink Settings")]
    [SerializeField] float blinkDistance = 3f;
    [SerializeField] float waitTime = 0.5f;

    [Header("Bug System")]
    [Tooltip("ID bag-a koji ovaj NPC otkriva (mora se poklapati sa bugId u HologramTerminal commands).")]
    [SerializeField] string myBugID;

    [Tooltip("Komanda koja se prikazuje igracu, npr. '/blink'")]
    [SerializeField] string revealedCommandText;

    [Header("UI")]
    [SerializeField] GameObject commandRevealPanel;
    [SerializeField] TMP_Text commandRevealText;

    bool running = true;
    bool playerInRange = false;
    bool hasBeenInteracted = false;

    void Start()
    {
        StartCoroutine(BlinkLoop());

        if (commandRevealPanel != null)
            commandRevealPanel.SetActive(false);
    }

    void Update()
    {
        if (playerInRange) InteractableRegistry.nextFrameAnyInRange = true;
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    IEnumerator BlinkLoop()
    {
        while (running)
        {
            // 1. Instant teleport forward
            transform.position += transform.forward * blinkDistance;

            yield return new WaitForSeconds(waitTime);

            // 2. Instant 180 turn
            transform.Rotate(0f, 180f, 0f);

            yield return new WaitForSeconds(waitTime);
        }
    }

    void Interact()
    {
        if (!hasBeenInteracted)
        {
            hasBeenInteracted = true;
            BugProgress.Discover(myBugID);
            Debug.Log($"Discovered bug: {myBugID}");
        }

        StartCoroutine(ShowCommandPanel());
    }

    System.Collections.IEnumerator ShowCommandPanel()
    {
        if (commandRevealPanel != null)
        {
            commandRevealPanel.SetActive(true);

            if (commandRevealText != null)
                for (int i = 0; i < 50; i++)
                {
                    yield return new WaitForSecondsRealtime(0.05f);
                    commandRevealText.text += revealedCommandText;
                }
            commandRevealPanel.SetActive(false);
            commandRevealText.text = "";
        }
        Destroy(gameObject);
        
    }

    public void CloseCommandPanel()
    {
        if (commandRevealPanel != null)
            commandRevealPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}