using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Game Manager")]
public class GameManager : ScriptableObject
{

    public event Action OnChangeCamera;

    public void ChangeCamera()
    {

        OnChangeCamera?.Invoke();

    }

}
