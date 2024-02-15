using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField]
    private float typingSpeed;

    [SerializeField]
    private DialogueManager dialogueManager;

    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private GameObject continueIcone;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private TMP_Text displayNameText;

    private Story currentStory;
    private bool dialogueIsPlaying;
    public bool DialogueIsPlaying { get => dialogueIsPlaying; }

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    [SerializeField]
    private GameObject[] choices;
    [SerializeField]
    private TMP_Text[] choicesText;

    [Header("Audio")]

    [SerializeField]
    private AudioClip[] clips;

    [SerializeField, Range(1, 5)]
    private int frecuencyLevel;

    [SerializeField, Range(-3, 3)]
    private float minPitch;

    [SerializeField, Range(-3, 3)]
    private float maxPitch;

    [SerializeField]
    private bool stopAudioSource;

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


    void EnterDialogueMode(Story currentStory)
    {

        this.currentStory = currentStory;
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();

    }

    void ExitDialogueMode()
    {

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        dialogueManager.ExitDialogueMode(currentStory);

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

        continueIcone.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        foreach (char letter in line.ToCharArray())
        {
            if (letter == '<' || isAddingRichTextTag)
            {

                isAddingRichTextTag = true;
                dialogueText.text += letter;
                if (letter == '>')
                {

                    isAddingRichTextTag = false;

                }

            }
            else
            {

                PlayDialogueSound(dialogueText.maxVisibleCharacters);
                dialogueText.maxVisibleCharacters++;

                yield return new WaitForSeconds(typingSpeed);

            }

        }

        continueIcone.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;

        displayLineCoroutine = null;

    }

    private void PlayDialogueSound(int currentDisplayedCharacterCount)
    {


        if(currentDisplayedCharacterCount % frecuencyLevel == 0)
        {

            if (stopAudioSource)
            {

                AudioManager.Instance.StopSound();

            }

            int randomIndex = Random.Range(0, clips.Length);

            AudioManager.Instance.SetPitch2Fbx(Random.Range(minPitch, maxPitch));

            AudioManager.Instance.PlaySound(clips[randomIndex]);

        }

    }

    void DisplayAllLine(string line)
    {

        dialogueText.maxVisibleCharacters = line.Length;

        continueIcone.SetActive(true);
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

        foreach (Choice choice in currentChoices)
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
