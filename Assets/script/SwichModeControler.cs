using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichModeControler : MonoBehaviour
{
    [SerializeField]
    private GameObject camera1;
    [SerializeField]
    private List<GameObject> objectsMode1;

    [SerializeField]
    private GameObject camera2;
    [SerializeField]
    private GameObject[] objectsMode2;
    [SerializeField]
    private GameManager gameManager;

    private void OnEnable()
    {
        gameManager.OnChangeCamera += SwichCameras;
    }

    void SwichCameras()
    {


        if(camera1.active == true)
        {

            camera1.SetActive(false);
            camera2.SetActive(true);

            foreach (GameObject gM in objectsMode1)
            {

                gM.SetActive(false);

            }
            foreach (GameObject gM in objectsMode2)
            {

                gM.SetActive(true);

            }
        }
        else
        {

            camera1.SetActive(false);
            camera2.SetActive(true);

            foreach (GameObject gM in objectsMode2)
            {

                gM.SetActive(true);

            }
            foreach (GameObject gM in objectsMode1)
            {

                gM.SetActive(false);

            }

        }


    }

}
