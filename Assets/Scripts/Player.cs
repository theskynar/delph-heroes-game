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
        character.PointClick(specs.name);
        character.Rotation();
        character.Move();

        UpdateLife();
    }

    public void UpdateLife()
    {
        if (specs.name == GameState.instance.playerName)
        {
            hpBar = GameObject.Find("HpBar");
            var image = hpBar.GetComponent<Image>();
            var health = (float)(specs.attribute.life / specs.attribute.originalLife);
            image.fillAmount = health;
        }
    }
}