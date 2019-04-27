using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    private void Start()
    {
        string name = PlayerPrefs.GetString("CharacterName");
        Debug.Log("NOme: " + name);

        player = GameObject.Find(name);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.transform.position.x + offset.x,
            player.transform.position.y + offset.y,
            player.transform.position.z + offset.z);
    }
}
