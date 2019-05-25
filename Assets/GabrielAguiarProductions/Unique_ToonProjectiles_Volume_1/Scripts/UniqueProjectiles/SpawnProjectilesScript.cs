//
//NOTES:
//This script is used for DEMONSTRATION porpuses of the Projectiles. I recommend everyone to create their own code for their own projects.
//This is just a basic example.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnProjectilesScript : MonoBehaviour
{

    public bool use2D;
    public Text effectName;
    public GameObject firePoint;
    public GameObject cameras;
    public List<GameObject> VFXs = new List<GameObject>();

    private int count = 0;
    private float timeToFire = 0f;
    private GameObject effectToSpawn;
    private List<Camera> camerasList = new List<Camera>();
    private Camera singleCamera;
    private Player player;

    void Start()
    {

        if (cameras.transform.childCount > 0)
        {
            for (int i = 0; i < cameras.transform.childCount; i++)
            {
                camerasList.Add(cameras.transform.GetChild(i).gameObject.GetComponent<Camera>());
            }
            if (camerasList.Count == 0)
            {
                Debug.Log("Please assign one or more Cameras in inspector");
            }
        }
        else
        {
            singleCamera = cameras.GetComponent<Camera>();
            if (singleCamera != null)
                camerasList.Add(singleCamera);
            else
                Debug.Log("Please assign one or more Cameras in inspector");
        }

        if (VFXs.Count > 0)
            effectToSpawn = VFXs[0];
        else
            Debug.Log("Please assign one or more VFXs in inspector");

        if (effectName != null) effectName.text = effectToSpawn.name;

        player = transform.GetComponent<Player>();
    }

    void Update()
    {
        if (player.specs.name == GameState.instance.playerName)
        {
            if (Input.GetKey(KeyCode.Q) && Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1f / effectToSpawn.GetComponent<ProjectileMoveScript>().fireRate;
                effectToSpawn = VFXs[0];

                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameState.instance.emitUseSkill(0, position);
                SpawnVFX(position);
            }
            else if (Input.GetKey(KeyCode.W) && Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1f / effectToSpawn.GetComponent<ProjectileMoveScript>().fireRate;
                effectToSpawn = VFXs[1];

                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameState.instance.emitUseSkill(1, position);
                SpawnVFX(position);
            }
        }
    }

    public void SpawnVFX(Vector3 position)
    {
        GameObject vfx;
        Quaternion rot = Quaternion.LookRotation(position - transform.position, Vector3.forward);
        effectToSpawn.GetComponent<ProjectileMoveScript>().owner = player;
        vfx = Instantiate(effectToSpawn, firePoint.transform.position, rot);
    }

    public void SpawnByEvent(Vector3 position, int index)
    {
        effectToSpawn = VFXs[index];
        effectToSpawn.GetComponent<ProjectileMoveScript>().owner = player;
        GameObject vfx;

        Quaternion rot = Quaternion.LookRotation(position - transform.position, Vector3.forward);

        vfx = Instantiate(effectToSpawn, firePoint.transform.position, rot);
    }
}
