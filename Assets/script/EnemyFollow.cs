using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private GameManager gameManager;

    bool follow;

    private void Update()
    {
        
        if(follow) 
        {

            agent.SetDestination(target.position);


        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        follow= true;

    }
    private void OnTriggerExit(Collider other)
    {

        follow = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {

            gameManager.ChangeCamera();

        }
        

    }

}
