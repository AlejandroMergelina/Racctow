using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : ScriptableObject
{

    public event Action OnChangeCamera;

    public void ChangeCamera()
    {

        OnChangeCamera?.Invoke();

    }

}
