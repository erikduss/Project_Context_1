using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour, Iinteractable
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void Interact()
    {
        Debug.Log("Teleport!");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            gameManager.TeleportPlayer(new Vector3(0, 0, 0));
        }
    }
}
