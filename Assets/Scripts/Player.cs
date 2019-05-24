using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerSpecs specs;
    public Character character;
    public GameObject hpBar;

    void Start()
    {
        character = GetComponent<Character>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
        }
        else if (Input.GetKey(KeyCode.W))
        {
        }

        
        character.PointClick(specs.name);
        character.Rotation();
        character.Move();

        UpdateLife();
    }

    public void UpdateLife()
    {
        hpBar = GameObject.Find("HpBar");
        var image = hpBar.GetComponent<Image>();
        var health = (float) (specs.attribute.life / specs.attribute.originalLife);
        image.fillAmount = health;
    }
}