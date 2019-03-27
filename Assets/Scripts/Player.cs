using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Spaceship spaceship;

    IEnumerator Start()
    {
        spaceship = GetComponent<Spaceship>();

        while (true)
        {
            spaceship.Shot(transform);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {
        spaceship.PointClick();
        spaceship.Rotation();
    }
}