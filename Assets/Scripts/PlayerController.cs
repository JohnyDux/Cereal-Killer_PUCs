using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public Transform cameraTransform; // Reference to the camera's transform

    private CharacterController characterController;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get input from WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow Keys
        float moveVertical = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow Keys

        // Create a movement vector relative to the camera's direction
        Vector3 move = cameraTransform.right * moveHorizontal + cameraTransform.forward * moveVertical;
        move.y = 0f; // Keep the movement on the horizontal plane

        // Move the character
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Rotate the player to face the direction of movement
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
    }
}
