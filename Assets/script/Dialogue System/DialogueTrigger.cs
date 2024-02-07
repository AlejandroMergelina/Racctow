using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private InputManager input;

    [SerializeField]
    private DialogueManager dialogueManager;

    private bool playerInRange = false;

    [SerializeField]
    private TextAsset inkJSON;

    private void OnEnable()
    {

        input.OnInteractAction += Interact;
        
    }

    private void Interact()
    {
        if (playerInRange)
        {
            
            dialogueManager.EnterDialogueMode(inkJSON);

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            playerInRange = true;

        }
        
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            playerInRange = false;

        }

    }

}
