using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Collider))]
public class CircleRunningBot : MonoBehaviour
{
    [SerializeField] float radius = 5f;
    [SerializeField] float speed = 2f;

    [Header("Bug System")]
    [Tooltip("ID bag-a koji ovaj NPC otkriva (mora se poklapati sa bugId u HologramTerminal commands).")]
    [SerializeField] string myBugID;

    [Tooltip("Komanda koja se prikazuje igracu, npr. '/lowg'")]
    [SerializeField] string revealedCommandText;

    [Header("UI")]
    [SerializeField] GameObject commandRevealPanel;
    [SerializeField] TMP_Text commandRevealText;

    Vector3 center;
    float angle;

    bool playerInRange = false;
    bool hasBeenInteracted = false;

    void Start()
    {
        center = transform.position;

        if (commandRevealPanel != null)
            commandRevealPanel.SetActive(false);
    }

    void Update()
    {
        angle += speed * Time.deltaTime;

        // Position on circle
        Vector3 newPos = center + new Vector3(
            Mathf.Cos(angle) * radius,
            0,
            Mathf.Sin(angle) * radius
        );

        transform.position = newPos;

        // Tangent direction
        Vector3 direction = new Vector3(
            -Mathf.Sin(angle),
            0f,
            Mathf.Cos(angle)
        );

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        if (playerInRange) InteractableRegistry.nextFrameAnyInRange = true;
        // Interaction
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
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