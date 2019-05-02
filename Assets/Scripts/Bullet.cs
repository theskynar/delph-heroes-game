using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        Player enemy = other.gameObject.GetComponent<Player>();

        if (enemy != null)
        {
            enemy.DamageTaken(5);
        }

    }

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
    }    
}
