using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float gravity = -9.81f; // Gravity value
    public float jumpHeight = 1.5f; // Jump height
    public float groundCheckDistance = 0.4f; // Distance to check if grounded
    public float groundCheckOffset = 0.1f; // Offset to adjust ground check position
    
    public LayerMask groundMask; // LayerMask to identify what is considered ground

    public float movementSpeed = 5f; // Movement speed
    public float rotationSpeed = 360f; // Rotation speed in degrees per second

    private CharacterController controller;
    private Vector3 velocity;
    public bool isGrounded;

    public Pause pauseRef;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseRef.GamePaused == true)
            {
                pauseRef.GamePaused = false;
            }
            else
            {
                pauseRef.GamePaused = true;
            }
        }

        if(pauseRef.GamePaused == false)
        {
            // Check if the character is grounded
            isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Small negative value to ensure the character stays grounded
            }

            // Check for jump input and apply jump force
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            // Apply gravity
            velocity.y += gravity * Time.deltaTime;

            // Move the character controller
            controller.Move(velocity * Time.deltaTime);

            // Rotation based on input keys
            RotateAndMoveCharacter();
        }
    }

    void RotateAndMoveCharacter()
    {
        // Get input direction
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right;
        }

        // Normalize the direction if moving diagonally
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        // Rotate towards the input direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move the character controller in the input direction
        controller.Move(moveDirection * movementSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        // Set the gizmo color
        Gizmos.color = Color.red;

        // Draw the ground check sphere
        Gizmos.DrawWireSphere(transform.position + Vector3.up * groundCheckOffset, groundCheckDistance);
    }
}
