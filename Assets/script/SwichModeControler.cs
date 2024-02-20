using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichModeControler : MonoBehaviour
{
    [SerializeField]
    private GameObject camera1;
    [SerializeField]
    private List<GameObject> objectsNavigationMode;

    [SerializeField]
    private GameObject camera2;
    [SerializeField]
    private List<GameObject> objectsCombatMode;
    [SerializeField]
    private GameManager gameManager;

    private void OnEnable()
    {
        gameManager.OnChangeCamera2CombatMode += ChangeCamera2CombatMode;
        gameManager.OnChangeCamera2NavigationMode += ChangeCamera2NavegationMode;
        gameManager.OnChageNavigationZone += GameManager_OnChageNavigationZone;
    }

    private void GameManager_OnChageNavigationZone(List<GameObject> obj)
    {
        throw new System.NotImplementedException();
    }

    void ChangeCamera2NavegationMode()
    {

        camera1.SetActive(false);
        camera2.SetActive(true);

        foreach (GameObject gM in objectsNavigationMode)
        {

            gM.SetActive(false);

        }
        foreach (GameObject gM in objectsCombatMode)
        {

            gM.SetActive(true);

        }
        
    }

    public void ChangeCamera2CombatMode()
    {
        camera1.SetActive(false);
        camera2.SetActive(true);

        foreach (GameObject gM in objectsCombatMode)
        {

            gM.SetActive(true);

        }
        foreach (GameObject gM in objectsNavigationMode)
        {

            gM.SetActive(false);

        }

    }

}
