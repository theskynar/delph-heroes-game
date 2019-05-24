using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameSocket;
    public GameObject socketController;

    // Start is called before the first frame update
    void Awake()
    {
        if (SocketController.instance == null)
        {
            Instantiate(socketController);
        }

        if (GameState.instance == null)
        {
            Instantiate(gameSocket);
            GameState.instance.io = SocketController.instance;
        }
    }
}
