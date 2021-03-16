using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour, Iinteractable
{
    private GameManager gameManager;
    [SerializeField] private int teleporterFromPlayer;
    [SerializeField] private int teleporterID;
    [SerializeField] private Vector3 teleportLocation = Vector3.zero;
    public bool IsActivated { get; private set; } = false;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public bool AttemptActivationCode(int input)
    {
        if (CheckActivationCode(input))
        {
            IsActivated = true;
            return true;
        }

        return false;
    }

    private bool CheckActivationCode(int input)
    {
        switch (teleporterFromPlayer)
        {
            case 1:
                switch (teleporterID)
                {
                    case 1:
                        if (input == Codes.Player1_ActivationCode_1) return true;
                        break;
                    case 2:
                        if (input == Codes.Player1_ActivationCode_2) return true;
                        break;
                    case 3:
                        if (input == Codes.Player1_ActivationCode_3) return true;
                        break;
                    default:
                        return false;
                }
                break;
            case 2:
                switch (teleporterID)
                {
                    case 1:
                        if (input == Codes.Player2_ActivationCode_1) return true;
                        break;
                    case 2:
                        if (input == Codes.Player2_ActivationCode_2) return true;
                        break;
                    case 3:
                        if (input == Codes.Player2_ActivationCode_3) return true;
                        break;
                    default:
                        return false;
                }
                break;
            case 3:
                switch (teleporterID)
                {
                    case 1:
                        if (input == Codes.Player2_ActivationCode_1) return true;
                        break;
                    case 2:
                        if (input == Codes.Player2_ActivationCode_2) return true;
                        break;
                    case 3:
                        if (input == Codes.Player2_ActivationCode_3) return true;
                        break;
                    default:
                        return false;
                }
                break;
            case 4:
                switch (teleporterID)
                {
                    case 1:
                        if (input == Codes.Player3_ActivationCode_1) return true;
                        break;
                    case 2:
                        if (input == Codes.Player3_ActivationCode_2) return true;
                        break;
                    case 3:
                        if (input == Codes.Player3_ActivationCode_3) return true;
                        break;
                    default:
                        return false;
                }
                break;
            default:
                return false;
        }
        return false;
    }

    public void Interact()
    {
        if (IsActivated)
        {
            Debug.Log("Teleport!");
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                gameManager.TeleportPlayer(teleportLocation);
            }
        }
        else
        {
            gameManager.OpenCodeInputPanel(this);
        }
    }
}
