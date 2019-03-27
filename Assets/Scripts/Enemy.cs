using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Transform player;
    public HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = new HealthSystem(100);
    }

    public void DamageTaken(int damage)
    {
        if(healthSystem.GetHealth() == 0) {
            Destroy(this.gameObject);
        } else {
            healthSystem.Damage(damage);
        }
    }

    void FixedUpdate()
    {
        float z = Mathf.Atan2(
            (player.transform.position.y - transform.position.y), 
            (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

        transform.eulerAngles = new Vector3(0, 0, z);

        GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed);
    }
}
