using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;

    // Position control
    public Vector3 target = new Vector3();
    public Vector3 direction = new Vector3();
    public Vector3 position = new Vector3();
    public float speed = 2.0f;
    private bool collided;

    private void OnTriggerEnter2D(Collision co)
    {
        if (co.gameObject.tag != "Bullet")
        {
            collided = true;
            Debug.Log("Colidiu");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player enemy = other.gameObject.GetComponent<Player>();

        if (enemy != null)
        {
            Debug.Log("Collded");
        }
    }

    public void Move()
    {
        rb.velocity = new Vector2(direction.x, direction.y);
    }

    public void PointClick(string name)
    {
        animator = gameObject.GetComponent<Animator>();
        position = gameObject.transform.position;

        if (Input.GetKey(KeyCode.Mouse0) && name == GameState.instance.playerName)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;

            GameState.instance.emitPlayerPositionChange(new Vector2(target.x, target.y));
        }

        if (target != Vector3.zero && (target - position).magnitude >= .06)
        {
            direction = (target - position).normalized;
            gameObject.transform.position += direction * speed * Time.deltaTime;
            animator.SetFloat("Magnitude", 1);
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        else
        {
            direction = Vector3.zero;
            animator.SetFloat("Magnitude", 0);
        }
    }

    public void Rotation()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, 0);
        GetComponent<Rigidbody2D>().angularVelocity = 0;
    }
}
