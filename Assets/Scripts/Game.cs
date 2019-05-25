using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    Text allyKills;
    Text enemyKills;
    Text playerStats;
    Text time;

    Text atack;
    Text critical;
    Text defense;
    Text defenseRate;

    PlayerSpecs specs;

    private float secondsCount = 0;
    private int minuteCount = 10;

    private void Start()
    {
        // Stats
        allyKills = GameObject.Find("AllyKills").GetComponent<Text>();
        enemyKills = GameObject.Find("EnemyKills").GetComponent<Text>();
        playerStats = GameObject.Find("StatsText").GetComponent<Text>();
        time = GameObject.Find("Time").GetComponent<Text>();

        // Info
        atack = GameObject.Find("AttackText").GetComponent<Text>();
        critical = GameObject.Find("CriticalText").GetComponent<Text>();
        defense = GameObject.Find("DefenseText").GetComponent<Text>();
        defenseRate = GameObject.Find("DefenseRateText").GetComponent<Text>();

        // Cache
        specs = GameObject.Find(GameState.instance.playerName).GetComponent<Player>().specs;

        // Update
        UpdateSpecUI();
    }

    private void Update()
    {
        allyKills.text = GameState.instance.allyStats.kills.ToString();
        enemyKills.text = GameState.instance.enemyStats.kills.ToString();
        playerStats.text = string.Format("{0}/{1}", GameState.instance.playerStats.kills, GameState.instance.playerStats.deaths);

        UpdateTime();        
    }

    public void UpdateTime()
    {
        secondsCount -= Time.deltaTime;
        time.text = minuteCount + ":" + (int)secondsCount;
        if (secondsCount <= 0)
        {
            minuteCount--;
            secondsCount = 59;
        }

        if (minuteCount <= 0)
        {
            minuteCount = 0;
            secondsCount = 0;
        }
    }

    public void UpdateSpecUI()
    {
        atack.text = specs.attribute.damage.ToString();
        critical.text = specs.attribute.criticalRate.ToString();
        defense.text = specs.attribute.defense.ToString();
        defenseRate.text = specs.attribute.defenseRate.ToString();
    }

    public void Exit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
