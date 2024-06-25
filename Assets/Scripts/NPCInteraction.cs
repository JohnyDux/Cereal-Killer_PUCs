using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 10)]
    public string dialogueText;
}

public class NPCInteraction : MonoBehaviour
{
    public AIController controller;
    public Animator animator;
    
    //Dialogue
    public bool talking;
    public DialogueLine[] dialogueLines;
    int currentLineIndex;
    [SerializeField] TextMeshProUGUI dialogueTextBox;

    [SerializeField] GameObject KillingSign;

    [SerializeField] GameObject DialogueSign;

    [SerializeField] GameObject DialogueBox;

    [SerializeField] TextMeshProUGUI CharacterName;

    private void Start()
    {
        DialogueBox.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered");

            if (controller.isAlive)
            {
                KillingSign.SetActive(true);
                DialogueSign.SetActive(true);
            }

            if (Input.GetKey(KeyCode.E) && talking == false)
            {
                StartTalk();
            }
            
            if (talking == true && Input.GetMouseButtonDown(1))
            {
                StopTalk();
            }

            if (Input.GetMouseButton(0))
            {
                GetMurdered(GetComponent<AIController>().characterClass);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopTalk();
    }

    private void StartTalk()
    {
        if(controller.isAlive == true)
        {
            Debug.Log("Started Talking");
            talking = true;

            KillingSign.SetActive(false);
            DialogueSign.SetActive(false);
            CharacterName.text = transform.name;
            DialogueBox.SetActive(true);

            int lineCount = 0;

            while (lineCount < dialogueLines.Length)
            {
                dialogueTextBox.text = dialogueLines[lineCount].dialogueText;
                lineCount++;
            }
        }
    }
    private void StopTalk()
    {
        if (controller.isAlive == true)
        {
            Debug.Log("Stop Talking");
            talking = false;
            KillingSign.SetActive(true);
            DialogueSign.SetActive(true);
            DialogueBox.SetActive(false);
        }
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

    private void Die()
    {
        Debug.Log("Killed NPC");
        controller.isAlive = false;
        controller.agent.speed = 0f;
        animator.SetBool("Walking", false);

        KillingSign.SetActive(false);
        DialogueSign.SetActive(false);
    }

    private void Kills()
    {
        Debug.Log("Lost Game");
        Time.timeScale = 0f;
        //show Lost screen
    }
}
