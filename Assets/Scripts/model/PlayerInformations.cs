using System;
using UnityEngine;

[Serializable]
public class PlayersInfo
{
    public PlayerSpecs[] one;
    public PlayerSpecs[] two;
}

[Serializable]
public class PlayerSpecs
{
    public string name;
    public string hero;
    public PlayerAttribute attribute;

    [Serializable]
    public class PlayerAttribute
    {
        public double life;
        public double originalLife;
        public int damage;
        public int criticalRate;
        public int defense;
        public int defenseRate;
    }
}