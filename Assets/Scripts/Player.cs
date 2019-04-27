using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Character character;
    public HealthSystem healthSystem;

    void Start()
    {
        character = GetComponent<Character>();

        healthSystem = new HealthSystem(100);
    }

    void Update()
    {
        character.PointClick();
        character.Rotation();
    }

    public void DamageTaken(int damage)
    {

        if (healthSystem.GetHealth() == 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            healthSystem.Damage(damage);
        }
    }
}