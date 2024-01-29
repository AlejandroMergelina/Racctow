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

    [SerializeField]
    private GameObject[] choices;
    [SerializeField]
    private TextMeshProUGUI[] choicesText;


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

            DisplayChoices();

        }
        else
        {

            ExitDialogueMode();

        }

    }

    private void DisplayChoices()
    {
        print("hola");

        List<Choice> currentChoices = currentStory.currentChoices;

        int index = 0;

        foreach(Choice choice in currentChoices)
        {

            choices[index].SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {

            choices[i].SetActive(false);

        }

    }

    public void MakeChoice(int choiceIndex)
    {

        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();


    }

}
