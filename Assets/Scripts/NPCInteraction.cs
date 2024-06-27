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

    public bool canDie;
    
    //Dialogue
    public bool talking;
    public DialogueLine[] dialogueLines;
    public int currentDialogueLineIndex;
    public int lastDialogueLineIndex;
    [SerializeField] TextMeshProUGUI dialogueTextBox;

    [SerializeField] GameObject KillingSign;

    [SerializeField] GameObject DialogueSign;

    [SerializeField] GameObject DialogueBox;

    [SerializeField] TextMeshProUGUI CharacterName;

    private void Start()
    {
        DialogueBox.SetActive(false);
        canDie = true;
    }

    private void Update()
    {
        if (talking == true)
        {
            dialogueTextBox.text = dialogueLines[currentDialogueLineIndex].dialogueText;
        } 
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

            if (talking == true && Input.GetMouseButtonDown(0))
            {
                Talking();
            }

            if (talking == true && Input.GetMouseButtonDown(1))
            {
                StopTalk();
            }

            if (Input.GetKey(KeyCode.M) && talking == false)
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
            canDie = false;

            KillingSign.SetActive(false);
            DialogueSign.SetActive(false);
            CharacterName.text = transform.name;
            DialogueBox.SetActive(true);
        }
    }

    private void Talking()
    {
        if (Input.GetMouseButton(0))
        {
            if (currentDialogueLineIndex < dialogueLines.Length || currentDialogueLineIndex == 0)
            {
                lastDialogueLineIndex = currentDialogueLineIndex;
                currentDialogueLineIndex++;
            }
            else
            {
                currentDialogueLineIndex = 0;
            }
        }
    }

    private void StopTalk()
    {
        if (controller.isAlive == true)
        {
            talking = false;
            currentDialogueLineIndex = 0;

            Debug.Log("Stop Talking");
            KillingSign.SetActive(true);
            DialogueSign.SetActive(true);
            DialogueBox.SetActive(false);

            Invoke("CanDie", 5);
        }
    }

    void CanDie()
    {
        canDie = true;
    }

    void GetMurdered(CharacterClass ThisCharacterClass)
    {
        if (ThisCharacterClass == CharacterClass.NPC && canDie == true)
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
        animator.SetTrigger("Death");

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
