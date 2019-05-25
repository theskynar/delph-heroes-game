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

    public string allyKey;
    public Statitics gameStats;
    public Statitics.Team allyStats;
    public Statitics.Team enemyStats;
    public Statitics.Player playerStats;

    public DateTime startedAt;

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
            startedAt = DateTime.Now;

            // Parse message
            GameStarted msg = JsonUtility.FromJson<GameStarted>(e.data);
            gameStats = msg.gameStats;
            playersInfo = msg.playerInfos;

            // Verify team stats
            allyKey = "two";
            foreach (var item in gameStats.one.players)
            {
                if (item.name == playerName)
                {
                    allyKey = "one";
                    break;
                }
            }

            UpdateStats();

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

            foreach (var item in msg.one)
            {
                var playerObject = GameObject.Find(item.name);
                var player = playerObject.GetComponent<Player>();
                player.specs = item;

                if (item.attribute.life == 0)
                {
                    GameObject.Find(item.name).SetActive(false);
                } else
                {
                    GameObject.Find(item.name).SetActive(true);
                }
            }

            foreach (var item in msg.two)
            {
                var playerObject = GameObject.Find(item.name);
                var player = playerObject.GetComponent<Player>();
                player.specs = item;

                if (item.attribute.life == 0)
                {
                    GameObject.Find(item.name).SetActive(false);
                }
                else
                {
                    GameObject.Find(item.name).SetActive(true);
                }
            }

            playersInfo = msg;
        });

        io.On("game-statistics-update", (SocketIOEvent e) =>
        {
            Statitics msg = JsonUtility.FromJson<Statitics>(e.data);
            gameStats = msg;

            UpdateStats();
        });

        io.On("player-position-change", (SocketIOEvent e) =>
        {
            PositionChange msg = JsonUtility.FromJson<PositionChange>(e.data);
            Player player = GameObject.Find(msg.playerName).GetComponent<Player>();

            player.character.target.x = msg.position.x;
            player.character.target.y = msg.position.y;
            player.character.PointClick(player.specs.name);
        });

        io.On("player-use-skill", (SocketIOEvent e) =>
        {
            SkillMessage msg = JsonUtility.FromJson<SkillMessage>(e.data);
            SpawnProjectilesScript projectile = GameObject.Find(msg.player).GetComponent<SpawnProjectilesScript>();

            projectile.SpawnByEvent(msg.position, msg.index);
        });

        io.Connect();
    }

    private void UpdateStats()
    {
        if (allyKey == "one")
        {
            allyStats = gameStats.one;
            enemyStats = gameStats.two;
        }
        else
        {
            allyStats = gameStats.two;
            enemyStats = gameStats.one;
        }

        foreach (var player in allyStats.players)
        {
            if (player.name == playerName)
            {
                playerStats = player;
                break;
            }
        }
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

    public void emitUseSkill(int index, Vector3 position)
    {
        SkillMessage msg = new SkillMessage()
        {
            index = index,
            position = position,
            player = playerName
        };

        io.Emit("player-use-skill", JsonUtility.ToJson(msg));
    }

    public void emitAttack(string target)
    {
        AttackMessage msg = new AttackMessage()
        {
            target = target
        };

        io.Emit("player-attack", JsonUtility.ToJson(msg));
    }
}

[Serializable]
public class SkillMessage
{
    public int index;
    public Vector3 position;
    public string player;
}

[Serializable]
public class AttackMessage
{
    public string target;
}