using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Spaceship : MonoBehaviour
{

    public float speed;
    public float shotDelay;
    public Vector2 targetPosition;
    public GameObject bullet;
    public Animator animator;

    public void Shot (Transform origin)
    {
        if (Input.GetKey(KeyCode.A))
        {
            Instantiate(bullet, origin.position, origin.rotation);
        }
    }

    public void PointClick()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (transform.position.x < targetPosition.x)
        {
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Magnitude", 1);
        } else if (transform.position.x > targetPosition.x)
        {
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Magnitude", 1);
        } else if (transform.position.y < targetPosition.y)
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Magnitude", 1);
        } else if (transform.position.y > targetPosition.y)
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Magnitude", 1);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }

    public void Rotation()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        GetComponent<Rigidbody2D>().angularVelocity = 0;
    }
}
