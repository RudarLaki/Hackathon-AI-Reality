using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private bool firstPerson;
    private Vector2 mouseInput;
    private float sensitivity;

    private PlayerControler playerControler;
    
    [SerializeField] private float thirdPersonDistance = 5f;
    [SerializeField] private float thirdPersonHeight = 2f;

    private float currentPitch = 0f; // vertical angle for third person orbit

    void Start()
    {
        playerControler = transform.parent.GetComponent<PlayerControler>();
        sensitivity = playerControler.GetSensitivity();
        if (firstPerson)
        {
            transform.localPosition = new Vector3(0f, 0f, 0f);
        }
        else
        {
            transform.localPosition = new Vector3(0f, 0f, -2);
        }
    }

    void Update()
    {
        mouseInput = playerControler.GetMouseInput();

        if (firstPerson)
        {
            MoveCameraInFirstPerson();
        }
        else
        {
            MoveCameraInThirdPerson();
        }
    }

    private void MoveCameraInFirstPerson()
    {
        transform.Rotate(Vector3.left, mouseInput.y * sensitivity);
    }

    private void MoveCameraInThirdPerson()
    {
        // Accumulate vertical angle, clamped so camera can't flip over
        currentPitch -= mouseInput.y * sensitivity;
        currentPitch = Mathf.Clamp(currentPitch, -30f, 60f);

        // The player already handles horizontal rotation (mouseInput.x),
        // so we just need to position the camera behind and above the player.
        Transform player = transform.parent;

        // Orbit offset: pull back along player's forward, up by height
        Quaternion pitchRotation = Quaternion.Euler(currentPitch, 0f, 0f);
        Vector3 offset = pitchRotation * new Vector3(0f, 0f, -thirdPersonDistance);
        offset.y += thirdPersonHeight;

        transform.position = player.position + player.TransformDirection(offset);
        transform.LookAt(player.position + Vector3.up * thirdPersonHeight);
    }
}