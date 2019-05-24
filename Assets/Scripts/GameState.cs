using UnityEngine;
using UnitySocketIO.Events;
using UnityEngine.SceneManagement;
using System;

public class GameState : MonoBehaviour
{
    public static GameState instance = null;
    public SocketController io;
    public string playerName;
    public PlayerInformations info;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        startSocket();
    }

    void startSocket()
    {
        Debug.Log("SocketIO starting...");

        io.On("connect", (SocketIOEvent e) => 
        {
            Debug.Log("SocketIO connected");
        });

        io.On("error", (SocketIOEvent e) =>
        {
            Debug.LogError(e);
        });

        io.On("game-started", (SocketIOEvent e) =>
        {
            // Parse message
            GameStartedMessage msg = JsonUtility.FromJson<GameStartedMessage>(e.data);
            info = msg.playerInfos;

            // Go to game scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log(e);
        });

        io.On("game-finished", (SocketIOEvent e) =>
        {
            SceneManager.LoadScene(0);
            Debug.Log(e);
        });

        io.Connect();
    }

    public void emitLobbyEntered()
    {
        TextMessage message = new TextMessage()
        {
            name = "teste"
        };

        io.Emit("lobby-entered", JsonUtility.ToJson(message));
    }

    public void emitPlayerName(string name)
    {
        playerName = name;

        TextMessage message = new TextMessage()
        {
            name = name
        };

        io.Emit("name", JsonUtility.ToJson(message));
    }

    public void emitPlayerPositionChange(Vector2 position)
    {
        io.Emit("player-position-change", JsonUtility.ToJson(position));
    }
}

[Serializable]
class GameStartedMessage
{
    public Statitics gameStats;
    public PlayerInformations playerInfos;
}

class TextMessage
{
    public string name;
}
