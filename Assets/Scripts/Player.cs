using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Character character;
    public HealthSystem healthSystem;
    private GameObject hpBar;

    void Start()
    {
        character = GetComponent<Character>();

        healthSystem = new HealthSystem(100);
    }

    void FixedUpdate()
    {
        character.PointClick();
        character.Rotation();
        character.Move();

        hpBar = GameObject.Find("HpBar");
        var image = hpBar.GetComponent<Image>();
        var currHealth = (float) healthSystem.GetHealth();
        var health =  (currHealth / 100.0f);
        image.fillAmount = health;
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