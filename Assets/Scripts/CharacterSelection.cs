using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private PlayersInfo info;
    public GameObject berry;
    public GameObject cyrus;
    public GameObject lezo;
    public GameObject korva;
    public GameObject roann;
    public GameObject stokv;

    private int index;

    private void Awake()
    {
        info = GameState.instance.playersInfo;
        
        foreach (var item in info.one)
        {
            GameObject player;

            if (item.hero == "berry")
            {
                player = Instantiate(berry, new Vector3(-10.75f, -15.65f, 0), Quaternion.identity);
            }
            else if (item.hero == "cyrus")
            {
                player = Instantiate(cyrus, new Vector3(-10.25f, -15.65f, 0), Quaternion.identity);
            }
            else if (item.hero == "lezo")
            {
                player = Instantiate(lezo, new Vector3(-11.25f, -15.65f, 0), Quaternion.identity);
            }
            else if (item.hero == "korva")
            {
                player = Instantiate(korva, new Vector3(-10.75f, -16.65f, 0), Quaternion.identity);
            }
            else if (item.hero == "roann")
            {
                player = Instantiate(roann, new Vector3(-11.25f, -16.65f, 0), Quaternion.identity);
            }
            else
            {
                player = Instantiate(stokv, new Vector3(-10.75f, -14.65f, 0), Quaternion.identity);
            }

            player.SetActive(true);
            player.name = item.name;
            player.tag = "TeamOne";
            player.GetComponent<Player>().specs = item;
        }

        foreach (var item in info.two)
        {
            GameObject player;

            if (item.hero == "berry")
            {
                player = Instantiate(berry, new Vector3(13.75f, -1.5f, 0), Quaternion.identity);          
            }
            else if (item.hero == "cyrus")
            {
                player = Instantiate(cyrus, new Vector3(13.25f, -1.5f, 0), Quaternion.identity);
            }
            else if (item.hero == "lezo")
            {
                player = Instantiate(lezo, new Vector3(13.75f, -2.25f, 0), Quaternion.identity);
            }
            else if (item.hero == "korva")
            {
                player = Instantiate(korva, new Vector3(12.75f, -1.5f, 0), Quaternion.identity);
            }
            else if (item.hero == "roann")
            {
                player = Instantiate(roann, new Vector3(12.75f, -2.25f, 0), Quaternion.identity);
            }
            else
            {
                player = Instantiate(stokv, new Vector3(12.75f, -1.0f, 0), Quaternion.identity);
            }

            player.SetActive(true);
            player.name = item.name;
            player.tag = "TeamTwo";
            player.GetComponent<Player>().specs = item;
        }
    }
}
