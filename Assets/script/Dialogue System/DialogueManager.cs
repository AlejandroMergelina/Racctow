using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue Manager")]
public class DialogueManager : ScriptableObject
{
    public event Action<TextAsset> OnEnterDialogueMode;

    public void EnterDialogueMode(TextAsset inkJSON)
    {

        OnEnterDialogueMode?.Invoke(inkJSON);

    }

}
