using System;

[Serializable]
public class Statitics
{
    public Team one;
    public Team two;

    [Serializable]
    public class Team
    {
        public int kills;
        public int deaths;
        public Player[] players;
    }

    [Serializable]
    public class Player
    {
        public string name;
        public int kills;
        public int deaths;
    }
}