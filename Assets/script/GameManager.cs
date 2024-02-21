using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Game Manager")]
public class GameManager : ScriptableObject
{


    public event Action OnChange2CombatMode;
    public event Action OnChange2NavigationMode;
    public event Action<GameObject> OnChageNavigationZone;

    public void Change2CombatMode()
    {

        OnChange2CombatMode?.Invoke();

    }
    public void Change2NavigationMode()
    {

        OnChange2NavigationMode?.Invoke();

    }

    public void ChangeNavigationZone(GameObject zone2Change)
    {

        OnChageNavigationZone?.Invoke(zone2Change);

    }

}
