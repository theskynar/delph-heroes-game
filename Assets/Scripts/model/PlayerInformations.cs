using System;
using UnityEngine;

[Serializable]
public class PlayerInformations
{
    public PlayerInfo[] one;
    public PlayerInfo[] two;
}

[Serializable]
public class PlayerInfo
{
    public string name;
    public string hero;
    public PlayerAttribute attribute;
    public Vector2 position;

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