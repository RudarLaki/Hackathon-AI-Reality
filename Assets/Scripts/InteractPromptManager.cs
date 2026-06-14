using UnityEngine;

public class InteractPromptManager : MonoBehaviour
{
    public GameObject promptObject;

    void Awake()
    {
        promptObject.SetActive(false);
    }

    void LateUpdate()
    {
        promptObject.SetActive(InteractableRegistry.nextFrameAnyInRange);
        InteractableRegistry.nextFrameAnyInRange = false;
    }
}