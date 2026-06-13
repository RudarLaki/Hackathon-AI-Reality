using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{

    private Vector2 moveInput;          
    private Vector2 mouseInput;
    [SerializeField] private float sensitivity = 5;
    [SerializeField] public float moveSpeed = 5;
    
    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }
    
    private void OnLook(InputValue inputValue)
    {
        mouseInput = inputValue.Get<Vector2>();
    }

    private void Update()
    {
        //Move player
        Vector3 positionChange =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;
        transform.position += positionChange * moveSpeed  * Time.deltaTime;
        
        //Rotate player
        transform.Rotate(Vector3.up, mouseInput.x * sensitivity);
    }

    public Vector2 GetMouseInput()
    {
        return mouseInput;
    }

    public float GetSensitivity()
    {
        return sensitivity;
    }
}