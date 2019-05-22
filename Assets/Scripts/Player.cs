using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Character character;
    public HealthSystem healthSystem;
    private GameObject hpBar;
    public GameObject firstSkill;
    public GameObject secondSkill;

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
            Quaternion rot = Quaternion.LookRotation(mousePosition - transform.position, Vector3.forward);
            
            GameObject objeto = Instantiate(firstSkill, transform.position, rot) as GameObject;
            objeto.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100);
        }

        if (Input.GetKey(KeyCode.W))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Quaternion rot = Quaternion.LookRotation(mousePosition - transform.position, Vector3.forward);

            GameObject objeto = Instantiate(secondSkill, transform.position, rot) as GameObject;
            objeto.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100);
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