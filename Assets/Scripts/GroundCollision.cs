using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColision : MonoBehaviour
{
    public PlayerController player;
    public bool isGrounded;

    private void Update()
    {
        player.isGrounded = isGrounded;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
