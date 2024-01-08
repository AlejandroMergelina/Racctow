using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionSystem : MonoBehaviour
{
    private bool canInteract = false;

    public event Action OnInteraction;

    [SerializeField]
    private InputManager inputManager;

    public abstract void Interact();



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            canInteract= true;

        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            canInteract = false;

        }

    }

}
