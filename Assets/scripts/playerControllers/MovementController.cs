using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private Vector2 moveInput;
    private Vector2 mouseInput;

    [SerializeField] private float sensitivity;
    [SerializeField] private float moveSpeed;

    public bool canMove = true;

    private void OnMove(InputValue inputValue)
    {
        if (!canMove) return;
        moveInput = inputValue.Get<Vector2>();
    }

    private void OnMouseMove(InputValue inputValue)
    {
        if (!canMove) return;
        mouseInput = inputValue.Get<Vector2>();
    }

    private void Update()
    {
        if (!canMove) return;

        // Move player
        Vector3 positionChange =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        transform.position += positionChange * moveSpeed * Time.deltaTime;

        // Rotate player
        transform.Rotate(Vector3.up, mouseInput.x * sensitivity);
    }

    public Vector2 GetMouseInput() => mouseInput;
    public float GetSensitivity() => sensitivity;
}