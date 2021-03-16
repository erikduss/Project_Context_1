using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;

    private bool canChangeDestination = true; //The player can only change direction if they are not pathfinding towards a pickup (maybe change this?)
    [HideInInspector] public bool canMove = true;

    //Iinteractable is an interface. It calls the function Interact() on either a pickup or a teleporter at the moment. So any object that can be interacted with.
    Iinteractable currentInteractable; //The last interactable object the player clicked on.

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = agent.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canChangeDestination && canMove)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }

            Iinteractable pickup;

            if (hit.transform != null)
            {
                if (hit.transform.gameObject.tag == "Teleporter")
                {
                    pickup = hit.transform.gameObject.GetComponent<TeleporterController>();
                }
                else if(hit.transform.gameObject.tag == "EndMachine")
                {
                    pickup = hit.transform.gameObject.GetComponent<EndGameMachineController>();
                }
                else
                {
                    pickup = hit.transform.gameObject.GetComponent<Pickup>();
                }

                if (pickup != null)
                {
                    currentInteractable = pickup;
                    canChangeDestination = false;
                }
            }
        }

        //TODO: Fix: If the player clicks on the pickup/teleporter when standing still the distance does not get checked correctly.
        if (!canChangeDestination && agent.speed > 0 && agent.remainingDistance < 0.1f && canMove)
        {
            canChangeDestination = true;
            currentInteractable.Interact();
            currentInteractable = null;
            agent.destination = agent.transform.position;
        }
    }
}
