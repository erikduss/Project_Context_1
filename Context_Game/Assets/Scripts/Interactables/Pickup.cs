using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, Iinteractable
{
    //The game manager is most likely needed to check if the player has an item in their inventory already or not and to add the item to their inventory.
    private GameManager gameManager; 

    [SerializeField] private int pickupNumberInScene; //The name that is displayed/bound to the item.
    [SerializeField] private IventoryItem itemKind = IventoryItem.Default; //The item kind (to decide the displayed icon in the players inventory) -> see interfaces.cs if you want to add more options.

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void Interact()
    {
        bool finalItem = false;
        if (pickupNumberInScene == 1) finalItem = true;

        gameManager.TogglePaperPanel(true, finalItem);
    }
}
