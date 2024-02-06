using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueDisplay : MonoBehaviour
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
    private void OnEnable()
    {

        dialogueManager.OnEnterDialogueMode += EnterDialogueMode;
        dialogueManager.OnContinuedialog += ContinueButtom;

    }

    public void ContinueButtom()
    {
        
        if (displayLineCoroutine != null)
        {

            StopCoroutine(displayLineCoroutine);
            DisplayAllLine(currentStory.currentText);

        }
        else if (canContinueToNextLine && currentStory.currentChoices.Count <= 0)
        {

            ContinueStory();

        }

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
        dialogueManager.ExitDialogueMode();

    }

    public void ContinueStory()
    {
        
        if (currentStory.canContinue)
        {            

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

        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        continueButtom.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        foreach (char letter in line.ToCharArray())
        {
            if(letter == '<' || isAddingRichTextTag)
            {

                isAddingRichTextTag = true;
                dialogueText.text += letter;
                if(letter == '>')
                {

                    isAddingRichTextTag = false;

                }

            }
            else
            {

                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);

            }
            
        }

        continueButtom.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;

        displayLineCoroutine = null;

    }

    void DisplayAllLine(string line)
    {
        
        dialogueText.maxVisibleCharacters = line.Length;

        continueButtom.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;

        displayLineCoroutine = null;
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
