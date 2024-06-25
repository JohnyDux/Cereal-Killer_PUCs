using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public AIController controller;
    public Animator animator;
    [SerializeField] bool talking;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered");

            if (Input.GetKey(KeyCode.E) && !talking)
            {
                StartTalk();
            }
            
            if (talking && Input.GetMouseButtonDown(1))
            {
                StopTalk();
            }

            if (Input.GetMouseButton(0))
            {
                Die();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopTalk();
    }

    private void Die()
    {
        Debug.Log("Killed NPC");
        controller.isAlive = false;
        controller.agent.speed = 0f;
        animator.SetBool("Walking", false);
    }

    private void StartTalk()
    {
        if(controller.isAlive == true)
        {
            Debug.Log("Started Talking");
            controller.hasPath = false;
        }
    }
    private void StopTalk()
    {
        Debug.Log("Stop Talking");
    }

    void GetMurdered(CharacterClass ThisCharacterClass)
    {
        if (ThisCharacterClass == CharacterClass.NPC)
        {
            Die();
        }
        if (ThisCharacterClass == CharacterClass.Detective)
        {
            Kills();
        }
    }

    private void Kills()
    {
        Debug.Log("Lost Game");
        Time.timeScale = 0f;
    }
}
