using UnityEngine;

public class SkillTreeUIController : MonoBehaviour
{
    public GameObject skillTreeUI;
    public MovementController playerMovement;

    private bool isOpen = false;

    void Start()
    {
        CloseInstant();
    }

    void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public void Open()
    {
        skillTreeUI.SetActive(true);
        isOpen = true;

        playerMovement.canMove = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Close()
    {
        skillTreeUI.SetActive(false);
        isOpen = false;

        playerMovement.canMove = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void CloseInstant()
    {
        skillTreeUI.SetActive(false);
        isOpen = false;

        playerMovement.canMove = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}