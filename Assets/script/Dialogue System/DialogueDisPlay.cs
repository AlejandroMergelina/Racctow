using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueDisPlay : MonoBehaviour
{

    [SerializeField]
    private DialogueManager dialogueManager;

    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    private Story currentStory;
    private bool dialogueIsPlaying;
    public bool DialogueIsPlaying { get => dialogueIsPlaying;}


    private void OnEnable()
    {

        dialogueManager.OnEnterDialogueMode += EnterDialogueMode;


    }

    void EnterDialogueMode(TextAsset inkJSON)
    {

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();

    }

    void ExitDialogueMode()
    {

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

    }

    public void ContinueStory()
    {

        if (currentStory.canContinue)
        {

            dialogueText.text = currentStory.Continue();

        }
        else
        {

            ExitDialogueMode();

        }

    }

}
