using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Game Manager")]
public class GameManager : ScriptableObject
{

    public event Action OnChangeCamera2CombatMode;
    public event Action OnChangeCamera2NavigationMode;
    public event Action<List<GameObject>> OnChageNavigationZone;

    public void ChangeCamera2CombatMode()
    {

        OnChangeCamera2CombatMode?.Invoke();

    }
    public void ChangeCamera2NavigationMode()
    {

        OnChangeCamera2NavigationMode?.Invoke();

    }

    public void ChangeNavigationZone(List<GameObject> gameObjects2Change)
    {

        OnChageNavigationZone?.Invoke(gameObjects2Change);

    }

}
