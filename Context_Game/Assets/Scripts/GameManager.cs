using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Simple teleporting of the player, for example to a new area.
    public void TeleportPlayer(Vector3 teleportLocation)
    {
        player.transform.position = teleportLocation;
    }
}
