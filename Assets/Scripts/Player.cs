using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Character character;
    public HealthSystem healthSystem;
    private GameObject hpBar;
    public GameObject bullet;

    void Start()
    {
        character = GetComponent<Character>();

        healthSystem = new HealthSystem(100);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
            GameObject objeto = Instantiate(bullet, transform.position, rot) as GameObject;
            
        }
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