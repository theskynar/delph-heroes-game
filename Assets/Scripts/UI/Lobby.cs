using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public string playerName = "";

    public void UpdateName()
    {
        var nameInput = GameObject.Find("NameInput").GetComponent<InputField>();
        playerName = nameInput.text;
    }

    public void ChooseName()
    {
        GameState.instance.emitPlayerName(playerName);
    }

    public void FindGame()
    {
        GameState.instance.emitLobbyEntered();
    }
}
