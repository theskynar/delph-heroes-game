using UnityEngine;
using UnitySocketIO.Events;
using UnityEngine.SceneManagement;
using System;

public class GameState : MonoBehaviour
{
    public static GameState instance = null;
    public SocketController io;
    public string playerName;
    public PlayersInfo playersInfo;
    public Statitics gameStats;

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
            GameStarted msg = JsonUtility.FromJson<GameStarted>(e.data);
            playersInfo = msg.playerInfos;

            // Go to game scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log(e);
        });

        io.On("game-finished", (SocketIOEvent e) =>
        {
            SceneManager.LoadScene(0);
            Debug.Log(e);
        });

        io.On("game-players-update", (SocketIOEvent e) =>
        {
            PlayersInfo msg = JsonUtility.FromJson<PlayersInfo>(e.data);
            playersInfo = msg;
        });

        io.On("game-statistics-update", (SocketIOEvent e) =>
        {
            Statitics msg = JsonUtility.FromJson<Statitics>(e.data);
            gameStats = msg;
        });

        io.On("player-position-change", (SocketIOEvent e) =>
        {
            PositionChange msg = JsonUtility.FromJson<PositionChange>(e.data);
            Player player = GameObject.Find(msg.playerName).GetComponent<Player>();

            player.character.target.x = msg.position.x;
            player.character.target.y = msg.position.y;
            player.character.PointClick(player.specs.name);
        });

        io.Connect();
    }

    public void emitLobbyEntered()
    {
        io.Emit("lobby-entered");
    }

    public void emitPlayerName(string name)
    {
        playerName = name;

        NameMessage message = new NameMessage()
        {
            name = name
        };

        io.Emit("name", JsonUtility.ToJson(message));
    }

    public void emitPlayerPositionChange(Vector2 position)
    {
        io.Emit("player-position-change", JsonUtility.ToJson(position));
    }

    public void emitUseSkill()
    {

    }
}
