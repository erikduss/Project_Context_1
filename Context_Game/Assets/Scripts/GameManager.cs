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

    private TeleporterController currentTeleporter;

    Regex rgx = new Regex("[^0-9]");

    // Start is called before the first frame update
    void Start()
    {
        codeInputPanel.SetActive(false);
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

    public void ConfirmCodeInput()
    {
        bool result;

        if (codeInputText.text.Length < 3)
        {
            result = false;
        }
        else
        {
            result = currentTeleporter.AttemptActivationCode(int.Parse(codeInputText.text));
        }

        if (result)
        {
            resultLabel.color = Color.green;
            resultLabel.text = "Correct code, teleporter unlocked.";
            StartCoroutine(FadeTextToZeroAlpha(3, resultLabel, result));
        }
        else
        {
            resultLabel.color = Color.red;
            resultLabel.text = "Wrong code, try again.";
            StartCoroutine(FadeTextToZeroAlpha(3, resultLabel, result));
        }
    }

    public void OpenCodeInputPanel(TeleporterController teleporter)
    {
        currentTeleporter = teleporter;
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
    public void TeleportPlayer(Vector3 teleportLocation)
    {
        player.transform.position = teleportLocation;
        playerScript.agent.SetDestination(teleportLocation);
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
