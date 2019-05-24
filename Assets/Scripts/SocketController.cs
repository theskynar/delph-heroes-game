using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySocketIO;

public class SocketController : SocketIOController
{
    public static SocketController instance = null;

    protected override void Awake()
    {
        base.Awake();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
