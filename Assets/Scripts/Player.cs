using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Character character;
    public HealthSystem healthSystem;
    private GameObject hpBar;

    IEnumerator Start()
    {
        character = GetComponent<Character>();

        healthSystem = new HealthSystem(100);
        

        while (true)
        {
            character.Shot(transform);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {
        character.PointClick();
        character.Rotation();

        hpBar = GameObject.Find("HpBar");
        var image = hpBar.GetComponent<Image>();
        var currHealth = (float) healthSystem.GetHealth();
        var health =  (currHealth / 100.0f);
        image.fillAmount = health;

        Debug.Log("vida: " + health);
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