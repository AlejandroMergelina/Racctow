using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue Manager")]
public class DialogueManager : ScriptableObject
{
    public event Action<TextAsset> OnEnterDialogueMode;
    public event Action OnContinuedialog;

    [SerializeField]
    private InputManager inputManager;

    private void OnEnable()
    {
        inputManager.OnNextLineAction += ContinueDialogue;
    }


    public void EnterDialogueMode(TextAsset inkJSON)
    {

        inputManager.SwichActionMap(ActionMaps.DialogueMode);
        OnEnterDialogueMode?.Invoke(inkJSON);

    }

    public void ContinueDialogue()
    {
        
        OnContinuedialog?.Invoke();
    }

    public void ExitDialogueMode()
    {

        inputManager.SwichActionMap(ActionMaps.MoveOut);

    }

}
