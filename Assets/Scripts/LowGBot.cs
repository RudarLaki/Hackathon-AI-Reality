using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class LowGravityJumpBot : MonoBehaviour
{
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float lowGravity = 2f; // smaller = floatier
    [SerializeField] float jumpInterval = 1.5f;

    [Header("Bug System")]
    [SerializeField] string myBugID;
    [SerializeField] string revealedCommandText;

    [Header("UI")]
    [SerializeField] GameObject commandRevealPanel;
    [SerializeField] TMP_Text commandRevealText;

    [Header("Interaction")]
    [SerializeField] Transform player;
    [SerializeField] float interactRange = 3f;

    private Rigidbody rb;
    private float timer;
    bool hasBeenInteracted = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Start()
    {
        if (commandRevealPanel != null)
            commandRevealPanel.SetActive(false);

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        bool inRange = dist <= interactRange;
        if (inRange) InteractableRegistry.nextFrameAnyInRange = true;

        if (inRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        // custom "low gravity"
        rb.AddForce(Vector3.down * lowGravity, ForceMode.Acceleration);

        // continuous jump timer
        timer += Time.fixedDeltaTime;

        if (IsGrounded() && timer >= jumpInterval)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            timer = 0f;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void Interact()
    {
        if (!hasBeenInteracted)
        {
            hasBeenInteracted = true;
            BugProgress.Discover(myBugID);
            Debug.Log($"Discovered bug: {myBugID}");
            StartCoroutine(ShowCommandPanel());
        }
        else
        {
            if (commandRevealPanel != null)
            {
                commandRevealPanel.SetActive(true);
                if (commandRevealText != null)
                    commandRevealText.text = revealedCommandText;
            }
        }
    }

    System.Collections.IEnumerator ShowCommandPanel()
    {
        if (commandRevealPanel != null)
        {
            commandRevealPanel.SetActive(true);
            commandRevealText.text = "";

            for (int i = 0; i < 50; i++)
            {
                yield return new WaitForSecondsRealtime(0.05f);
                commandRevealText.text += revealedCommandText;
            }
            commandRevealPanel.SetActive(false);
            commandRevealText.text = "";
        }
        Destroy(this.gameObject);
    }

    public void CloseCommandPanel()
    {
        if (commandRevealPanel != null)
            commandRevealPanel.SetActive(false);
    }
}