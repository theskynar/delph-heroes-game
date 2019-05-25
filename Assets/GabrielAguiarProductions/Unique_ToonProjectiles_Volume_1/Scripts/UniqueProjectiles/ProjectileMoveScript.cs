﻿//
//NOTES:
//This script is used for DEMONSTRATION porpuses of the Projectiles. I recommend everyone to create their own code for their own projects.
//This is just a basic example.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveScript : MonoBehaviour
{

    public bool bounce = false;
    public float bounceForce = 10;
    public float speed;
    [Tooltip("From 0% to 100%")]
    public float accuracy;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public AudioClip shotSFX;
    public AudioClip hitSFX;
    public Player owner;
    public List<GameObject> trails;

    private Vector3 startPos;
    private float speedRandomness;
    private Vector3 offset;
    private bool collided;
    private Rigidbody2D rb;
    private RotateToMouseScript rotateToMouse;
    private GameObject target;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();

        //used to create a radius for the accuracy and have a very unique randomness
        if (accuracy != 100)
        {
            accuracy = 1 - (accuracy / 100);

            for (int i = 0; i < 2; i++)
            {
                var val = 1 * Random.Range(-accuracy, accuracy);
                var index = Random.Range(0, 2);
                if (i == 0)
                {
                    if (index == 0)
                        offset = new Vector3(0, -val, 0);
                    else
                        offset = new Vector3(0, val, 0);
                }
                else
                {
                    if (index == 0)
                        offset = new Vector3(0, offset.y, -val);
                    else
                        offset = new Vector3(0, offset.y, val);
                }
            }
        }

        if (muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward + offset;
            var ps = muzzleVFX.GetComponent<ParticleSystem>();
            if (ps != null)
                Destroy(muzzleVFX, ps.main.duration);
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }

        if (shotSFX != null && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(shotSFX);
        }
    }

    void FixedUpdate()
    {
        if (target != null)
            rotateToMouse.RotateToMouse(gameObject, target.transform.position);
        if (speed != 0 && rb != null)
            rb.velocity = transform.up.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (!bounce)
        {
            var enemyTag = GameState.instance.allyKey == "one" ? "TeamTwo" : "TeamOne";
            if (co.gameObject.tag == enemyTag && !collided)
            {
                if (GameState.instance.playerName != owner.specs.name && owner.specs.name != co.gameObject.name)
                {
                    GameState.instance.emitAttack(co.gameObject.name);
                }

                collided = true;

                if (trails.Count > 0)
                {
                    for (int i = 0; i < trails.Count; i++)
                    {
                        trails[i].transform.parent = null;
                        var ps = trails[i].GetComponent<ParticleSystem>();
                        if (ps != null)
                        {
                            ps.Stop();
                            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                        }
                    }
                }

                speed = 0;
                GetComponent<Rigidbody2D>().isKinematic = true;

                StartCoroutine(DestroyParticle(0f));
            }
        }
        else
        {
            rb.drag = 0.5f;
            Destroy(this);
        }
    }

    public IEnumerator DestroyParticle(float waitTime)
    {

        if (transform.childCount > 0 && waitTime != 0)
        {
            List<Transform> tList = new List<Transform>();

            foreach (Transform t in transform.GetChild(0).transform)
            {
                tList.Add(t);
            }

            while (transform.GetChild(0).localScale.x > 0)
            {
                yield return new WaitForSeconds(0.01f);
                transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                for (int i = 0; i < tList.Count; i++)
                {
                    tList[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

    public void SetTarget(GameObject trg, RotateToMouseScript rotateTo)
    {
        target = trg;
        rotateToMouse = rotateTo;
    }
}
