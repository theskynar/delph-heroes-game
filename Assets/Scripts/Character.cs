using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public Animator animator;
    public Vector3 target = new Vector3();
    public Vector3 direction = new Vector3();
    public Vector3 position = new Vector3();
    public float speed = 2.0f;
    public HealthSystem healthSystem;
    public float shotDelay;
    public GameObject bullet;
    public Rigidbody2D rb;
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
            enemy.DamageTaken(5);
        }

    }

    public void Move()
    {
        rb.velocity = new Vector2(direction.x, direction.y);
    }

    public void Shot(Transform origin)
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("atirou");

            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
            transform.rotation = rot;
            GameObject InstantiatedGameObject = Instantiate(bullet, origin.position, rot);
            InstantiatedGameObject.transform.SetParent(origin);
        }
    }

    public void PointClick()
    {
        animator = gameObject.GetComponent<Animator>();

        position = gameObject.transform.position;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
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

        //GameState.instance.emitPlayerPositionChange(new Vector2(target.x, target.y));
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
