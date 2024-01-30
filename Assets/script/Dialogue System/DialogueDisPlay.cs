using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueDisPlay : MonoBehaviour
{
    [SerializeField]
    private float typingSpeed;

    [SerializeField]
    private DialogueManager dialogueManager;

    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private GameObject continueButtom;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private TMP_Text displayNameText;

    private Story currentStory;
    private bool dialogueIsPlaying;
    public bool DialogueIsPlaying { get => dialogueIsPlaying;}

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    [SerializeField]
    private GameObject[] choices;
    [SerializeField]
    private TMP_Text[] choicesText;

    private const string speakerTag = "speaker";

    public void ContinueButtom()
    {

        if (canContinueToNextLine)
        {

            ContinueStory();

        }

    }

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

            if (displayLineCoroutine != null)
            {

                StopCoroutine(displayLineCoroutine);

            }

            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));


            HandleTags(currentStory.currentTags);
        }
        else
        {

            ExitDialogueMode();

        }        

    }

    IEnumerator DisplayLine(string line)
    {

        dialogueText.text = "";

        continueButtom.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButtom.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;

    }

    void HideChoices()
    {

        foreach (GameObject choiceButtom in choices)
        {

            choiceButtom.SetActive(false);

        }

    }

    void HandleTags(List<string> currentTags)
    {

        foreach (string tag in currentTags)
        {

            string[] splitTag = tag.Split(':');

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            if (tagKey == speakerTag)
            {

                displayNameText.text = tagValue;

            }
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
        if (canContinueToNextLine)
        {

            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();

        }
            


    }

}
