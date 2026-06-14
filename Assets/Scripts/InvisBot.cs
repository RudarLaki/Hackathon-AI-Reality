using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class InvisBot : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] GameObject targetObject;

    [Header("Timing")]
    [SerializeField] float activeTime = 2f;
    [SerializeField] float inactiveTime = 2f;

    [Header("Bug System")]
    [SerializeField] string myBugID;
    [SerializeField] string revealedCommandText;

    [Header("UI")]
    [SerializeField] GameObject commandRevealPanel;
    [SerializeField] TMP_Text commandRevealText;
    
    [SerializeField] Image interactImg;

    [Header("Interaction")]
    [SerializeField] Transform player;
    [SerializeField] float interactRange = 3f;

    bool running = true;
    bool hasBeenInteracted = false;

    void Start()
    {
        StartCoroutine(ToggleRoutine());

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
        if (inRange)
        {
            Debug.Log("IN RANGE - setting AnyInRange true");
            InteractableRegistry.nextFrameAnyInRange = true;
        }
        
        

        if (inRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    IEnumerator ToggleRoutine()
    {
        while (running)
        {
            if (targetObject != null)
                targetObject.SetActive(false);

            yield return new WaitForSeconds(inactiveTime);

            if (targetObject != null)
                targetObject.SetActive(true);

            yield return new WaitForSeconds(activeTime);
        }
    }

    void OnDisable()
    {
        running = false;
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

    IEnumerator ShowCommandPanel()
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
        }
        commandRevealPanel.SetActive(false);
        commandRevealText.text = "";

        if (targetObject != null)
            Destroy(targetObject);
        Destroy(this.gameObject);
    }

    public void CloseCommandPanel()
    {
        if (commandRevealPanel != null)
            commandRevealPanel.SetActive(false);
    }
}