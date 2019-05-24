using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    private void Start()
    {

        player = GameObject.Find(GameState.instance.playerName);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.transform.position.x + offset.x,
            player.transform.position.y + offset.y,
            player.transform.position.z + offset.z);
    }
}
