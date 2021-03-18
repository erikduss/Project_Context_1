using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject codeInputPanel;

    private PlayerController playerScript;

    [SerializeField] private InputField codeInputText;
    [SerializeField] private Text resultLabel;
    [SerializeField] private Text titleLabel;

    [SerializeField] private GameObject paperPickupPanel;

    private TeleporterController currentTeleporter;
    private EndGameMachineController currentMachine;

    Regex rgx = new Regex("[^0-9]");

    // Start is called before the first frame update
    void Start()
    {
        codeInputPanel.SetActive(false);
        paperPickupPanel.SetActive(false);
        playerScript = player.GetComponent<PlayerController>();
        codeInputText.characterLimit = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (codeInputPanel.activeInHierarchy)
        {
            codeInputText.text = rgx.Replace(codeInputText.text, "");
        }
    }

    public void TogglePaperPanel(bool needsToBeOn)
    {
        if(paperPickupPanel != null)
        {
            if (!paperPickupPanel.activeInHierarchy && needsToBeOn)
            {
                paperPickupPanel.SetActive(true);
                playerScript.canMove = false;
            }
            else if(paperPickupPanel.activeInHierarchy && !needsToBeOn)
            {
                paperPickupPanel.SetActive(false);
                playerScript.canMove = true;
            }
        }
    }

    public void ConfirmCodeInput()
    {
        bool result = false;

        if (codeInputText.text.Length < 3)
        {
            result = false;
        }
        else
        {
            if(currentTeleporter != null)
            {
                result = currentTeleporter.AttemptActivationCode(int.Parse(codeInputText.text));
            }
            else if(currentMachine != null)
            {
                result = currentMachine.AttemptActivationCode(int.Parse(codeInputText.text));
            }
        }

        if (result)
        {
            resultLabel.color = Color.green;
            if (currentTeleporter != null)
            {
                resultLabel.text = "Correct code, teleporter unlocked.";
            }
            else if (currentMachine != null)
            {
                if(currentMachine.codesLeft > 1)
                {
                    resultLabel.text = "Correct code, " + currentMachine.codesLeft + " codes left to activate the machine.";
                }
                else if(currentMachine.codesLeft == 0)
                {
                    resultLabel.text = "Correct code, you can activate the machine now.";
                }
                else
                {
                    resultLabel.text = "Correct code, " + currentMachine.codesLeft + " code left to activate the machine.";
                }
            }
            
            StartCoroutine(FadeTextToZeroAlpha(3, resultLabel, result));
        }
        else
        {
            resultLabel.color = Color.red;
            resultLabel.text = "Wrong code, try again.";
            StartCoroutine(FadeTextToZeroAlpha(3, resultLabel, result));
        }
    }

    public void EndGame()
    {
        Debug.Log("Game end");
        Application.Quit();
    }

    public void OpenCodeInputPanel(TeleporterController teleporter, EndGameMachineController machine)
    {
        if(teleporter != null)
        {
            titleLabel.text = "Enter the code to activate the teleporter.";
            currentTeleporter = teleporter;
            machine = null;
        }
        else if(machine != null)
        {
            titleLabel.text = "Enter the codes to activate the machine.";
            currentMachine = machine;
            currentTeleporter = null;
        }

        codeInputPanel.SetActive(true);
        playerScript.canMove = false;
    }

    public void CloseCodeInputPanel()
    {
        playerScript.canMove = true;
        codeInputPanel.SetActive(false);
        codeInputText.text = string.Empty;
    }

    //Simple teleporting of the player, for example to a new area.
    public void TeleportPlayer(Vector3 teleportLocation, Vector3 cameraPosition)
    {
        playerScript.agent.Warp(teleportLocation);
        //player.transform.position = teleportLocation;
        playerScript.agent.SetDestination(teleportLocation);
        Camera mainCam = Camera.main;
        mainCam.transform.position = cameraPosition;
    }

    public IEnumerator FadeTextToZeroAlpha(float time, Text textElement, bool inputResult)
    {
        textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, 1);
        while (textElement.color.a > 0.0f)
        {
            textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, textElement.color.a - (Time.deltaTime / time));
            yield return null;
        }
        if (inputResult)
        {
            CloseCodeInputPanel();
        }
    }
}
