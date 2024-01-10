using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractuableForDialogue : Interactuable
{

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Dialogue dialogue;

    [SerializeField]
    private GameObject dialogueGO;
    [SerializeField]
    private TMP_Text dialogueText;

    [SerializeField]
    private Button buttonContinue;

    public override void Interaction()
    {
        //inputManager.
        dialogueGO.SetActive(true);
        dialogueText.text = dialogue.CurrentDialogue;

        buttonContinue.onClick.AddListener(() => Next());

    }

    public void Next()
    {
        Debug.Log("fdsfdsfds");
        if(dialogue.CurrentDialogueIndex >= dialogue.AllDialogue.Length-1)
        {
            dialogueGO.SetActive(false);
            dialogue.ResetDialogue();
        }
        else
        {
            dialogue.NextLine();
            dialogueText.text = dialogue.CurrentDialogue;
        }
    }

}
