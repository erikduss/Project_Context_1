using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMachineController : MonoBehaviour, Iinteractable
{
    private GameManager gameManager;
    private List<int> codes = new List<int>();
    public int codesLeft = 0;
    public bool IsActivated { get; private set; } = false;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        codesLeft = codes.Count;

        foreach(int code in Codes.FinalActivationCodes)
        {
            codes.Add(code);
        }
    }

    public bool AttemptActivationCode(int input)
    {
        if (CheckActivationCode(input))
        {
            codes.Remove(input);
            if(codes.Count <= 0)
            {
                IsActivated = true;
            }
            codesLeft = codes.Count;
            return true;
        }

        return false;
    }

    private bool CheckActivationCode(int input)
    {
        for(int i = 0; i < codes.Count; i++)
        {
            if (input == codes[i]) return true;
        }
        return false;
    }

    public void Interact()
    {
        if (IsActivated)
        {
           gameManager.EndGame();
        }
        else
        {
            gameManager.OpenCodeInputPanel(null,this);
        }
    }
}
