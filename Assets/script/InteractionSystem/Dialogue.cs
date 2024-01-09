using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : ScriptableObject
{
    [SerializeField]
    private string[] AllDialogue;

    private string currentDialogue;
    public string CurrentDialogue { get => currentDialogue; set => currentDialogue = value; }
    private int currentDialogueIndex;


    public void nextLine()
    {

        currentDialogueIndex++;
        currentDialogue= AllDialogue[currentDialogueIndex];

    }

}
