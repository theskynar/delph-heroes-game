using UnityEngine;
using System.Collections.Generic;

public class ComponentManager : MonoBehaviour
{
    public static ComponentManager control;      // cheeky self-reference
    public GameObject player;                    // our component reference

    void Awake()
    {
        control = this;                          // linking the self-reference
        DontDestroyOnLoad(transform.gameObject); // set to dont destroy
    }
}